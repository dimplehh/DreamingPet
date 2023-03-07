using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager:MonoBehaviour
{
    [SerializeField]
    string[] enemyObjs;
    [SerializeField]
    Transform[] spawnPoints;
    [SerializeField]
    float maxSpawnDelay;
    [SerializeField]
    float curSpawnDelay;

    [SerializeField]
    float speed;
    [SerializeField]
    ObjectManager objectManager;
    public float score;
    public int life;
    public Image[] lifeImage;
    [SerializeField]
    GameObject Panel;
    [SerializeField]
    GameObject Back;

    GameObject enemy;

    public void gamePause(float timescale)
    {
        Time.timeScale = timescale; // 게임 시간 일시 정지
    }

    public GameObject Spawn(string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);
        return go;
    }

    public float Score(float score)
    {
        return score;
    }
    public int Life(int life)
    {
        return life;
    }
    void Awake()
    {
        enemyObjs = new string[] {"enemy"};
    }

    void Start()
    {
        Managers mg = Managers.Instance;///이걸 나중에 사용할 수 있을 것(싱글톤 클래스)- 코드 깔끔히 하는 용  
    }

    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > maxSpawnDelay) //소환할 때 됐다.
        {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.5f, 3f);
            curSpawnDelay = 0;
        }
        DeleteEnemy();
    }

    void DeleteEnemy()
    {
        int ranEnemy = 0;//나중에 장애물 여러개 생기면 Random.Range(0,3);이런식으로 바꾸기
        int ranPoint = Random.Range(0, spawnPoints.Length);//소환될 위치
        objectManager.DelObj(enemyObjs[ranEnemy]);
    }

    void SpawnEnemy()
    {
        int ranEnemy = 0;//나중에 장애물 여러개 생기면 Random.Range(0,3);이런식으로 바꾸기
        int ranPoint = Random.Range(0, spawnPoints.Length);//소환될 위치
        GameObject enemy = objectManager.MakeObj(enemyObjs[ranEnemy]);
        enemy.transform.position = spawnPoints[ranPoint].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.up * speed;
    }

    /* 플레이어의 목숨 업데이트 */
    public void UpdateLife(int curlife){
        /*life 초기화*/
        for(int i = 0; i < 5; i++){
            lifeImage[i].color = new Color(1,1,1,0);
        }
        /*life 적용*/
        for(int i = 0; i < curlife; i++){
            lifeImage[i].color = new Color(1,1,1,1);
        }
        if (curlife <= 0)
        {
            Panel.SetActive(true);
            Back.GetComponent<Background>().enabled = false;
        }
    }

    //프로토타입 캐릭터 삭제에 사용된다 추후 게임에서는 사용되지 않는다.
    public void DestroyPlayer()
    {
        if (GameObject.FindWithTag("Player") == true)
        {
            Destroy(GameObject.FindWithTag("Player"));
            Destroy(GameObject.FindWithTag("Bone"));
        }
        else
        {
            GameObject.Find("@Scene").GetComponent<GameScene>().player = Spawn("dog");
            Spawn("bone");
            UpdateLife(5);
            GameObject.Find("PlayerSpeedSlider").GetComponent<PlayerSpeed>().speedSlider.value = 1f;
        }
    }

    
}
