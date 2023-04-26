using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Background2 : MonoBehaviour
{
    [SerializeField]
    public float speed;
    [SerializeField]
    int startIndex;
    [SerializeField]
    int endIndex;
    [SerializeField]
    Transform[] sprites;
    float viewHeight;
    private int cyclecnt;
    [SerializeField]
    TMP_Text cycle;
    private int tmp;

    private void Awake()
    {
        viewHeight = 55.99f;
        cyclecnt = 0;
        tmp = 0;
    }
    void Update()
    {
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.up * speed * Time.deltaTime;
        transform.position = curPos + nextPos;

        if ((transform.position.y+40+200) % 202 <= 1 && transform.position.y / (viewHeight % 3) >= 1)
        {
            if (tmp == 0)
            {
                cyclecnt++;
                cycle.text = string.Format("\"¿ìÁÖ {0}¹ÙÄû\"", cyclecnt);
                StartCoroutine(cycles());
                tmp = 1;
            }
        }
        else
            tmp = 0;
        

        if (sprites[endIndex].position.y > 0)
        {
            Vector3 downSprite = sprites[endIndex].localPosition;
            sprites[startIndex].transform.localPosition = downSprite + Vector3.down * viewHeight;

            //#.Cursor Index Change
            int endIndexSave = endIndex;
            endIndex = startIndex;
            startIndex = (endIndexSave - 1 == -1) ? sprites.Length - 1 : endIndexSave - 1;

        }
    }
    IEnumerator cycles()
    {
        cycle.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        cycle.gameObject.SetActive(false);
    }
}


