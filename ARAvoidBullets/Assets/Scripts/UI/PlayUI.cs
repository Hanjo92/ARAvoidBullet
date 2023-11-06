using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Almond;

namespace ARAvoid
{
	public class PlayUI : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI highScore;
		[SerializeField] private TextMeshProUGUI currentScore;
		[SerializeField] private Button pauseButton;
		[SerializeField] private Button dashButton;
		[SerializeField] private Joystick joystick;

		private void Awake()
		{
			pauseButton.onClick.AddListener(() =>
			{
				GameManager.GameController.Pause();
				PoppupManager.ShowPopup("PausedPopup");
			});
		}

		public void Initialize()
		{
			
		}
	}
}