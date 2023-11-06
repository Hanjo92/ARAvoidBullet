using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ARAvoid
{
	public class MainUI : Almond.PageBase
	{
		[SerializeField] private Image mask;
		[SerializeField] private GameObject title;
		[SerializeField] private Button playButton;
		[SerializeField] private Button optionButton;

		public override string Key => Keys.MainUIKey;

		private void Awake()
		{
			playButton.onClick.AddListener(() => GameManager.Instance.OnClickChangePage(Keys.PlayUIKey));
			optionButton.onClick.AddListener(() => GameManager.Instance.OnClickChangePage(Keys.OptionUIKey));
		}

		private void OnEnable()
		{
			var themaColors = GameManager.DataContainer.GetThemaColors(ThemaManager.Inst.CurrentThema);

		}

		public override async UniTask Active()
		{
			title.transform.DOScale(Vector3.zero, 0);
			playButton.transform.DOScale(Vector3.zero, 0);
			optionButton.transform.DOScale(Vector3.zero, 0);

			title.transform.DOScale(Vector3.one, Defines.DefaultScaleTime);
			playButton.transform.DOScale(Vector3.one, Defines.DefaultScaleTime);
			optionButton.transform.DOScale(Vector3.one, Defines.DefaultScaleTime);

			await GameManager.EffectManager.ToggleGlitch(true);
		}

		public override async UniTask Inactive()
		{
			title.transform.DOScale(Vector3.one, 0);
			playButton.transform.DOScale(Vector3.one, 0);
			optionButton.transform.DOScale(Vector3.one, 0);

			title.transform.DOScale(Vector3.zero, Defines.DefaultScaleTime);
			playButton.transform.DOScale(Vector3.zero, Defines.DefaultScaleTime);
			optionButton.transform.DOScale(Vector3.zero, Defines.DefaultScaleTime);

			await GameManager.EffectManager.ToggleGlitch(false);
		}
	}
}