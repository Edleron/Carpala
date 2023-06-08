namespace Game.Pull
{
    using UnityEngine;
    using Edleron.Events;

    public class PullController : MonoBehaviour
    {
        [HideInInspector] public PullVisualizer pullVisualizer;

        private void Awake()
        {
            pullVisualizer = GetComponent<PullVisualizer>();
        }

        private void OnEnable()
        {
            EventManager.onSwipeDown += pullVisualizer.PullAnimPassiveTrue;
        }

        private void OnDisable()
        {
            EventManager.onSwipeDown -= pullVisualizer.PullAnimPassiveTrue;
        }

    }
}
