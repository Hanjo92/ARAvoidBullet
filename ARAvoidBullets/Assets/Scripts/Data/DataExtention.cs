using System;
using UnityEngine;

namespace ARAvoid
{
	public static class DataExtention 
	{
		public static void SavePlayData( this SaveData savedData )
		{
			PlayerPrefs.SetFloat( DataKeys.HighScore, savedData.maxTime );
			PlayerPrefs.SetInt( DataKeys.DashCount, savedData.dashCount );
			PlayerPrefs.SetInt( DataKeys.Bullets, savedData.avoidBullets );
			PlayerPrefs.SetInt( DataKeys.PlayCount, savedData.playCount );
		}
		public static void SaveOptionData( this SaveData savedData )
		{
			PlayerPrefs.SetInt( DataKeys.UseLeftHand, savedData.leftHand ? 1 : 0 );
			PlayerPrefs.SetFloat( DataKeys.GameVolume, savedData.volume );
			PlayerPrefs.SetInt( DataKeys.ReduceEffect, savedData.reduceEffect ? 1 : 0 );
		}
	}
}