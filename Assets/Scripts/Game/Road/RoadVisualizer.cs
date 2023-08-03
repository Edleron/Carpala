namespace Game.Road
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    using Game.SOLevel;

    public class RoadVisualizer : MonoBehaviour
    {
        public static RoadVisualizer Instance { get; private set; }

        public GameObject Target;
        public TextMeshPro Text;
        public List<Vector3> RoadTarget = new List<Vector3>();
        public List<GameObject> Rozets = new List<GameObject>();

        private void Awake()
        {
            Instance = this;
        }

        public void SetRoad()
        {
            foreach (var item in Rozets)
            {
                item.SetActive(false);
            }

            int level = LevelManager.Instance.GetLevelIndex();

            if (level >= 10)
            {
                Rozets[0].SetActive(true);
            }

            if (level >= 20)
            {
                Rozets[1].SetActive(true);
            }

            if (level >= 50)
            {
                Rozets[2].SetActive(true);
            }

            Text.text = level.ToString();
            Target.transform.position = RoadTarget[level];
        }
    }
}