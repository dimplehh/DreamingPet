using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    int health;
    [SerializeField]
    Sprite[] sprites;
    [SerializeField]
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
        //if (collision.gameObject.tag == "Border")
        //    gameObject.SetActive(false);
        if (collision.gameObject.tag == "Player")//�÷��̾� ����� ��
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().UpdateLife(--collision.gameObject.GetComponent<Player>().life);
            if (collision.gameObject.GetComponent<Player>().life <= 0)
            {
                OnHit(collision);//������ �ֱ�
                Destroy(GameObject.FindGameObjectsWithTag("Bone")[0]);//���� �����
            }
            else
            {
                //피격 후 무적
                StartCoroutine(InvicibleTime(collision));
            }
        }
    }
    IEnumerator InvicibleTime(Collider2D collision)
    {
        collision.gameObject.layer = 7;
        collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(1f);
        collision.gameObject.layer = 0;
        collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
    
}
