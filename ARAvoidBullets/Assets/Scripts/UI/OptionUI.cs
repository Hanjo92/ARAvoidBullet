using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ARAvoid
{
	public class OptionUI : Almond.PageBase
	{
		[SerializeField] private GameObject title;
		[SerializeField] private Toggle leftHandToggle;
		[SerializeField] private Slider volumeSlider;
		[SerializeField] private Toggle effectToggle;
		[SerializeField] private Button homeButton;

		public override string Key => Keys.OptionUIKey;


		private void Awake()
		{
			leftHandToggle.onValueChanged.AddListener(v => OptionData.SetUseLeftHandMode(v));
			volumeSlider.onValueChanged.AddListener((v) => OptionData.SetVolume(v));
			effectToggle.onValueChanged.AddListener(v => OptionData.SetReduceEffect(v));
			homeButton.onClick.AddListener(() =>GameManager.Instance.OnClickChangePage(Keys.MainUIKey));
		}


		public override async UniTask Active()
		{
			title.transform.DOScale(Vector3.zero, 0);
			leftHandToggle.transform.DOScale(Vector3.zero, 0);
			volumeSlider.transform.DOScale(Vector3.zero, 0);
			effectToggle.transform.DOScale(Vector3.zero, 0);
			homeButton.transform.DOScale(Vector3.zero, 0);

			title.transform.DOScale(Vector3.one, Defines.DefaultScaleTime);
			leftHandToggle.transform.DOScale(Vector3.one, Defines.DefaultScaleTime);
			volumeSlider.transform.DOScale(Vector3.one, Defines.DefaultScaleTime);
			effectToggle.transform.DOScale(Vector3.one, Defines.DefaultScaleTime);
			homeButton.transform.DOScale(Vector3.one, Defines.DefaultScaleTime);

			await GameManager.EffectManager.ToggleGlitch(true);
		}

		public override async UniTask Inactive()
		{
			title.transform.DOScale(Vector3.one, 0);
			leftHandToggle.transform.DOScale(Vector3.one, 0);
			volumeSlider.transform.DOScale(Vector3.one, 0);
			effectToggle.transform.DOScale(Vector3.one, 0);
			homeButton.transform.DOScale(Vector3.one, 0);

			title.transform.DOScale(Vector3.zero, Defines.DefaultScaleTime);
			leftHandToggle.transform.DOScale(Vector3.zero, Defines.DefaultScaleTime);
			volumeSlider.transform.DOScale(Vector3.zero, Defines.DefaultScaleTime);
			effectToggle.transform.DOScale(Vector3.zero, Defines.DefaultScaleTime);
			homeButton.transform.DOScale(Vector3.zero, Defines.DefaultScaleTime);

			await GameManager.EffectManager.ToggleGlitch(false);
		}
	}
}