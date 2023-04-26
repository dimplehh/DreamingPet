using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveBone : MonoBehaviour
{
    public float speed = 10f;
    Vector2 mousePos, transPos, targetPos;
    [SerializeField] Transform Panel;

    private void Start()
    {
        Panel = GameObject.Find("Canvas").transform.Find("PausePanel");
    }
    void Update()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            CalTargetPos();
        }
        MoveToTarget();
    }

    void CalTargetPos(){
        mousePos = Input.mousePosition; 
        transPos = Camera.main.ScreenToWorldPoint(mousePos);
        if (Panel.gameObject.activeSelf == false)
        {
            if ((transPos.y < 3.0 && transPos.y > -5.0)&&(transPos.x > -2.8 && transPos.x<2.8))
            {
                targetPos = new Vector2(transPos.x, transPos.y);
            }
        }

    }
    
    void MoveToTarget(){
        transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
    }
}
