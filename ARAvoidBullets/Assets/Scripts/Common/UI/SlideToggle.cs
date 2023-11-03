using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.Collections;
using static UnityEngine.Rendering.DebugUI;

namespace Almond
{
	public class SlideToggle : Toggle
	{
		protected const float SlideTime = 0.2f;

		[Header("Handle")]
		[SerializeField] private RectTransform handle;
		[ReadOnly][SerializeField] private RectTransform handleTrue;
		[ReadOnly][SerializeField] private RectTransform handleFalse;
		[Header("Toggle Effect")]
		[SerializeField] private Image toggleBG;
		protected virtual Color SelectedColor { get; set; }
		protected virtual Color DisabledColor { get; set; }
		[SerializeField] private ParticleSystem effect; 

		private Toggle toggle;
		protected override void Awake()
		{
			base.Awake();
			SetColors();

			toggle.onValueChanged.AddListener(ChangeValueAction);
		}

		protected virtual void SetColors()
		{
			SelectedColor = colors.selectedColor;
			DisabledColor = colors.disabledColor;
		}
		protected virtual void ChangeValueAction(bool value)
		{
			if(effect != null)
				effect.Play();

			ChangeColor(SlideTime);
			ChangeHandlePosition(SlideTime);
		}

		public void SetValueImmediately(bool newValue)
		{
			isOn = newValue;
			ChangeColor(0);
			ChangeHandlePosition(0);
		}

		protected void ChangeColor(float time) => toggleBG?.DOColor(isOn ? SelectedColor : DisabledColor, time);
		protected void ChangeHandlePosition(float time) => handle?.transform.DOMove(isOn ? handleTrue.position : handleFalse.position, time);
	}
}