using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkAnim2 : MonoBehaviour
{
    float time;
    int i = 3;
    void Update()
    {
        if (i > 0)
        {
            if (time < 0.25f)
                GetComponent<Image>().color = new Color(1, 1, 1, 1 - time * 2);
            else
            {
                GetComponent<Image>().color = new Color(1, 1, 1, time * 2);
                if (time > 0.5f)
                {
                    time = 0;
                    i--;
                }
            }
            time += Time.deltaTime;
        }
    }
}
