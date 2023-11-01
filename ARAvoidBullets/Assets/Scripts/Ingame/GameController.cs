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
		private float startTime;
		private bool isPaused = false;
		public bool IsPaused => isPaused;
		private bool isContacted = false;

		public void Pause() => isPaused = true;
		public void Resume() => isPaused = false;

		//TODO : Add cts
		public async UniTask<bool> PlayGame()
		{
			isPaused = false;
			setUp = false;
			isContacted = false;
			startTime = Defines.StartCountDown;
			// Ridar Setup
			await UniTask.WaitUntil( () => setUp );
			// Count down
			await UniTask.WaitUntil( () =>
			{
				if( isPaused == false)
				{
					startTime -= Time.deltaTime;
					// TODO : Update Count UI
				}

				return startTime <= 0;
			} );
			// Game Play

			await UniTask.WaitUntil( () =>
			{
				return isContacted;
			} );
			// Direction

			// Wait Input

			// Popup Close

			return true;
		}
	}
}