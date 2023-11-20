using Almond;
using Cysharp.Threading.Tasks;
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
		private bool isContacted = false;
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
						PoppupManager.ShowPopup("PausedPopup", ()=> Resume()).Forget();
					},
					()=>{ }
				);
			}
			if(playerController == null)
			{
				playerController = await GameManager.AddressableContainer.InstanceComponent<PlayerController>("PlayerController");

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
			isContacted = false;
			countTime = Defines.StartCountDown;

			// Count down
			await UniTask.WaitUntil(() =>
			{
				if(isPaused == false)
				{
					countTime -= Time.deltaTime;
					// TODO : Update Count UI
				}

				return countTime <= 0;
			});
			// Game Play
			await UniTask.WaitUntil(() =>
			{
				if(isPaused == false)
				{
					playData.avoidTime += Time.deltaTime;
					playUI.UpdateScore(playData.avoidTime);
					//var moveValue = 
				}

				return isContacted;
			});
			// Direction

			// Wait Input

			// Popup Close
		}

		private void OnApplicationFocus(bool focus)
		{
			if(GameManager.Instance.GameState != GameState.Play)
				return;

			if(focus == false)
			{
				Pause();
				PoppupManager.ShowPopup("PausedPopup").Forget();
			}
		}
	}
}