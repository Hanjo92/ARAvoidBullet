using UnityEditor.Build.Pipeline;
using UnityEngine;

namespace ARAvoid
{
	public enum ThemaColorType
	{
		Main,
		Light,
		Dark,

		SelectedUI,
		DisabledUI,
	}

	public struct ThemaColors
	{
		public Thema thema;
		public Color main;
		public Color dark;
		public Color light;

		public Color selectedUI;
		public Color disabledUI;

		public Color GetColor(ThemaColorType colorType) =>
			colorType switch
			{
				ThemaColorType.Main => main,
				ThemaColorType.Dark => dark,
				ThemaColorType.Light => light,
				ThemaColorType.SelectedUI => selectedUI,
				ThemaColorType.DisabledUI => disabledUI,
				_ => Color.white,
			};
	}
}