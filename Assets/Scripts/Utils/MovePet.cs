using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Mathematics;

public class MovePet : MonoBehaviour
{
    Transform bone;
    public float speed;
    [SerializeField]
    Sprite Dog;
    [SerializeField]
    Sprite biteDog;
    [SerializeField] float distanceX, distanceY;
    GameObject boneObject;
    Color color;
    void Start(){
        boneObject = GameObject.Find("bone");//���߿� ���� ���̴� �ڵ�� �ٲ�� �� ��..
        bone = boneObject.transform;
        speed = boneObject.GetComponent<MoveBone>().speed * 0.15f;
        color = boneObject.GetComponent<SpriteRenderer>().color;
    }
    void Update(){
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            distanceX = transform.position.x - bone.position.x;
            distanceY = transform.position.y - bone.position.y;
            if (distanceX > 0.1)
            {
                transform.Translate(new Vector2(-distanceX, 0) * Time.deltaTime * speed);
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (distanceX < -0.1)
            {
                transform.Translate(new Vector2(distanceX, 0) * Time.deltaTime * speed);
                transform.eulerAngles = new Vector3(0, 180, 0);
            }

            if (distanceY > 0.1)
            {
                transform.Translate(new Vector2(0, -distanceY) * Time.deltaTime * speed);
            }
            else if (distanceY < -0.1)
            {
                transform.Translate(new Vector2(0, -distanceY) * Time.deltaTime * speed);
            }

            if (Mathf.Abs(distanceX) < 0.15 && Mathf.Abs(distanceY) < 0.15)
            {
                GetComponent<SpriteRenderer>().sprite = biteDog;
                color.a = 0.0f;
                boneObject.GetComponent<SpriteRenderer>().color = color;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = Dog;
                color.a = 1.0f;
                boneObject.GetComponent<SpriteRenderer>().color = color;
            }
        }
    }

    
}
