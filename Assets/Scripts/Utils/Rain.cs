using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    public float rainSlow = 0.5f;
    public float slowTime = 3.0f;
    
    IEnumerator ResetSpeed(GameObject player)
    {   
        yield return new WaitForSeconds(slowTime);
        player.GetComponent<MovePet>().speed /= rainSlow;
        player.GetComponent<Player>().isSlow = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player")&&other.GetComponent<Player>().isSlow==false)
        {
            other.GetComponent<MovePet>().speed *= rainSlow;
            other.GetComponent<Player>().isSlow = true;
            StartCoroutine(ResetSpeed(other));
        }
    
        
    }


}
