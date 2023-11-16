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

		private void OnEnable()
		{
			leftHandToggle.onValueChanged.AddListener(v => OptionData.SetUseLeftHandMode(v));
			volumeSlider.onValueChanged.AddListener((v) => OptionData.SetVolume(v));
			effectToggle.onValueChanged.AddListener(v => OptionData.SetReduceEffect(v));
			homeButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Main).Forget());

			leftHandToggle.isOn = OptionData.UseLeftHandMode;
			leftHandToggle.SetValueImmediately(OptionData.UseLeftHandMode);
			volumeSlider.value = OptionData.Volume;
			effectToggle.isOn = OptionData.ReduceEffect;
			effectToggle.SetValueImmediately(OptionData.ReduceEffect);
		}
		private void OnDisable()
		{
			leftHandToggle.onValueChanged.RemoveAllListeners();
			volumeSlider.onValueChanged.RemoveAllListeners();
			effectToggle.onValueChanged.RemoveAllListeners();
			homeButton.onClick.RemoveAllListeners();
		}

		public override async UniTask Active()
		{
			title.transform.DOScale(Vector3.zero, 0);
			leftHandToggle.transform.DOScale(Vector3.zero, 0);
			volumeSlider.transform.DOScale(Vector3.zero, 0);
			effectToggle.transform.DOScale(Vector3.zero, 0);
			changeUI.transform.DOScale(Vector3.zero, 0);
			homeButton.transform.DOScale(Vector3.zero, 0);

			title.transform.DOScale(Vector3.one, Defines.DefaultScaleTime);
			leftHandToggle.transform.DOScale(Vector3.one, Defines.DefaultScaleTime);
			volumeSlider.transform.DOScale(Vector3.one, Defines.DefaultScaleTime);
			effectToggle.transform.DOScale(Vector3.one, Defines.DefaultScaleTime);
			changeUI.transform.DOScale(Vector3.one, Defines.DefaultScaleTime);
			homeButton.transform.DOScale(Vector3.one, Defines.DefaultScaleTime);

			await GameManager.Instance.EffectManager.ToggleGlitch(true);
		}

		public override async UniTask Inactive()
		{
			title.transform.DOScale(Vector3.one, 0);
			leftHandToggle.transform.DOScale(Vector3.one, 0);
			volumeSlider.transform.DOScale(Vector3.one, 0);
			effectToggle.transform.DOScale(Vector3.one, 0);
			changeUI.transform.DOScale(Vector3.one, 0);
			homeButton.transform.DOScale(Vector3.one, 0);

			title.transform.DOScale(Vector3.zero, Defines.DefaultScaleTime);
			leftHandToggle.transform.DOScale(Vector3.zero, Defines.DefaultScaleTime);
			volumeSlider.transform.DOScale(Vector3.zero, Defines.DefaultScaleTime);
			effectToggle.transform.DOScale(Vector3.zero, Defines.DefaultScaleTime);
			changeUI.transform.DOScale(Vector3.zero, Defines.DefaultScaleTime);
			homeButton.transform.DOScale(Vector3.zero, Defines.DefaultScaleTime);

			await GameManager.Instance.EffectManager.ToggleGlitch(false);
		}
	}
}