using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField]
    AudioClip effectSound;
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        if (collision.gameObject.tag == "Player" && !(gm.feverState))
        {
            gm.soundManager2.EffectSoundPlay(effectSound);
            if (collision.gameObject.GetComponent<Player>().life < 3)
            {
                gm.UpdateLife(++collision.gameObject.GetComponent<Player>().life);
            }
            gameObject.SetActive(false);
        }
    }
}
