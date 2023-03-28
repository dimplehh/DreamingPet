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
    private float maxAngle = 30f;
    private Vector3 moveDirection;
    private Animator animator;

    void Start(){
        boneObject = GameObject.Find("bone");
        bone = boneObject.transform;
        speed = boneObject.GetComponent<MoveBone>().speed * 0.4f;
        color = boneObject.GetComponent<SpriteRenderer>().color;
        animator = GetComponent<Animator>();
    }
    void Update(){
        moveDirection = transform.position - bone.position;
        distanceX = moveDirection.x;
        distanceY = moveDirection.y;
        if (distanceX != 0 || distanceY != 0)
        {
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
        }
        float angle = Vector3.Angle(moveDirection, Vector3.up);
        angle = Mathf.Abs(90f - angle);

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (distanceX > 0)
            {
                if (angle > maxAngle)
                {
                    if (distanceY > 0.2)
                    {
                        transform.eulerAngles = new Vector3(0, 0, maxAngle);
                    }
                    else if (distanceY < -0.2)
                    {
                        transform.eulerAngles = new Vector3(0, 0, -maxAngle);
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                }
                else
                {
                    if (distanceY > 0.2)
                    {
                        transform.eulerAngles = new Vector3(0, 0, angle);
                    }
                    else if (distanceY < -0.2)
                    {
                        transform.eulerAngles = new Vector3(0, 0, -angle);
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                    }

                }
                transform.GetChild(1).eulerAngles = new Vector3(0, 0, 0);
            }
            else if (distanceX < 0)
            {
                if (angle > maxAngle)
                {
                    if (distanceY > 0.2)
                    {
                        transform.eulerAngles = new Vector3(0, 180, maxAngle);
                    }
                    else if (distanceY < -0.2)
                    {
                        transform.eulerAngles = new Vector3(0, 180, -maxAngle);
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0, 180, 0);
                    }

                }
                else
                {
                    if (distanceY > 0.2)
                    {
                        transform.eulerAngles = new Vector3(0, 180, angle);
                    }
                    else if (distanceY < -0.2)
                    {
                        transform.eulerAngles = new Vector3(0, 180, -angle);
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0, 180, 0);

                    }

                }
                transform.GetChild(1).eulerAngles = new Vector3(0, 180, 0);
            }



            transform.position = Vector2.MoveTowards(transform.position, bone.position, Time.smoothDeltaTime * speed);
            





            if (Mathf.Abs(distanceX) < 0.1 && Mathf.Abs(distanceY) < 0.1)
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
