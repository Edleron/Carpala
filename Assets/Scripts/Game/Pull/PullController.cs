namespace Game.Pull
{
    using UnityEngine;
    using Edleron.Events;

    public class PullController : MonoBehaviour
    {
        private PullVisualizer pullVisualizer;

        private void Awake()
        {
            pullVisualizer = GetComponent<PullVisualizer>();
        }

        public void InitialPull()
        {
            pullVisualizer.PullAnimActiveTrue();
            pullVisualizer.PullInit();
            pullVisualizer.PullGenerate();
        }

        private void OnEnable()
        {
            EventManager.onPull += pullVisualizer.PullAnimActiveTrue;
            EventManager.onSwipeDown += pullVisualizer.PullAnimPassiveTrue;
        }

        private void OnDisable()
        {
            EventManager.onPull -= pullVisualizer.PullAnimActiveTrue;
            EventManager.onSwipeDown -= pullVisualizer.PullAnimPassiveTrue;
        }

    }
}
