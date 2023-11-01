namespace ARAvoid
{
    public enum Thema
    {
        Neon,
        //Desert
        //Snow
    }


    public static class Defines
    {
        public const float StartCountDown = 5f;

        public const float BulletSpeed = 1f;
        public const int SpreadCount = 8;
    }

    public static class DataKeys
    {
        public const string HighScore = "Player_High_Score_Seconds";
        public const string DashCount = "Player_Dash_Count";
        public const string Bullets = "Player_Avoid_Bullets_Count";
        public const string PlayCount = "Player_Game_Played_Count";

        public const string UseLeftHand = "Player_Use_Left_Hand";
        public const string GameVolume = "Game_Volume_Level";
        public const string ReduceEffect = "Game_Particle_Reduce";
    }
}