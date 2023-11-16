using Almond;
using Cysharp.Threading.Tasks;

namespace ARAvoid
{
	public class MapState : GameStateBase
	{
		public override string Key => GameState.Map.ToString();
		private MapUI mapUI;

		public override async UniTask OnEnterState()
		{
			mapUI ??= await GameManager.AddressableContainer.InstanceComponent<MapUI>(Key + "UI");
			mapUI.gameObject.SetActive(true);
			await mapUI.Active();
		}

		public override async UniTask OnLeaveState()
		{
			mapUI.gameObject.SetActive(false);
			await mapUI.Inactive();
		}

		public override async UniTask ProcessState()
		{
		}
	}
}