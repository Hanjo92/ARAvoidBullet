using Almond;
using Cysharp.Threading.Tasks;
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
		private PlayUI playUI;
		public float AvoidTime => playData.avoidTime;
		public void Pause() => isPaused = true;
		public void Resume() => isPaused = false;

		public override async UniTask OnEnterState()
		{
			playUI ??= await GameManager.AddressableContainer.InstanceComponent<PlayUI>(Key + "UI");
			playUI.gameObject.SetActive(true);
			await playUI.Active();
		}

		public override async UniTask OnLeaveState()
		{
			await playUI.Inactive();
			playUI.gameObject.SetActive(false);
		}

		public override async UniTask ProcessState()
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