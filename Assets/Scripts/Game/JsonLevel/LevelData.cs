namespace Game.JsonLevel
{
    [System.Serializable]
    public class LevelData
    {
        public int LevelNumber;
        public string LevelName;
        public string LevelDescription;
        public string LevelDifficulty;
        public int TargetScore;
        public int[] PrepareStamp;
        public int Section;

        // Diğer seviye verileri
    }
}