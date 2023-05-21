
namespace Edleron.Stats
{
    using UnityEngine;
    public class FpsStats : MonoBehaviour
    {
        [SerializeField]
        private float _refleshTime = 0.5f;

        private int _frameCount;
        private float _timeCounter;
        private float _fps;

        private void Update()
        {
            if (_timeCounter < _refleshTime)
            {
                _timeCounter += Time.deltaTime;
                _frameCount++;
            }
            else
            {
                _fps = _frameCount / _timeCounter;
                _frameCount = 0;
                _timeCounter = 0;
            }

            Debug.Log(_fps);
        }
    }
}