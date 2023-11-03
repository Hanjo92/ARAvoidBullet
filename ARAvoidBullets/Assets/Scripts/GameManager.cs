using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using Almond;
using System.Threading;
using System.Linq;

namespace ARAvoid
{
	public class GameManager : MonoSingleton<GameManager>
	{
		[SerializeField] private EffectManager effectManager;
		[SerializeField] private DataContainer dataContainer;
		[SerializeField] private GameController gameController;
		[SerializeField] private Page[] pages; 

		public static GameController GameController => Instance.gameController;
		public static EffectManager EffectManager => Instance.effectManager;
		public static DataContainer DataContainer => Instance.dataContainer;

		private CancellationTokenSource gameCTS;

		private SaveData saveData;
		private Page currentPage;

		private bool changingPage;
		private void Awake()
		{
			saveData = SaveData.Load();
		}

		public void OnClickChangePage(string key)
		{
			if(changingPage)
			{
				Debug.LogWarning($"Page changing :: {currentPage.Key}");
				return;
			}

			changingPage = true;
			var nextPage = pages.FirstOrDefault(p => p.Key == key);
			if(nextPage == null)
			{
				Debug.LogWarning($"Can't find page :: {key}");
				return;
			}
			ChangePage(nextPage).Forget();
		}

		public async UniTaskVoid ChangePage(Page nextPage)
		{
			if(currentPage != null)
				await currentPage.Inactive();

			currentPage = nextPage;
			await currentPage.Active();
			changingPage = false;
		}
	}
}