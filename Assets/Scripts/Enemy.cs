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

    void OnHit(Collider2D collision)//������ �ε����� ��
    {
        Destroy(collision.gameObject);//������ �ֱ�
        
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
            gameObject.SetActive(false);
        else if (collision.gameObject.tag == "Player")//�÷��̾� ����� ��
        {

            if(--collision.gameObject.GetComponent<Player>().life<=0){
                OnHit(collision);//������ �ֱ�
                Destroy(GameObject.FindGameObjectsWithTag("Bone")[0]);//���� �����
            }

            GameObject.Find("GameManager").GetComponent<GameManager>().UpdateLife(collision.gameObject.GetComponent<Player>().life);
        }
        
    }
}
