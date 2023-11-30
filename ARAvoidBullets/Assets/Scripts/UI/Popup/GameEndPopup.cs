using UnityEngine.UI;
using UnityEngine;
using Almond;
using TMPro;
using System;

namespace ARAvoid
{
	public class GameEndPopup : PopupBase
	{
		[SerializeField] private TextMeshProUGUI highestScore;
		[SerializeField] private TextMeshProUGUI currentScore;
		[SerializeField] private Button homeButton;
		[SerializeField] private Button restartButton;

		protected override void OnEnable()
		{
			if(highestScore)
			{
				highestScore.text = TimeSpan.FromSeconds(GameManager.Instance.HighScore).ToString(@"hh\:mm\:ss\.ff");
			}
			if(currentScore)
			{
				currentScore.text = TimeSpan.FromSeconds(GameManager.Instance.HighScore).ToString(@"hh\:mm\:ss\.ff");
			}
		}
	}
}