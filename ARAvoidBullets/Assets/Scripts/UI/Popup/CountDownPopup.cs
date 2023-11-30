using Almond;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ARAvoid
{
	public class CountDownPopup : PopupBase
	{
		[SerializeField] private TextMeshProUGUI count;

		public void UpdateCount(float fTime)
		{
			if(fTime <= 0)
			{
				Close();
			}
			count.text = fTime.ToString("f0");
		}
	}
}