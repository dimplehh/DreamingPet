using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float _size = 2;

    public float _upSizeTime = 0.3f;
    public float time = 0.6f;
    // Update is called once per frame
    void Update()
    {
        if (time <= _upSizeTime)
        {
            transform.rotation = Quaternion.Euler(0, 0, 500 * time);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        time += Time.deltaTime;
    }
}
