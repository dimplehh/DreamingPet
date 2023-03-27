using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fever : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {//GameObject.Find ���� ����
            GameObject.Find("GameManager").GetComponent<GameManager>().UpdateFeverScore(++collision.gameObject.GetComponent<Player>().feverScore);
            gameObject.SetActive(false);
        }
    }
}
