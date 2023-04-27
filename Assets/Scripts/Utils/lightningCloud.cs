using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightningCloud : MonoBehaviour
{
    //흠 왜 안될...?
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && GameObject.Find("GameManager").GetComponent<GameManager>().feverState)
        {
            Vector2 vectA = new Vector2(transform.position.x - collision.transform.position.x, transform.position.y - collision.transform.position.y);
            transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity = transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity + 3 * vectA;
            collision.GetComponent<Player>().score += 50;
            StartCoroutine(DestroyEnemy(collision));
        }
    }
    IEnumerator DestroyEnemy(Collider2D collision)
    {
        yield return new WaitForSeconds(1f);
        transform.parent.gameObject.SetActive(false);
    }
}
