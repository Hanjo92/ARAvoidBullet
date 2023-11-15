using Almond;
using Cysharp.Threading.Tasks;
using static UnityEditor.Progress;

namespace ARAvoid
{
	public class OptionState : GameStateBase
	{
		public override string Key => GameState.Option.ToString();
		private OptionUI optionUI;

		public override async UniTask OnEnterState()
		{
			optionUI ??= await GameManager.AddressableContainer.InstanceComponent<OptionUI>(Key + "UI");
			optionUI.gameObject.SetActive(true);
			await optionUI.Active();
		}

		public override async UniTask OnLeaveState()
		{
			await optionUI.Inactive();
			optionUI.gameObject.SetActive(false);
		}

		public override async UniTask ProcessState()
		{
		}
	}
}