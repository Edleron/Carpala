namespace Edleron.Events
{
    using System;
    public class EventManager
    {
        public static event Action onSwipeUp;
        public static void Fire_onSwipeUp() { onSwipeUp?.Invoke(); }

        public static event Action onSwipeDown;
        public static void Fire_onSwipeDown() { onSwipeDown?.Invoke(); }

        public static event Action onCorrectUp;
        public static void Fire_onCorrectUp() { onCorrectUp?.Invoke(); }
    }
}


