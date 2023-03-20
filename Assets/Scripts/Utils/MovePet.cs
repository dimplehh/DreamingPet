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
        speed = boneObject.GetComponent<MoveBone>().speed * 0.4f;
        color = boneObject.GetComponent<SpriteRenderer>().color;
    }
    void Update(){
        distanceX = transform.position.x - bone.position.x;
        distanceY = transform.position.y - bone.position.y;
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // 기존 간식과의 거리에 비례하여 속도가 조절되던 것을 거리 상관없이 일정하게 바꾸었다.
            if (distanceX > 0.1)
            {
                //transform.Translate(new Vector2(-distanceX, 0) * Time.deltaTime * speed);
                transform.eulerAngles = new Vector3(0, 0, 0);
                transform.GetChild(1).eulerAngles = new Vector3(0, 0, 0);
            }
            else if (distanceX < -0.1)
            {
                //transform.Translate(new Vector2(distanceX, 0) * Time.deltaTime * speed);
                transform.eulerAngles = new Vector3(0, 180, 0);
                transform.GetChild(1).eulerAngles = new Vector3(0, 180, 0);
            }
            transform.position = Vector2.MoveTowards(transform.position, bone.position, Time.deltaTime * speed);

            /*
            if (distanceY > 0.1)
            {
                transform.Translate(new Vector2(0, -distanceY) * Time.deltaTime * speed);
            }
            else if (distanceY < -0.1)
            {
                transform.Translate(new Vector2(0, -distanceY) * Time.deltaTime * speed);
            }
            */

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

    public void ResetSpeed()
    {
        speed = boneObject.GetComponent<MoveBone>().speed * 0.4f;
    }

    public void SlowSpeed()
    {
        speed = boneObject.GetComponent<MoveBone>().speed * 0.15f;
    }

    

}
