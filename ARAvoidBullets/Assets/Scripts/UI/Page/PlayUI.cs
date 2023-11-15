using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Almond;
using Cysharp.Threading.Tasks;
using System;

namespace ARAvoid
{
	public class PlayUI : PageBase
	{
		[SerializeField] private TextMeshProUGUI highScoreText;
		[SerializeField] private TextMeshProUGUI currentScoreText;
		[SerializeField] private Button pauseButton;
		[SerializeField] private Button dashButton;
		[SerializeField] private Joystick joystick;

		public Vector3 JoystickValue
			=> (joystick == null) ? Vector3.zero : (Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal);

		private float highScore; 

		private void OnEnable()
		{
			pauseButton.onClick.AddListener(() =>
			{
				GameManager.Instance.GameController.Pause();
				PoppupManager.ShowPopup("PausedPopup").Forget();
			});
		}
		private void OnDisable()
		{
			pauseButton.onClick.RemoveAllListeners();
		}

		public void Initialize()
		{
			
		}

		public override string Key => Keys.PlayUIKey;

		public override async UniTask Active()
		{
			await GameManager.Instance.EffectManager.ToggleGlitch(true);
			highScore = GameManager.Instance.HighScore;
			highScoreText.text = TimeSpan.FromSeconds(highScore).ToString(@"hh\:mm\:ss\\.ff");
			// ¸Ê ¼¼ÆÃ
		}

		public override async UniTask Inactive()
		{
			await GameManager.Instance.EffectManager.ToggleGlitch(false);
		}

		public void UpdateScore(float seconds)
		{
			currentScoreText.text = TimeSpan.FromSeconds(seconds).ToString(@"hh\:mm\:ss\\.ff");
			if(highScore < seconds)
			{
				highScore = seconds;
				highScoreText.text = TimeSpan.FromSeconds(highScore).ToString(@"hh\:mm\:ss\\.ff");
			}
		}
	}
}