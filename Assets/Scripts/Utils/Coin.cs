using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    AudioClip effectSound;
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        if (collision.gameObject.tag == "Player" && !(gm.feverState))
        {
            gm.soundManager2.EffectSoundPlay(effectSound);
            gameObject.SetActive(false);
            gm.UpdateCoin(10);
        }
    }
}
