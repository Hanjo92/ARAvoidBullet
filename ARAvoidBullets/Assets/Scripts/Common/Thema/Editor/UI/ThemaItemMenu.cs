using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace Almond
{
	public class ThemaItemMenu : MonoBehaviour
	{
		[MenuItem("GameObject/Almond/Thema/UI/Button")]
		public static void CreateThemaButton(MenuCommand menuCommand)
		{
			var go = new GameObject("Thema Button");
			GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
			go.AddComponent<Image>();
			go.AddComponent<ThemaButton>();
			Undo.RegisterCreatedObjectUndo(go, "Create" + go.name);
			Selection.activeObject = go;
		}
		[MenuItem("GameObject/Almond/Thema/UI/Image")]
		public static void CreateThemaImage(MenuCommand menuCommand)
		{
			var go = new GameObject("Thema Image");
			GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
			go.AddComponent<ThemaImage>();
			Undo.RegisterCreatedObjectUndo(go, "Create" + go.name);
			Selection.activeObject = go;
		}
		[MenuItem("GameObject/Almond/Thema/UI/TMP")]
		public static void CreateThemaTMP(MenuCommand menuCommand)
		{
			var go = new GameObject("Thema TMP");
			GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
			go.AddComponent<ThemaTMP>();
			Undo.RegisterCreatedObjectUndo(go, "Create" + go.name);
			Selection.activeObject = go;
		}
		[MenuItem("GameObject/Almond/Thema/UI/Slider")]
		public static void CreateThemaSlider(MenuCommand menuCommand)
		{
			var go = new GameObject("Thema Slider");
			GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
			go.AddComponent<ThemaSlider>();
			Undo.RegisterCreatedObjectUndo(go, "Create" + go.name);
			Selection.activeObject = go;
		}
		[MenuItem("GameObject/Almond/Thema/UI/SlideToggle")]
		public static void CreateThemaSlideToggle(MenuCommand menuCommand)
		{
			var go = new GameObject("Thema SlideToggle");
			GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
			go.AddComponent<ThemaSlideToggle>();
			Undo.RegisterCreatedObjectUndo(go, "Create" + go.name);
			Selection.activeObject = go;
		}
	}
}