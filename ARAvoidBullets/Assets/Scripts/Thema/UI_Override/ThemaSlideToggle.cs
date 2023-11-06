using Almond;
using UnityEngine;

namespace ARAvoid
{
	[AddComponentMenu("ARAvoid/UI/ThemaSlideToggle")]
	public class ThemaSlideToggle : SlideToggle, IThemaUI
	{
		[SerializeField] private ThemaColors themaColors;
		protected override Color SelectedColor => themaColors.selectedUI;
		protected override Color DisabledColor => themaColors.disabledUI;

		protected virtual void OnEnable()
		{
			ThemaManager.Inst.AddListener(ApplyThema);
			ApplyThema(ThemaManager.Inst.CurrentThema, true);
		}
		protected virtual void OnDisable()
		{
			ThemaManager.Inst.RemoveListener(ApplyThema);
		}

		public void ApplyThema(Thema newThema, bool Immediate)
		{
			if(Application.isPlaying == false)
				return;

			themaColors = GameManager.DataContainer.GetThemaColors(newThema);
			ChangeColor(Immediate ? 0 : Defines.ChangeThemaTime);
		}
	}
}