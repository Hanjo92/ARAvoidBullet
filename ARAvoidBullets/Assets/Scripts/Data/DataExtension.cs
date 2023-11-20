using System;
using UnityEngine;

namespace ARAvoid
{
	public static class DataExtension 
	{
		public static void SavePlayData( this SaveData savedData )
		{
			PlayerPrefs.SetFloat( Keys.HighScore, savedData.maxTime );
			PlayerPrefs.SetInt( Keys.DashCount, savedData.dashCount );
			PlayerPrefs.SetInt( Keys.Bullets, savedData.avoidBullets );
			PlayerPrefs.SetInt( Keys.PlayCount, savedData.playCount );
		}
		public static void SaveOptionData( this SaveData savedData )
		{
			PlayerPrefs.SetInt( Keys.UseLeftHand, savedData.leftHand ? 1 : 0 );
			PlayerPrefs.SetFloat( Keys.GameVolume, savedData.volume );
			PlayerPrefs.SetInt( Keys.ReduceEffect, savedData.reduceEffect ? 1 : 0 );
		}
	}
}