using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    private Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            animator = collision.GetComponent<Animator>();
            animator.SetBool("rain", true);
            collision.gameObject.GetComponent<MovePet>().speed *= 0.5f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator = collision.GetComponent<Animator>();
            animator.SetBool("rain", false);
            collision.gameObject.GetComponent<MovePet>().speed *= 2f;
        }
    }
}
