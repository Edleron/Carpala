namespace Game.SOLevel
{
    [System.Serializable]
    public class LevelData
    {
        public int LevelNumber;
        public string LevelName;
        public string LevelDescription;
        public string LevelDifficulty;
        public int TargetScore;
        public int RotatineSpeed;
        public int VisibleStamp;
        public int[] PrepareStamp;
        public int[] PrepareValue;
        public ResultData[] PrepareResult;
    }

    [System.Serializable]
    public class ResultData
    {
        public int result;
        public int valueOne;
        public int valueTwo;
    }
}
