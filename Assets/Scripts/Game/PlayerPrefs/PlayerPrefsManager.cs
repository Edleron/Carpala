namespace Game.PlayerPrefs
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "PlayerPrefs", menuName = "Custom/PlayerPrefs")]
    public class PlayerPrefsManager : ScriptableObject
    {
        public int Level;
        public int Heart;

        public void SaveLevel(int value)
        {
            Level = value;
        }

        public int LoadLevel()
        {
            return Level;
        }

        public void SaveHeart(int value)
        {
            Heart = value;
        }

        public int LoadHeart()
        {
            return Heart;
        }
    }
}