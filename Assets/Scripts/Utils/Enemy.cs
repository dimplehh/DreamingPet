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
    private Animator animator;
    [SerializeField]
    AudioClip[] bgList;

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
        GameManager gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        if (collision.gameObject.tag == "Player" && !(gm.feverState))
        {
            if (gm.shieldState)
            {
                gm.soundManager2.EffectSoundPlay(bgList[3]);
                collision.gameObject.transform.Find("bubble").gameObject.SetActive(false);
                gm.shieldState = false;
            }
            else
            {
                gm.soundManager2.EffectSoundPlay(bgList[0]);
                gm.UpdateLife(--collision.gameObject.GetComponent<Player>().life);
                collision.gameObject.layer = 0;
            }
            if (collision.gameObject.GetComponent<Player>().life <= 0)
            {
                collision.gameObject.SetActive(false);
                GameObject.FindGameObjectsWithTag("Bone")[0].SetActive(false);
                gm.soundManager2.EffectSoundPlay(bgList[1]);
            }
            else
            {
                //피격 후 무적
                animator = collision.GetComponent<Animator>();
                animator.SetTrigger("hit");
                StartCoroutine(InvicibleTime(collision));
            }
        }
        else if(collision.gameObject.tag == "Player" && gm.feverState)
        {
            if(collision.gameObject.layer == 7)
            {
                collision.gameObject.layer = 0;
                collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
            gm.soundManager2.EffectSoundPlay(bgList[2]);
            Vector2 vectA = new Vector2(transform.position.x - collision.transform.position.x, transform.position.y - collision.transform.position.y);
            gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity * 5 + 5 * vectA;
            gameObject.GetComponent<Rotate>().time = 0;
            StartCoroutine(DestroyEnemy(collision));
        }
    }
    IEnumerator InvicibleTime(Collider2D collision)
    {
        collision.gameObject.layer = 7;

        for (int i = 0; i < 3; i++)
        {
            if (i % 2 == 0)
                collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.7f);
            else
                collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.3f);
        }
        collision.gameObject.layer = 0;
        collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
    
    IEnumerator DestroyEnemy(Collider2D collision)
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        collision.GetComponent<Player>().score += 50;
    }
}
