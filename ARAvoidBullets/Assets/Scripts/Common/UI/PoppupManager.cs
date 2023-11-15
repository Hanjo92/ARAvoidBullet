using ARAvoid;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Almond
{
	public static class PoppupManager
	{
		private static AddressableContainer AddressableContainer => GameManager.AddressableContainer;
		private static Dictionary<string, PopupBase> popups = new Dictionary<string, PopupBase>();

		private static bool IsActivedPoppup(string popupName, ref PopupBase popup)
		{
			if(popups.TryGetValue(popupName, out popup))
			{
				return popup.gameObject.activeSelf;
			}
			return false;
		}

		public static async UniTask<PopupBase> ShowPopup(string popupName)
		{
			PopupBase popup = null;
			if(IsActivedPoppup(popupName, ref popup))
			{
				Debug.LogWarning($"Popup is already Opend :: {popupName}");
				return null;
			}

			if(popup == null)
			{
				Debug.Log($"Instancing Popup :: {popupName}");
				popup = await AddressableContainer.InstanceComponent<PopupBase>(popupName);
			}

			popup.gameObject.SetActive(true);
			await popup.OpenAnimation();
			return popup;
		}

		public static void Close(PopupBase popup)
		{
			CloseAsync(popup).Forget();
		}
		public static void Close(string popupName)
		{
			PopupBase popup = null;
			if(IsActivedPoppup(popupName, ref popup) == false)
			{
				Debug.LogWarning($"Popup already closed or not instanced :: {popupName}");
				return;
			}

			CloseAsync(popup).Forget();
		}
		public static async UniTask CloseAsync(PopupBase popup)
		{
			if(popup == null)
			{
				return;
			}

			await popup.CloseAnimation();
			popup.gameObject.SetActive(false);
		}
	}
}