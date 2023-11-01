using UnityEngine;

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
				maxTime = PlayerPrefs.GetFloat( DataKeys.HighScore, 0 ),
				dashCount = PlayerPrefs.GetInt( DataKeys.DashCount, 0 ),
				avoidBullets = PlayerPrefs.GetInt( DataKeys.Bullets, 0 ),
				playCount = PlayerPrefs.GetInt(DataKeys.PlayCount, 0),

				leftHand = PlayerPrefs.GetInt( DataKeys.UseLeftHand, 0 ) == 1,
				volume = PlayerPrefs.GetFloat(DataKeys.GameVolume, 1),
				reduceEffect = PlayerPrefs.GetInt(DataKeys.ReduceEffect, 0) == 1
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
}