using Almond;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARAvoid
{
	public class PausedPopup : PopupBase
	{
		[Space]
		[SerializeField] private Toggle leftHandToggle;
		[SerializeField] private Slider volumeSlider;
		[SerializeField] private Button homeButton;
		[SerializeField] private Button resumeButton;

		protected override void Awake()
		{
			base.Awake();

			leftHandToggle.onValueChanged.AddListener(v => OptionData.SetUseLeftHandMode(v));
			volumeSlider.onValueChanged.AddListener((v) => OptionData.SetVolume(v));
			homeButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Main).Forget());
			resumeButton.onClick.AddListener(()=> { ClosePopup().Forget(); });
		}

		private async UniTaskVoid ClosePopup()
		{
			await CloseAnimation();
			GameManager.Instance.GameController.Resume();
			gameObject.SetActive(false);
		}
	}
}