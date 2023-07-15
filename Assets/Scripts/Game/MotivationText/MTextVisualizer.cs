namespace Game.MotivationText
{
    using UnityEngine;

    public class MTextVisualizer : MonoBehaviour
    {
        public static MTextVisualizer Instance { get; private set; }
        public GameObject correctPrefab;
        public GameObject wrongPrefab;
        private Transform mTransform;

        private void Awake()
        {
            Instance = this;
            mTransform = this.gameObject.transform;
        }

        public void SetNewLevel()
        {
            Instantiate(correctPrefab, new Vector3(0, -4.5f, 0), Quaternion.identity, mTransform);
        }

        public void SetRepeatLevel()
        {
            Instantiate(wrongPrefab, new Vector3(0, -4.5f, 0), Quaternion.identity, mTransform);
        }
    }
}
