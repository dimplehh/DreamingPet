using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField]
    GameObject spaceReturn;

    private void Awake()
    {
        viewHeight = 55.99f;
    }
    void Update()
    {
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.up * speed * Time.deltaTime;
        transform.position = curPos + nextPos;
        if (transform.position.y % (viewHeight * 3) <= 1 && transform.position.y / (viewHeight % 3) >= 1)
        {
            spaceReturn.SetActive(true);
        }
        else
            spaceReturn.SetActive(false);

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
}
