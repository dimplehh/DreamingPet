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
    Vector2 vector;

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
        if (collision.gameObject.tag == "Player" && !(GameObject.Find("GameManager").GetComponent<GameManager>().feverState))
        {//find 함수 나중에 고치기
            GameObject.Find("GameManager").GetComponent<GameManager>().UpdateLife(--collision.gameObject.GetComponent<Player>().life);
            if (collision.gameObject.GetComponent<Player>().life <= 0)
            {
                collision.gameObject.SetActive(false);
                GameObject.FindGameObjectsWithTag("Bone")[0].SetActive(false);
                
            }
            else
            {
                //피격 후 무적
                StartCoroutine(InvicibleTime(collision));
            }
        }
        else if(collision.gameObject.tag == "Player" && GameObject.Find("GameManager").GetComponent<GameManager>().feverState)
        {
            Vector2 vectA = new Vector2(transform.position.x - collision.transform.position.x, transform.position.y - collision.transform.position.y);
            gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity * 2 + vectA;
            collision.GetComponent<Player>().score += 50;
            StartCoroutine(DestroyEnemy(collision));
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
    
    IEnumerator DestroyEnemy(Collider2D collision)
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
