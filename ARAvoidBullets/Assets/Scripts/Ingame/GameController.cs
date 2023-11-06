using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace ARAvoid
{
	public class GameController : MonoBehaviour
	{
		// State -> AR Setting -> Play
		//Add AR Manager

		

		private bool setUp = false;
		private float countTime;
		private bool isPaused = false;
		public bool IsPaused => isPaused;
		private bool isContacted = false;
		private PlayData playData;
		public float AvoidTime => playData.avoidTime;
		public void Pause() => isPaused = true;
		public void Resume() => isPaused = false;

		//TODO : Add cts
		public async UniTask<bool> PlayGame()
		{
			playData = new PlayData();
			isPaused = false;
			setUp = false;
			isContacted = false;
			countTime = Defines.StartCountDown;
			// Ridar Setup
			await UniTask.WaitUntil( () => setUp );
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
				}

				return isContacted;
			} );
			// Direction

			// Wait Input

			// Popup Close

			return true;
		}
	}
}