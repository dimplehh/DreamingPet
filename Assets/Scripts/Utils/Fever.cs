using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fever : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites;

    void Awake()
    {
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {//GameObject.Find 차후 수정
            Debug.Log("플레이어피버스코어:" + collision.gameObject.GetComponent<Player>().feverScore);
            GameObject.Find("GameManager").GetComponent<GameManager>().UpdateFeverScore(++collision.gameObject.GetComponent<Player>().feverScore);
            Debug.Log("feverScore:" + collision.gameObject.GetComponent<Player>().feverScore);
            gameObject.SetActive(false);
        }
    }
}
