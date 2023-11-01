using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Almond
{
	[RequireComponent(typeof(RectTransform))]
	public class Page : MonoBehaviour
	{
		private RectTransform rectTransform;
		private void Awake()
		{
			rectTransform = GetComponent<RectTransform>();
			rectTransform.anchorMin = Vector3.zero;
			rectTransform.anchorMax = Vector3.one;
			rectTransform.pivot = Vector3.one * 0.5f;
			rectTransform.sizeDelta = Vector2.zero;
		}
	}
}