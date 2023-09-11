using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public int score;
    public int coin;
    public int life;
    public int feverScore;
    public int shieldScore;
    public bool isSlow = false;
    [SerializeField]
    GameObject Touch;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ScoreUpdate");
        coin = 0;
    }
    
    /* 시간의 흐름에 따른 점수 증가 */
    IEnumerator ScoreUpdate(){
        while(true){
            yield return new WaitForSeconds(1.0f);
            if(Touch.activeSelf == false)
                score += (int)(Time.smoothDeltaTime * 1.5 * 1000);
            //기존 score는 Time.deltaTime * GetComponent<MovePet>().speed * 1000로 player의 속도에 따라 증감한다.
            //Test용으로 score의 변화는 일정하게 잠시 바꿔 놓았다.
        }
    }

    IEnumerator InvicibleTime()
    {
        gameObject.layer = 7;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(3f);
        gameObject.layer = 0;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
    public void StartScore()
    {
        StartCoroutine("ScoreUpdate");
    }
    public void StartInvicible()
    {
        StartCoroutine(InvicibleTime());
    }

}
