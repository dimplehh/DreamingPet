using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAnim2 : MonoBehaviour
{
    float time = 0;
    public float _size = 5;

    public float _upSizeTime = 1f;
    // Update is called once per frame
    void Update()
    {
        if(time <= _upSizeTime)
        {
            transform.Translate(Vector2.up * _size);
        }
        transform.Translate(Vector2.zero);
        time += Time.deltaTime;
    }
}
