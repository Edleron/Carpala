namespace Edleron.Events
{
    using System;
    public class EventManager
    {
        public static event Action onSwipeUp;
        public static void Fire_onSwipeUp() { onSwipeUp?.Invoke(); }
    }
}


