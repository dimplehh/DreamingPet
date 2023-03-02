using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public int score;
    public int life;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ScoreUpdate");
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    /* 시간의 흐름에 따른 점수 증가 */
    IEnumerator ScoreUpdate(){
        while(true){
            yield return new WaitForSeconds(1.0f);
            score += (int)(Time.deltaTime * GetComponent<MovePet>().speed * 1000);
        }
    }
    
}
