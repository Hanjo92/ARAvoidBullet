using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Almond;
using DG.Tweening;
using UnityEngine.Video;
using System;
using Cysharp.Threading.Tasks;

namespace ARAvoid
{
	public class EffectManager : MonoBehaviour
	{
		[SerializeField] private ScriptableRendererData urpRendererData;
		[SerializeField] private GlitchFeature glitch;
		private Material glitchMaterial;

		[SerializeField] private Material sharedUIMaterial;

		private void Awake()
		{
			glitchMaterial = glitch.glitchMaterial;
		}

		public async UniTask ChangeMenuEffect(Action changeAction)
		{
			await ToggleGlitch(true);
			changeAction?.Invoke();
			await ToggleGlitch(false);
		}

		public async UniTask ToggleGlitch(bool active)
		{
			var glitchSeq = DOTween.Sequence();
			glitchSeq.Append(glitchMaterial.DOFloat(0, Defines.GlitchRatioProperty, 0)).
				Append(glitchMaterial.DOFloat(active ? 1f : 0f, Defines.GlitchRatioProperty, Defines.GlitchTime * 0.5f));

			await glitchSeq.AsyncWaitForCompletion();
		}
	}
}