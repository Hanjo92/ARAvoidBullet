using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ARAvoid
{
	[AddComponentMenu("ARAvoid/UI/ThemaTMP")]
	public class ThemaTMP : TextMeshProUGUI, IThemaUI
	{
		private ThemaColors themaColors;
		protected override void OnEnable()
		{
			base.OnEnable();
			ThemaManager.Inst.AddListener(ApplyThema);

			ApplyThema(ThemaManager.Inst.CurrentThema, true);
		}
		protected override void OnDisable()
		{
			base.OnDisable();
			ThemaManager.Inst.RemoveListener(ApplyThema);
		}

		public void ApplyThema(Thema newThema, bool Immediate)
		{
			if(Application.isPlaying == false)
				return;

			themaColors = GameManager.DataContainer.GetThemaColors(newThema);
			this.DOColor(themaColors.light, Immediate ? 0 : Defines.ChangeThemaTime);
		}
	}
}