using ARAvoid;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Almond
{
	public static class PoppupManager
	{
		private static Dictionary<string, PopupBase> popups = new Dictionary<string, PopupBase>();

		private static bool IsActivedPoppup(string popupName, ref PopupBase popup)
		{
			if(popups.TryGetValue(popupName, out popup))
			{
				return popup.gameObject.activeSelf;
			}
			return false;
		}

		public static async UniTask<PopupBase> ShowPopup(string popupName, object[] param = null)
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
				popup = await SimplePool.Instantiate<PopupBase>(popupName, param);
				popups.Add(popupName, popup);
			}
			else
			{
				popup.Init(param);
			}
			popup.gameObject.SetActive(true);
			await popup.OpenAnimation();
			return popup;
		}
		public static async UniTask<T> ShowPopup<T>(string popupName, object[] param = null) where T : PopupBase
		{
			var popup = await ShowPopup(popupName);

			return popup == null ? null : popup as T;
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
			ScreenLock.Lock();
			await popup.CloseAnimation();
			popup.Release();
			ScreenLock.Unlock();
		}
	}
}