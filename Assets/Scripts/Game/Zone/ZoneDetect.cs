namespace Game.Zone
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ZoneDetect : MonoBehaviour
    {
        public Transform target; // Hedef noktanın referansı
        private bool checkMove = false;

        private void Awake()
        {
            checkMove = false;
        }

        private void Update()
        {
            if (!checkMove) return;
            LaunchObject();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(ZoneTags.Detect))
            {
                Debug.Log(gameObject.name);
                checkMove = true;
            }
        }

        public void LaunchObject()
        {
            Vector2 currentPosition = transform.position;
            transform.position = Vector2.MoveTowards(currentPosition, target.position, 10.0f * Time.deltaTime);
        }
    }
}

