﻿using System.Linq;
using UnityEngine;

namespace ARAvoid
{
    public static class Defines
    {
        public const float StartCountDown = 5f;

        public const float BulletSpeed = 1f;
        public const int SpreadCount = 8;

        public const float DefaultScaleTime = 0.1f;
        public const float MaxMapHeight = 3f;

        public const float MapMaxAwayDistance = 2f;
        public const int MapVerticsCount = 20;
        public const float MapSize = 1;
        public const float MapVerticsInterval = MapSize / MapVerticsCount;

		public const float GlitchTime = 1.5f;
		public const string GlitchRatioProperty = "_GlitchRatio";

        public const float PlayerDefaultSpeed = 1f;
        public const float DashCoolTime = 3f;

        public static int MapLayer => LayerMask.NameToLayer("Field");
        public static LayerMask MapMask => 1 << MapLayer;
	}

    public static class Keys
    {
        public const string MainUIKey = "_MainUI";
		public const string PlayUIKey = "_PlayUI";
		public const string MapUIKey = "_MapUI";
		public const string OptionUIKey = "_OptionUI";

		public const string HighScore = "Player_High_Score_Seconds";
        public const string DashCount = "Player_Dash_Count";
        public const string Bullets = "Player_Avoid_Bullets_Count";
        public const string PlayCount = "Player_Game_Played_Count";

        public const string UseLeftHand = "Player_Use_Left_Hand";
        public const string GameVolume = "Game_Volume_Level";
        public const string ReduceEffect = "Game_Particle_Reduce";
    }

    public enum Direction
    {
        Forward,
        Right,
        Back,
        Left
    }
}