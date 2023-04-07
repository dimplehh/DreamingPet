using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fever : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites;
    public AudioClip effectSound;

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (collision.gameObject.tag == "Player")
        {//GameObject.Find 차후 수정
            gm.EffectSoundPlay(effectSound);
            gm.UpdateFeverScore(++collision.gameObject.GetComponent<Player>().feverScore);
            gameObject.SetActive(false);
        }
    }
}
