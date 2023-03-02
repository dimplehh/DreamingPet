using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    public GameObject Spawn(string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);
        return go;
    }

    public float Score(float score)
    {
        return score;
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
}
