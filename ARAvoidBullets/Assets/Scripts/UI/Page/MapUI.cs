using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ARAvoid
{
	public class MapUI : Almond.PageBase
	{
		[SerializeField] private Button prevButton;
		[SerializeField] private Button playButton;

		public override string Key => Keys.MapUIKey;

		private void OnEnable()
		{
			prevButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Main).Forget());
			playButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Play).Forget());
		}
		private void OnDisable()
		{
			prevButton.onClick.RemoveAllListeners();
			playButton.onClick.RemoveAllListeners();
		}

		public override async UniTask Active()
		{
			prevButton.transform.DOScale(Vector3.zero, 0);
			playButton.transform.DOScale(Vector3.zero, 0);

			prevButton.transform.DOScale(Vector3.one, Defines.DefaultScaleTime);
			playButton.transform.DOScale(Vector3.one, Defines.DefaultScaleTime);

			await GameManager.Instance.EffectManager.ToggleGlitch(true);
		}

		public override async UniTask Inactive()
		{
			prevButton.transform.DOScale(Vector3.zero, 0);
			playButton.transform.DOScale(Vector3.zero, 0);

			prevButton.transform.DOScale(Vector3.one, Defines.DefaultScaleTime);
			playButton.transform.DOScale(Vector3.one, Defines.DefaultScaleTime);

			await GameManager.Instance.EffectManager.ToggleGlitch(false);
		}
	}
}