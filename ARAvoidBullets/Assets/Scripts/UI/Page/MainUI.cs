using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Almond;

namespace ARAvoid
{
	public class MainUI : PageBase
	{
		[SerializeField] private Image mask;
		[SerializeField] private GameObject title;
		[SerializeField] private Button playButton;
		[SerializeField] private Button optionButton;

		public override string Key => Keys.MainUIKey;

		private void OnEnable()
		{
			var themaColors = ThemaManager.Inst.GetThemaColors();
			playButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Map).Forget());
			optionButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Option).Forget());
		}
		private void OnDisable()
		{
			playButton.onClick.RemoveAllListeners();
			optionButton.onClick.RemoveAllListeners();
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