namespace Game.Level
{
    [System.Serializable]
    public class LevelData
    {
        public int LevelNumber { get; set; }
        public string LevelName { get; set; }
        public string LevelDescription { get; set; }
        public string LevelDifficulty { get; set; }
        public int TargetScore { get; set; }

        // Diğer seviye verileri
    }
}