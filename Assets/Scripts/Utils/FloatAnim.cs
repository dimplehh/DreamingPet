using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAnim : MonoBehaviour
{
    float time = 0;
    public float _size = 5;

    public float _upSizeTime = 0.5f;
    // Update is called once per frame
    void Update()
    {
        if (time <= _upSizeTime)
        {
            transform.Translate(Vector2.down * time * _size);
        }
        else
        {
            transform.Translate(Vector2.up * (time - _upSizeTime) * _size);
        }
        if(time >= _upSizeTime * 2)
        {
            time = 0;
        }
        time += Time.deltaTime;
    }

    public void resetAnim()
    {
        time = 0;
    }
}
