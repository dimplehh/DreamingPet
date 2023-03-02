using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    int health;
    [SerializeField]
    Sprite[] sprites;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnHit(Collider2D collision)//구름에 부딪혔을 때
    {
        Destroy(collision.gameObject);//강아지 주금
        
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
            gameObject.SetActive(false);
        else if (collision.gameObject.tag == "Player")//플레이어 닿았을 때
        {

            if(--collision.gameObject.GetComponent<Player>().life<=0){
                OnHit(collision);//강아지 주금
                Destroy(GameObject.FindGameObjectsWithTag("Bone")[0]);//간식 사라짐
            }

            GameObject.Find("GameManager").GetComponent<GameManager>().UpdateLife(collision.gameObject.GetComponent<Player>().life);
        }
        
    }
}
