using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Almond;

namespace ARAvoid
{
	public class GameManager : MonoSingleton<GameManager>
	{
		[SerializeField] private DataContainer dataContainer;
		private GameController gameController;
		public static GameController GameController => Instance.gameController;

		private void Start()
		{
			InitializeGame().Forget();
		}

		private async UniTask InitializeGame()
		{
			var gcPrefab = await Resources.LoadAsync<GameController>( "Prefabs/Manager" );
			if(gcPrefab == null)
			{
				Debug.LogError( "GameController prefab not found" );
				return;
			}
			gameController = Instantiate<GameController>( gcPrefab as GameController );
			if( gameController == null)
			{
				Debug.LogError( "GameController can not instantiate" );
				return;
			}
			// load Data
		}
	}
}