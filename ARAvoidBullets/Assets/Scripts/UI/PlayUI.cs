using ARAvoid;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARAvoid
{
	public class PlayUI : MonoBehaviour, Page
	{
		public string Key => Keys.PlayUIKey;

		public async UniTask Active()
		{
			await GameManager.EffectManager.ToggleGlitch(true);
			// ¸Ê ¼¼ÆÃ
		}

		public async UniTask Inactive()
		{
			await GameManager.EffectManager.ToggleGlitch(false);
		}
	}
}