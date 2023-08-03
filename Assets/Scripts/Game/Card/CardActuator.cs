namespace Game.Card
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Edleron.Events;
    using Game.SOLevel;
    using Game.Field;
    using TMPro;

    public class CardActuator : MonoBehaviour
    {
        public static CardActuator Instance { get; private set; }
        public List<GameObject> stamp = new List<GameObject>();

        private Animator animator;
        private SpriteRenderer sp;
        private CardShake cardShake;
        private bool isGamePaused = false;

        private float rotationSpeed = 1.0f;
        [HideInInspector] public bool rotationControl = false;

        #region AWAKE
        private void Awake()
        {
            Instance = this;
            rotationSpeed = 1.0f;
            rotationControl = false;
            animator = GetComponent<Animator>();
            cardShake = GetComponent<CardShake>();
            sp = GetComponent<SpriteRenderer>();

            InitObject();
        }

        private void InitObject()
        {
            sp.enabled = false;
            StampFalse(false);
        }

        #endregion

        #region ROTATIONSPEEDFEATURE
        private void OnEnable()
        {
            EventManager.onUISlider += SetSpeed;
        }

        private void OnDisable()
        {
            EventManager.onUISlider -= SetSpeed;
        }
        public void SetSpeed(int value)
        {
            rotationSpeed = value;
        }
        public float GetRotationSpeed()
        {
            return rotationSpeed;
        }
        #endregion

        #region PUBLIC MEOTDS
        public void BeginCarding()
        {
            if (!sp.enabled)
            {
                sp.enabled = true;
            }

            CardAnimActiveTrue();
        }

        public void StartCarding()
        {
            CardAnimActiveTrue();
        }

        public void FinishCarding()
        {
            CardAnimPassiveTrue();
        }

        public void PauseGame()
        {
            isGamePaused = true;

            var arr = LevelManager.Instance.GetPrepareField();
            for (int i = 0; i < arr.Length; i++)
            {
                Transform stampObje = stamp[arr[i]].transform.GetChild(3);
                FieldRotate fieldRotate = stampObje.GetComponent<FieldRotate>();
                fieldRotate.FieldRotating(0, false);
            }
        }

        public void ResumeGame()
        {
            isGamePaused = false;

            var arr = LevelManager.Instance.GetPrepareField();
            for (int i = 0; i < arr.Length; i++)
            {
                Transform stampObje = stamp[arr[i]].transform.GetChild(3);
                FieldRotate fieldRotate = stampObje.GetComponent<FieldRotate>();
                fieldRotate.FieldRotating(rotationSpeed, true);
            }
        }

        public void RunFielding(string field)
        {
            GameObject foundObject = null;

            foreach (GameObject obj in stamp)
            {
                if (obj.name == field)
                {
                    foundObject = obj;
                    break;
                }
            }

            if (foundObject != null)
            {
                FieldDetect MoveComponent = foundObject.GetComponent<FieldDetect>();

                if (MoveComponent != null)
                {
                    MoveComponent.LaunchControl();
                }
                else
                {

                    Debug.Log("GameObject, Transform Component'e sahip değil: " + foundObject.name);
                }
            }
            else
            {

                Debug.Log("GameObject bulunamadı: " + field);
            }
        }
        #endregion

        private void Update()
        {
            if (!isGamePaused)
            {
                if (rotationControl)
                {
                    CardRotating();
                }
            }
        }

        private void CardRotating()
        {
            transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
        }


        private void Shake()
        {
            cardShake.StartShake();
        }

        private void StampFalse(bool value)
        {
            foreach (var item in stamp)
            {
                item.SetActive(value);
            }
        }

        private void CardGenerate()
        {
            var arr = LevelManager.Instance.GetPrepareField();
            var values = LevelManager.Instance.GetFieldValue();

            for (int i = 0; i < arr.Length; i++)
            {
                stamp[arr[i]].SetActive(true);
                stamp[arr[i]].transform.position = stamp[arr[i]].GetComponent<FieldDetect>().startPosition;
                Transform stampObje = stamp[arr[i]].transform.GetChild(3);
                TextMeshPro textObje = stampObje.GetComponent<TextMeshPro>();
                FieldRotate fieldRotate = stampObje.GetComponent<FieldRotate>();
                textObje.text = values[i].ToString();
                fieldRotate.FieldRotating(rotationSpeed, rotationControl);
            }
        }

        // Animation Events
        private void CardAnimActiveTrue()
        {
            animator.SetBool("active", true);
            Shake();
            StampFalse(false);
            rotationControl = false;
            sp.enabled = true;
        }
        private void CardAnimActiveFalse()
        {
            animator.SetBool("active", false);
            rotationControl = true;
            sp.enabled = true;
            CardGenerate();
        }
        public void CardAnimPassiveTrue()
        {
            animator.SetBool("passive", true);
            rotationControl = false;
            StampFalse(false);
            sp.enabled = true;
            Shake();
        }
        private void CardAnimPassiveFalse()
        {
            animator.SetBool("passive", false);
            rotationControl = false;
            sp.enabled = false;
        }
    }
}