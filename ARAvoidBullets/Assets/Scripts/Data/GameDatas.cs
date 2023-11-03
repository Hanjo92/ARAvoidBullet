using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace ARAvoid
{
	public struct PlayData
	{
		public float avoidTime;
		public int dashCount;
		public int avoidBullets;
	}

	public struct SaveData
	{
		public float maxTime;
		public int dashCount;
		public int avoidBullets;
		public int playCount;

		// Options
		public bool leftHand;
		public float volume;
		public bool reduceEffect;

		public static SaveData Load()
		{
			var data = new SaveData()
			{
				maxTime = PlayerPrefs.GetFloat(Keys.HighScore, 0),
				dashCount = PlayerPrefs.GetInt(Keys.DashCount, 0),
				avoidBullets = PlayerPrefs.GetInt(Keys.Bullets, 0),
				playCount = PlayerPrefs.GetInt(Keys.PlayCount, 0),

				leftHand = PlayerPrefs.GetInt( Keys.UseLeftHand, 0 ) == 1,
				volume = PlayerPrefs.GetFloat(Keys.GameVolume, 1),
				reduceEffect = PlayerPrefs.GetInt(Keys.ReduceEffect, 0) == 1
			};

			return data;
		}

		public void ApplyPlayData(PlayData playData)
		{
			maxTime = Mathf.Max( maxTime, playData.avoidTime );
			dashCount += playData.dashCount;
			avoidBullets += playData.avoidBullets;
			playCount++;
			this.SavePlayData();
		}
	}

	public static class OptionData
	{
		public static bool UseLeftHandMode => PlayerPrefs.GetInt(Keys.UseLeftHand, 0 ) == 1;
		public static bool ReduceEffect => PlayerPrefs.GetInt(Keys.ReduceEffect, 0) == 1;
		public static float volume => PlayerPrefs.GetFloat(Keys.GameVolume, 1);

		public static void SetUseLeftHandMode(bool value) => PlayerPrefs.SetInt(Keys.UseLeftHand, value ? 1 : 0);
		public static void SetReduceEffect(bool value) => PlayerPrefs.SetInt(Keys.ReduceEffect, value ? 1 : 0);
		public static void SetVolume(float value) => PlayerPrefs.SetFloat(Keys.GameVolume, value);
	}
}