using Almond;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace ARAvoid
{
	public class MapState : GameStateBase
	{
		public override string Key => GameState.Map.ToString();
		private MapGenerator mapGenerator;
		private MapUI mapUI;
		private GameState nextState;

		public override async UniTask OnEnterState()
		{
			mapGenerator ??= await GameManager.AddressableContainer.InstanceComponent<MapGenerator>("MapGenerator");
			mapGenerator.gameObject.SetActive(true);

			if(mapUI == null)
			{
				mapUI = await GameManager.AddressableContainer.InstanceComponent<MapUI>(Key + "UI");
				mapUI.AddPrevButtonListener(() =>
				{
					mapGenerator.SetUpEnd();
					nextState = GameState.Main;
				});
				mapUI.AddPlayButtonListener(() =>
				{
					mapGenerator.SetUpEnd();
					nextState = GameState.Play;
				});
			}

			mapUI.gameObject.SetActive(true);
			await mapUI.Active();
		}

		public override async UniTask OnLeaveState()
		{
			mapGenerator.gameObject.SetActive(false);
			await mapUI.Inactive();
			mapUI.gameObject.SetActive(false);
		}

		public override async UniTask ProcessState(CancellationTokenSource cts)
		{
			if(mapGenerator != null)
			{
				await mapGenerator.SetupPositions();
			}

			var mapInfo = await mapGenerator.MapSetting();
			if(nextState == GameState.Play)
			{
				GameManager.Instance.SaveMapPositions(mapInfo);
			}
			mapGenerator.DisableGenerate();
			GameManager.Instance.ChangeState(nextState).Forget();
		}
	}
}