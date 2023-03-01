using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePet : MonoBehaviour
{
    Transform bone;
    public float speed;
    [SerializeField] float distanceX, distanceY;
    void Start(){
        bone = GameObject.Find("bone").transform;
        speed = GameObject.Find("bone").GetComponent<MoveBone>().speed*0.15f;
        
    }
    void Update(){
        distanceX = transform.position.x-bone.position.x;
        distanceY = transform.position.y-bone.position.y;
        if(distanceX>0.1){
            transform.Translate(new Vector2(-distanceX,0)*Time.deltaTime*speed);
            transform.eulerAngles = new Vector3(0,0,0);
        }else if (distanceX<-0.1){
            transform.Translate(new Vector2(distanceX,0)*Time.deltaTime*speed);
            transform.eulerAngles = new Vector3(0,180,0);
        }

        if(distanceY>0.1){
            transform.Translate(new Vector2(0,-distanceY)*Time.deltaTime*speed);
        }else if (distanceY<-0.1){
            transform.Translate(new Vector2(0,-distanceY)*Time.deltaTime*speed);
        }

    }

    
}
