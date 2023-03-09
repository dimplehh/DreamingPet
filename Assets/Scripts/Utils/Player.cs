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
            score += (int)(Time.deltaTime * 1.5 * 1000);
            //기존 score는 Time.deltaTime * GetComponent<MovePet>().speed * 1000로 player의 속도에 따라 증감한다.
            //Test용으로 score의 변화는 일정하게 잠시 바꿔 놓았다.
        }
    }
}