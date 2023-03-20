using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightning : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !(GameObject.Find("GameManager").GetComponent<GameManager>().feverState))
        {
            collision.GetComponent<MovePet>().SlowSpeed();
            collision.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !(GameObject.Find("GameManager").GetComponent<GameManager>().feverState))
        {
            collision.GetComponent<MovePet>().ResetSpeed();
            collision.transform.GetChild(1).GetComponent<ParticleSystem>().Stop();
        }
    }
}

