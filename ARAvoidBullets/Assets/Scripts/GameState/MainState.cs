using Almond;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace ARAvoid
{
	public class MainState : GameStateBase
	{
		public override string Key => GameState.Main.ToString();
		private MainUI mainUI;

		public override async UniTask OnEnterState()
		{
			mainUI ??= await GameManager.AddressableContainer.InstanceComponent<MainUI>(Key + "UI");
			mainUI.gameObject.SetActive(true);
			await mainUI.Active();
		}

		public override async UniTask OnLeaveState()
		{
			await mainUI.Inactive();
			mainUI.gameObject.SetActive(false);
		}

		public override async UniTask ProcessState(CancellationTokenSource cts)
		{
		}
	}
}