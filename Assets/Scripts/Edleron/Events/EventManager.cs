namespace Edleron.Events
{
    using System;
    public class EventManager
    {
        // Parmak Yukarı Kaydırma Event'i
        public static event Action onSwipeUp;
        public static void Fire_onSwipeUp() { onSwipeUp?.Invoke(); }

        // Parmak Aşağı Kaydırma Event'i
        public static event Action onSwipeDown;
        public static void Fire_onSwipeDown() { onSwipeDown?.Invoke(); }

        // Parmak Basma Event'i
        public static event Action onTouch;
        public static void Fire_onTouch() { onTouch?.Invoke(); }

        // Explosion Event
        public static event Action onPull;
        public static void Fire_onPull() { onPull?.Invoke(); }

        // Explosion Event
        public static event Action onExplosion;
        public static void Fire_onExplosion() { onExplosion?.Invoke(); }

        // Correct Event
        public static event Action onCorrect;
        public static void Fire_onCorrect() { onCorrect?.Invoke(); }

        // Wrong Event
        public static event Action onWrong;
        public static void Fire_onWrong() { onWrong?.Invoke(); }
    }
}


