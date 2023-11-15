using Almond;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Linq;

namespace ARAvoid
{
	public class UIManager : MonoBehaviour
	{
		[SerializeField] private PageBase[] pages;
		private PageBase currentPage;

		private bool changingPage;

		public void ChangePage(string key)
		{
			if(changingPage)
			{
				Debug.LogWarning($"Page changing :: {currentPage.Key}");
				return;
			}
			var nextPage = pages.FirstOrDefault(p => p.Key == key);
			if(nextPage == null)
			{
				Debug.LogWarning($"Can't find page :: {key}");
				return;
			}
			ChangePage(nextPage).Forget();
		}

		public async UniTaskVoid ChangePage(PageBase nextPage)
		{
			changingPage = true;
			if(currentPage != null)
			{
				await currentPage.Inactive();
				currentPage.gameObject.SetActive(false);
			}

			currentPage = nextPage;
			currentPage.gameObject.SetActive(true);
			await currentPage.Active();
			changingPage = false;
		}
	}
}