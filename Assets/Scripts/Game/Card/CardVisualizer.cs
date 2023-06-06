namespace Game.Card
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Game.Level;
    using Game.Pump;
    using Game.Zone;
    using TMPro;

    public class CardVisualizer : MonoBehaviour
    {
        private Animator animator;
        private CardShake cardShake;

        public List<GameObject> stamp = new List<GameObject>();

        [HideInInspector] public float rotationSpeed = 1.0f;
        [HideInInspector] public bool rotationControl = false;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            cardShake = GetComponent<CardShake>();
            rotationSpeed = 1.0f;
            rotationControl = false;
        }

        private void Update()
        {
            if (rotationControl)
            {
                ActiveRotate();
            }
        }

        public void Init()
        {
            // Card Shake Effects
            cardShake.StartShake();

            // Set Rotation
            rotationSpeed = rotationSpeed * LevelManager.Instance.GetSectionValue();

            // Set Stamp
            foreach (var item in stamp)
            {
                item.SetActive(false);
            }

            // Active Card Anim
            CardAnimActiveTrue(); // TODO

            // Active Pump
            // TODO

            // Active Stamp
            Invoke("ActiveStamp", 1f);
        }

        private void ActiveStamp()
        {
            rotationControl = true;

            var arr = LevelManager.Instance.GetStampValue();

            int[] randomNumber = GenerateRandomNumbers(arr.Length);

            for (int i = 0; i < arr.Length; i++)
            {
                stamp[arr[i]].SetActive(true);

                // TODO
                Transform stampObje = stamp[arr[i]].transform.GetChild(3);
                TextMeshPro textObje = stampObje.GetComponent<TextMeshPro>();
                ZoneRotate zoneRotate = stampObje.GetComponent<ZoneRotate>();

                textObje.text = randomNumber[i].ToString();
                zoneRotate.ActiveRotate(rotationSpeed, rotationControl);
            }

            // Passive Pump
            // TODO
        }

        private void ActiveRotate()
        {
            transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
        }

        private int[] GenerateRandomNumbers(int length)
        {
            int[] numbers = new int[length];
            System.Random random = new System.Random();

            for (int i = 0; i < length; i++)
            {
                int randomNumber;

                do
                {
                    randomNumber = random.Next(1, 12);
                } while (ArrayContains(numbers, randomNumber));

                numbers[i] = randomNumber;
            }

            return numbers;
        }

        private bool ArrayContains(int[] array, int value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == value)
                {
                    return true;
                }
            }

            return false;
        }

        // Animation Events
        public void CardAnimActiveTrue()
        {
            animator.SetBool("active", true);
        }
        private void CardAnimActiveFalse()
        {
            animator.SetBool("active", false);
        }
        public void CardAnimPassiveTrue()
        {
            animator.SetBool("passive", true);
        }
        private void CardAnimPassiveFalse()
        {
            animator.SetBool("passive", false);
        }
    }
}