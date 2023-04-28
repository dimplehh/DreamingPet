using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingMessage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject m1,m2,m3;
    void Start()
    {
        int r = Random.Range(0, 3);
        switch (r)
        {
            case 0:
                m1.gameObject.SetActive(true);
                m2.gameObject.SetActive(false);
                m3.gameObject.SetActive(false);
                break;
            case 1:
                m1.gameObject.SetActive(false);
                m2.gameObject.SetActive(true);
                m3.gameObject.SetActive(false);
                break;
            case 2:
                m1.gameObject.SetActive(false);
                m2.gameObject.SetActive(false);
                m3.gameObject.SetActive(true);
                break;
        }
    }

    // Update is called once per frame

}
