using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Test");
    }

    // Update is called once per frame
    void Update()
    {

    }
}


public class TestCode : MonoBehaviour
{
    [SerializeField]
    private float _refleshTime = 0.5f;

    private int _frameCount;
    private float _timeCounter
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