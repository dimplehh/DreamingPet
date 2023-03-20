using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBone : MonoBehaviour
{
    public float speed = 10f;
    Vector2 mousePos, transPos, targetPos;
    [SerializeField] Transform Panel;

    private void Start()
    {
        Panel = GameObject.Find("Canvas").transform.Find("Panel");
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
            CalTargetPos();
        MoveToTarget();
    }

    void CalTargetPos(){
        mousePos = Input.mousePosition; 
        transPos = Camera.main.ScreenToWorldPoint(mousePos);
        if(transPos.y<3&&Panel.gameObject.activeSelf==false)
            targetPos = new Vector2(transPos.x, transPos.y);
        
    }
    
    void MoveToTarget(){
        transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
    }
}
