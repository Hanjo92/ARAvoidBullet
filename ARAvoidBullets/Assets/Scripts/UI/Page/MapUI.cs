using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace ARAvoid
{
	public class MapUI : Almond.PageBase
	{
		[SerializeField] private Button prevButton;
		[SerializeField] private Button playButton;

		public override string Key => Keys.MapUIKey;

		public void AddPrevButtonListener(Action onClick) => prevButton.onClick.AddListener(()=>onClick.Invoke());
		public void AddPlayButtonListener(Action onClick) => playButton.onClick.AddListener(() => onClick.Invoke());

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