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
		[SerializeField] private Image coolTime;
		[SerializeField] private PlayerController joystick;

		private float highScore;

		public PlayerController GetJoyStick() => joystick;
		public void AddEvents(Action paused, Action dash)
		{
			pauseButton.onClick.AddListener(() => paused.Invoke());
			dashButton.onClick.AddListener(() => dash.Invoke());
		}

		public override string Key => Keys.PlayUIKey;

		public override async UniTask Active()
		{
			await GameManager.Instance.EffectManager.ToggleGlitch(false);
			highScore = GameManager.Instance.HighScore;
			highScoreText.text = TimeSpan.FromSeconds(highScore).ToString(@"hh\:mm\:ss\.ff");
			// ¸Ê ¼¼ÆÃ
		}

		public override async UniTask Inactive()
		{
			await GameManager.Instance.EffectManager.ToggleGlitch(true);
		}

		public void UpdateScore(float seconds)
		{
			currentScoreText.text = TimeSpan.FromSeconds(seconds).ToString(@"hh\:mm\:ss\.ff");
			if(highScore < seconds)
			{
				highScore = seconds;
				highScoreText.text = TimeSpan.FromSeconds(highScore).ToString(@"hh\:mm\:ss\.ff");
			}

		}
		public void UpdateCoolTime(float ratio)
		{
			coolTime.fillAmount = 1 - ratio;
		}
	}
}