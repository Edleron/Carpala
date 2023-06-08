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
    }
}


