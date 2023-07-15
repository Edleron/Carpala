namespace Edleron.Events
{
    using System;
    public class EventManager
    {
        #region Touch Input System
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
        public static event Action onTouch;
        public static void Fire_onTouch() { onTouch?.Invoke(); }
        #endregion

        #region Particle FX Çalıştıran Eventler
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

        #region Leven Eventleri
        public static event Action onEndLevel;
        public static void Fire_onEndLevel() { onEndLevel?.Invoke(); }
        // Explosion Event
        public static event Action onNewLevel;
        public static void Fire_onNewLevel() { onNewLevel?.Invoke(); }

        public static event Action onRepeatLevel;
        public static void Fire_onRepeatLevel() { onRepeatLevel?.Invoke(); }
        #endregion
    }
}


