using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPiece : MonoBehaviour
{
    public AudioClip effectSound;

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (collision.gameObject.tag == "Player")
        {
            gm.EffectSoundPlay(effectSound);
            gm.UpdateShieldScore(++collision.gameObject.GetComponent<Player>().shieldScore);
            gameObject.SetActive(false);
        }
    }
}
