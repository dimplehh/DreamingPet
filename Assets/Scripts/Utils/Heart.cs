using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !(GameObject.Find("GameManager").GetComponent<GameManager>().feverState))
        {
            if (collision.gameObject.GetComponent<Player>().life < 3)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().UpdateLife(++collision.gameObject.GetComponent<Player>().life);
            }
            gameObject.SetActive(false);
        }
    }
}
