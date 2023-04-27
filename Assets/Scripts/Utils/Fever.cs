using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fever : MonoBehaviour
{
    public AudioClip effectSound;

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        if (collision.gameObject.tag == "Player")
        {
            gm.soundManager2.EffectSoundPlay(effectSound);
            gm.UpdateFeverScore(++collision.gameObject.GetComponent<Player>().feverScore);
            gameObject.SetActive(false);
        }
    }
}
