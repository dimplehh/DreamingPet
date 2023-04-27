using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPiece : MonoBehaviour
{
    [SerializeField]
    AudioClip audioclip;
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        if (collision.gameObject.tag == "Player")
        {
            gm.soundManager2.EffectSoundPlay(audioclip);
            gm.UpdateShieldScore(++collision.gameObject.GetComponent<Player>().shieldScore);
            gameObject.SetActive(false);
        }
    }
}
