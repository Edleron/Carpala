namespace Game.PlayerPrefs
{
    using UnityEngine;

    public class PlayerPrefsManager : MonoBehaviour
    {
        public static PlayerPrefsManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void SaveLevel(int value)
        {
            PlayerPrefs.SetInt("Level", value);
            PlayerPrefs.Save();
        }

        public int LoadLevel()
        {
            int value = PlayerPrefs.HasKey("Level") == true ? PlayerPrefs.GetInt("Level") : 0;
            return value;
        }

        public void SaveHeart(int value)
        {
            PlayerPrefs.SetInt("Heart", value);
            PlayerPrefs.Save();
        }

        public int LoadHeart()
        {
            int value = PlayerPrefs.HasKey("Heart") == true ? PlayerPrefs.GetInt("Heart") : 0;
            return value;
        }
    }
}