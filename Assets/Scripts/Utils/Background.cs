using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    int startIndex;
    [SerializeField]
    int endIndex;
    [SerializeField]
    Transform[] sprites;
    [SerializeField]
    Sprite[] realsprites;
    [SerializeField]
    Sprite[] gradsprites;
    [SerializeField]
    int circle;
    float viewHeight;
    int count;
    int num;

    private void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2;
        count = 1;
        num = 1;
    }
    void Update()
    {
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.up * speed * Time.deltaTime;
        transform.position = curPos + nextPos;//�ð��� ���� ���ݾ� ȭ�� �̵���Ŵ

        if (sprites[endIndex].position.y > 0)//������ǥ
        {
            if (count % circle == circle - 2)
            {
                sprites[startIndex].gameObject.GetComponent<SpriteRenderer>().sprite = gradsprites[num];
            }
            if (count % circle == circle - 1)
            {
                sprites[startIndex].gameObject.GetComponent<SpriteRenderer>().sprite = realsprites[num];
            }
            else if (count % circle == 0)
            {
                for (int i = 0; i < 3; i++) { sprites[i].gameObject.GetComponent<SpriteRenderer>().sprite = realsprites[num]; }
                num++;
                num = (num % 3 == 0) ? 0 : num;
            }

            //#.Sprite ReUse
            Vector3 downSprite = sprites[endIndex].localPosition;
            sprites[startIndex].transform.localPosition = downSprite + Vector3.down * viewHeight;

            //#.Cursor Index Change
            int endIndexSave = endIndex;
            endIndex = startIndex;
            startIndex = (endIndexSave - 1 == -1) ? sprites.Length - 1 : endIndexSave - 1;

            count++;
        }
    }
}