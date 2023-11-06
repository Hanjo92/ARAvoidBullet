using ARAvoid;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARAvoid
{
	public class InGameUI : Almond.PageBase
	{
		#region Map Setting

		#endregion

		#region Play
		
		#endregion

		public override string Key => Keys.PlayUIKey;

		public override async UniTask Active()
		{
			await GameManager.EffectManager.ToggleGlitch(true);
			// ¸Ê ¼¼ÆÃ
		}

		public override async UniTask Inactive()
		{
			await GameManager.EffectManager.ToggleGlitch(false);
		}
	}
}