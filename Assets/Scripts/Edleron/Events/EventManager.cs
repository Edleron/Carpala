namespace Edleron.Events
{
    using System;
    public class EventManager
    {
        #region INPUT
        // Parmak Yukarı Kaydırma Event'i - Ateş Etme
        public static event Action onSwipeUp;
        public static void Fire_onSwipeUp() { onSwipeUp?.Invoke(); }

        // Parmak Aşağı Kaydırma Event'i - Pull Genere Etme
        public static event Action onSwipeDown;
        public static void Fire_onSwipeDown() { onSwipeDown?.Invoke(); }

        // Swipe'ı Locked Sistem
        public static event Action<bool> onSwipeLock;
        public static void Fire_onSwipeLock(bool value) { onSwipeLock?.Invoke(value); }

        // Parmak Basma Event'i
        #endregion

        #region RESULT
        public static event Action<int> onResult;
        public static void Fire_onResult(int value) { onResult?.Invoke(value); }
        #endregion

        #region PARTICLE
        // Explosion Event
        public static event Action onExplosion;
        public static void Fire_onExplosion() { onExplosion?.Invoke(); }

        // Correct Event
        public static event Action onCorrect;
        public static void Fire_onCorrect() { onCorrect?.Invoke(); }

        // Wrong Event
        public static event Action onWrong;
        public static void Fire_onWrong() { onWrong?.Invoke(); }
        #endregion

        #region TIME
        public static event Action onFinishedTime;
        public static void Fire_onFinishedTime() { onFinishedTime?.Invoke(); }
        #endregion

        #region UI
        public static event Action<string> onUITrigger;
        public static void Fire_onUITrigger(string value) { onUITrigger?.Invoke(value); }

        public static event Action<int> onUISlider;
        public static void Fire_onUISlider(int value) { onUISlider?.Invoke(value); }
        #endregion
    }
}


