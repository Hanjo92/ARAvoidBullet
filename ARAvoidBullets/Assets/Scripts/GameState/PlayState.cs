using Almond;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace ARAvoid
{
	public class PlayState : GameStateBase
	{
		public override string Key => GameState.Play.ToString();
		// State -> AR Setting -> Play
		//Add AR Manager
		[SerializeField] private float countTime;
		private bool isPaused = false;
		public bool IsPaused => isPaused;
		private bool isCountDown = false;

		private PlayData playData;

		private PlayerController playerController;
		private PlayUI playUI;
		private Map map;
		public float AvoidTime => playData.avoidTime;
		public void Pause() => isPaused = true;
		public void Resume() => isPaused = false;

		public override async UniTask OnEnterState()
		{
			if(playUI == null)
			{
				playUI = await GameManager.AddressableContainer.InstanceComponent<PlayUI>(Key + "UI");
				playUI.AddEvents(
					()=>
					{
						Pause();
						PoppupManager.ShowPopup("PausedPopup",
							new object[]
							{
								(Action)(()=>{Resume();})
							}).Forget();
					},
					()=>
					{
						Player.Inst.Dash();
						playData.dashCount++;
					}
				);

				playerController = playUI.GetJoyStick();
			}
			
			playUI.gameObject.SetActive(true);
			await playUI.Active();
			map ??= await GameManager.AddressableContainer.InstanceComponent<Map>("Map");
			map.gameObject.SetActive(true);
			map.Setup(GameManager.Instance.GetMapInfo());
		}

		public override async UniTask OnLeaveState()
		{
			await playUI.Inactive();
			playUI.gameObject.SetActive(false);
		}

		public override async UniTask ProcessState(CancellationTokenSource cts)
		{
			playData = new PlayData();
			isPaused = false;
			isCountDown = true;
			countTime = Defines.StartCountDown;

			var countPopup = await PoppupManager.ShowPopup<CountDownPopup>("CountDownPopup");
			ScreenLock.Lock();
			// Count down
			await UniTask.WaitUntil(() =>
			{
				if(isPaused == false)
				{
					countTime -= Time.deltaTime;
					countPopup.UpdateCount(countTime);
				}

				return countTime <= 0;
			});
			ScreenLock.Unlock();
			isCountDown = false;
			// Game Play
			await UniTask.WaitUntil(() =>
			{
				if(isPaused == false)
				{
					var deltaTime = Time.deltaTime;
					playData.avoidTime += deltaTime;
					var moveValue = playerController.GetControllerValue(deltaTime);
					Player.Inst.PlayerUpdate(moveValue, deltaTime);
					playUI.UpdateScore(playData.avoidTime);
					playUI.UpdateCoolTime(Player.Inst.CoolTimeRatio);
				}

				return isCountDown;
			});
			// Direction

			var endPopup = await PoppupManager.ShowPopup("GameEndPopup");
		}

		private void OnApplicationFocus(bool focus)
		{
			if(GameManager.Instance.GameState != GameState.Play)
				return;

			if(isCountDown)
			{
				if(focus)
				{
					Resume();
				}
				else
				{
					Pause();
				}
				return;
			}

			if(focus == false)
			{
				Pause();
				PoppupManager.ShowPopup("PausedPopup",
							new object[]
							{
								(Action)(()=>{Resume();})
							}).Forget();
			}
			
		}
	}
}