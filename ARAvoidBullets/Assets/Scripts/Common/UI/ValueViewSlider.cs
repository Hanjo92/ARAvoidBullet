using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Almond
{
	[RequireComponent(typeof(Slider))]
	public class ValueViewSlider : MonoBehaviour
	{
		private Slider slider;
		[SerializeField] private TextMeshProUGUI valueView;
		[Tooltip("소수 부분 표시 개수")][SerializeField][Range(0, 8)] private int decimalCount = 2;
		private void Awake()
		{
			slider.onValueChanged.AddListener(OnValueChanged);
		}
		protected virtual void OnValueChanged(float value)
		{
			if(valueView != null)
			{
				valueView.text = (value * 100).ToString($"F{decimalCount}");
			}
		}
	}
}