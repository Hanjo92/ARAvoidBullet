using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Almond;

namespace ARAvoid
{
	public class OptionUI : PageBase
	{
		[SerializeField] private GameObject title;
		[SerializeField] private SlideToggle leftHandToggle;
		[SerializeField] private Slider volumeSlider;
		[SerializeField] private SlideToggle effectToggle;
		[SerializeField] private ThemaChangeUI changeUI;
		[SerializeField] private Button homeButton;

		public override string Key => Keys.OptionUIKey;
		private void ToggleLeftHand(bool v) => OptionData.SetUseLeftHandMode(v);
		private void ToggleEffect(bool v) => OptionData.SetReduceEffect(v);
		private void SlideVolume(float v) => OptionData.SetVolume(v);
		private void HomeButton() => GameManager.Instance.ChangeState(GameState.Main).Forget();
		private void OnEnable()
		{
			leftHandToggle.onValueChanged.AddListener(ToggleLeftHand);
			volumeSlider.onValueChanged.AddListener(SlideVolume);
			effectToggle.onValueChanged.AddListener(ToggleEffect);
			homeButton.onClick.AddListener(HomeButton);

			leftHandToggle.isOn = OptionData.UseLeftHandMode;
			leftHandToggle.SetValueImmediately(OptionData.UseLeftHandMode);
			volumeSlider.value = OptionData.Volume;
			effectToggle.isOn = OptionData.ReduceEffect;
			effectToggle.SetValueImmediately(OptionData.ReduceEffect);
		}
		private void OnDisable()
		{
			leftHandToggle.onValueChanged.RemoveListener(ToggleLeftHand);
			volumeSlider.onValueChanged.RemoveListener(SlideVolume);
			effectToggle.onValueChanged.RemoveListener(ToggleEffect);
			homeButton.onClick.RemoveListener(HomeButton);
		}

		public override async UniTask Active()
		{
			await GameManager.Instance.EffectManager.ToggleGlitch(false);
		}

		public override async UniTask Inactive()
		{
			await GameManager.Instance.EffectManager.ToggleGlitch(true);
		}
	}
}