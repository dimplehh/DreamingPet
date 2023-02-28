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

    float viewHeight;

    private void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2;
    }
    void Update()
    {
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.up * speed * Time.deltaTime;
        transform.position = curPos + nextPos;//�ð��� ���� ���ݾ� ȭ�� �̵���Ŵ

        if (sprites[endIndex].position.y > 0)//������ǥ
        {
            //#.Sprite ReUse
            //Vector3 upSpritePos = sprites[startIndex].localPosition;
            Vector3 downSprite = sprites[endIndex].localPosition;
            sprites[startIndex].transform.localPosition = downSprite + Vector3.down * viewHeight;

            //#.Cursor Index Change
            int endIndexSave = endIndex;
            endIndex = startIndex;
            startIndex = (endIndexSave - 1 == -1) ? sprites.Length - 1 : endIndexSave - 1;
        }
    }
}
