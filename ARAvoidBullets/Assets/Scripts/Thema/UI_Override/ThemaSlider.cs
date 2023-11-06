using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ARAvoid
{
	[AddComponentMenu("ARAvoid/UI/ThemaSlider")]
	public class ThemaSlider : Slider, IThemaUI
	{
		private ThemaColors themaColors;
		private Image frame;
		private Image fill;
		private Image handle;

		protected override void Awake()
		{
			frame = GetComponent<Image>();
			fill = (fillRect == null) ? null : fillRect.GetComponent<Image>();
			handle = (handleRect == null) ? null : handleRect.GetComponent<Image>();
		}

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

		public void ApplyThema(Thema thema, bool Immediate)
		{
			if(Application.isPlaying == false)
				return;

			themaColors = GameManager.DataContainer.GetThemaColors(thema);
			var seq = DOTween.Sequence();
			if(frame != null)
			{
				seq.Join(frame.DOColor(themaColors.light, Immediate ? 0 : Defines.ChangeThemaTime));
			}
			if(fill != null)
			{
				seq.Join(fill.DOColor(themaColors.dark, Immediate ? 0 : Defines.ChangeThemaTime));
			}
			if(handle != null)
			{
				seq.Join(handle.DOColor(themaColors.main, Immediate ? 0 : Defines.ChangeThemaTime));
			}
			seq.Play();
		}
	}
}