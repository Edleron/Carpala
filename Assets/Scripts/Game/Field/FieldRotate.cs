namespace Game.Field
{
    using UnityEngine;

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