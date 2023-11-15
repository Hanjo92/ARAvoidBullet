using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Almond;

namespace ARAvoid
{
	public class GameController : MonoBehaviour
	{
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

		//TODO : Add cts
		public async UniTask<bool> PlayGame()
		{
			playData = new PlayData();
			isPaused = false;
			isContacted = false;
			countTime = Defines.StartCountDown;

			// Count down
			await UniTask.WaitUntil( () =>
			{
				if( isPaused == false)
				{
					countTime -= Time.deltaTime;
					// TODO : Update Count UI
				}

				return countTime <= 0;
			} );
			// Game Play

			await UniTask.WaitUntil( () =>
			{
				if(isPaused == false)
				{
					playData.avoidTime += Time.deltaTime;
					//var moveValue = 
				}

				return isContacted;
			} );
			// Direction

			// Wait Input

			// Popup Close

			return true;
		}

		private void OnApplicationFocus(bool focus)
		{
			if( focus == false)
			{ 
				Pause();
				PoppupManager.ShowPopup("PausedPopup").Forget();
			}
		}
	}
}