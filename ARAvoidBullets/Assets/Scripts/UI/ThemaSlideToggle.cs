using Almond;
using UnityEngine;

namespace ARAvoid
{
	public class ThemaSlideToggle : SlideToggle, IThemaUI
	{
		[SerializeField] private ThemaColors themaColors;
		private Thema thema;
		protected override Color SelectedColor => themaColors.selectedUI;
		protected override Color DisabledColor => themaColors.disabledUI;

		protected virtual void OnEnable()
		{
			ThemaManager.Inst.AddListener(ApplyThema);
			thema = ThemaManager.Inst.CurrentThema;
			ApplyThema(thema, true);
			themaColors = GameManager.DataContainer.GetThemaColors(thema);
		}
		protected virtual void OnDisable()
		{
			ThemaManager.Inst.RemoveListener(ApplyThema);
		}

		public void ApplyThema(Thema newThema, bool Immediate)
		{
			thema = newThema;
			ChangeColor(Immediate ? 0 : SlideTime);
		}

	}
}