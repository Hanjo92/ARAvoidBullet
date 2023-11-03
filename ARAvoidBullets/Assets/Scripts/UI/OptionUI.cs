using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ARAvoid
{
	public class OptionUI : MonoBehaviour, Page
	{
		[SerializeField] private GameObject title;
		[SerializeField] private Toggle leftButton;
		[SerializeField] private Slider volumeSlider;
		[SerializeField] private Toggle effectButton;
		[SerializeField] private Button homeButton;

		public string Key => Keys.OptionUIKey;


		private void Awake()
		{
			leftButton.onValueChanged.AddListener(v => OptionData.SetUseLeftHandMode(v));
			volumeSlider.onValueChanged.AddListener((v) => OptionData.SetVolume(v));
			effectButton.onValueChanged.AddListener(v => OptionData.SetReduceEffect(v));
			homeButton.onClick.AddListener(() =>GameManager.Instance.OnClickChangePage(Keys.MainUIKey));
		}


		public async UniTask Active()
		{
			title.transform.DOScale(Vector3.zero, 0);
			leftButton.transform.DOScale(Vector3.zero, 0);
			volumeSlider.transform.DOScale(Vector3.zero, 0);
			effectButton.transform.DOScale(Vector3.zero, 0);
			homeButton.transform.DOScale(Vector3.zero, 0);

			title.transform.DOScale(Vector3.one, Defines.DefaultScaleTime);
			leftButton.transform.DOScale(Vector3.one, Defines.DefaultScaleTime);
			volumeSlider.transform.DOScale(Vector3.one, Defines.DefaultScaleTime);
			effectButton.transform.DOScale(Vector3.one, Defines.DefaultScaleTime);
			homeButton.transform.DOScale(Vector3.one, Defines.DefaultScaleTime);

			await GameManager.EffectManager.ToggleGlitch(true);
		}

		public async UniTask Inactive()
		{
			title.transform.DOScale(Vector3.one, 0);
			leftButton.transform.DOScale(Vector3.one, 0);
			volumeSlider.transform.DOScale(Vector3.one, 0);
			effectButton.transform.DOScale(Vector3.one, 0);
			homeButton.transform.DOScale(Vector3.one, 0);

			title.transform.DOScale(Vector3.zero, Defines.DefaultScaleTime);
			leftButton.transform.DOScale(Vector3.zero, Defines.DefaultScaleTime);
			volumeSlider.transform.DOScale(Vector3.zero, Defines.DefaultScaleTime);
			effectButton.transform.DOScale(Vector3.zero, Defines.DefaultScaleTime);
			homeButton.transform.DOScale(Vector3.zero, Defines.DefaultScaleTime);

			await GameManager.EffectManager.ToggleGlitch(false);
		}
	}
}