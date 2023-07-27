namespace Game.Field
{
    using UnityEngine;
    using Edleron.Events;

    public class FieldRotate : MonoBehaviour
    {
        private float rotationSpeed = 1.0f;
        private bool rotationControl = false;

        void Update()
        {
            if (rotationControl)
            {
                StartRotate();
            }
        }

        private void OnEnable()
        {
            transform.rotation = Quaternion.identity;
            EventManager.onUISlider += SetSpeed;
        }

        private void OnDisable()
        {
            EventManager.onUISlider -= SetSpeed;
        }

        private void SetSpeed(int value)
        {
            rotationSpeed = -1 * value;
        }

        public void FieldRotating(float speed, bool control)
        {
            rotationSpeed = -1 * speed;
            rotationControl = control;
        }

        private void StartRotate()
        {
            transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
        }
    }
}