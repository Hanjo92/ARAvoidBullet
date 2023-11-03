using Almond;
using System;
using UnityEngine;

namespace ARAvoid
{
	public class ThemaManager : Singleton<ThemaManager>
	{
		public ThemaManager()
		{
			currentThema = (Thema)Enum.Parse(typeof(Thema), PlayerPrefs.GetString(Keys.GameThema, Thema.Neon.ToString()));
		}

		private Thema currentThema;
		public Thema CurrentThema
		{
			get => currentThema;
			private set
			{
				currentThema = value;
				PlayerPrefs.SetString(Keys.GameThema, currentThema.ToString());
			}
		}

		private Action<Thema, bool> OnChangeThema;
		public void AddListener(Action<Thema, bool> action) => OnChangeThema += action;
		public void RemoveListener(Action<Thema, bool> action) => OnChangeThema -= action;
		public void SetThema(Thema newThema, bool Immediate)
		{
			CurrentThema = newThema;
			OnChangeThema?.Invoke(currentThema, Immediate);
		}
	}
}