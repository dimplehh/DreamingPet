using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    string[] enemyObjs;
    [SerializeField]
    Transform[] spawnPoints;
    [SerializeField]
    ObjectManager objectManager;
    [SerializeField]
    GameObject Panel;
    [SerializeField]
    bool stop;

    float[] t;
    float[] maxSpawnDelay;
    float[] speed;

    float curSpawnDelay;
    int i;
    void Awake()
    {
        enemyObjs = new string[] { "enemy" };
    }
    void Start()
    {//Level Design
        i = 0;
        t = new float[6] { 15.0f , 20.0f, 30.0f, 40.0f, 50.0f, 60.0f};
        maxSpawnDelay = new float[6] { 5f, 4f, 4f, 3f, 2f, 2f };
        speed = new float[6] { 2.0f, 2.2f, 2.2f, 2.4f, 2.4f, 2.6f };
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
            curSpawnDelay += Time.deltaTime;
        else
            curSpawnDelay = 0;


        if (Panel.activeSelf)
        {
            stop = true;
            StopEnemy();
        }
        else
        {
            if (t[i] > 0)
            {
                t[i] -= Time.deltaTime;
                if (curSpawnDelay > maxSpawnDelay[i])
                {
                    SpawnEnemy();
                    curSpawnDelay = 0;
                }
                DeleteEnemy();
            }
            else
            {
                i++;
                Debug.Log("Level:" + i);
            }
        }
    }

    public void StopEnemy()
    {
        GameObject[] targetPool = objectManager.GetTargetPool(enemyObjs[0]);
        for (int index = 0; index < targetPool.Length; index++)
        {
            if (stop)
                targetPool[index].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            else
                targetPool[index].GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 2f);//프로토타입 일시정지 해{
        }
    }

    public void DeleteEnemy()
    {
        int ranEnemy = 0;//나중에 장애물 여러개 생기면 Random.Range(0,3);이런식으로 바꾸기
        objectManager.DelObj(enemyObjs[ranEnemy]);
    }

    public void SpawnEnemy()
    {
        int ranEnemy = 0;//나중에 장애물 여러개 생기면 Random.Range(0,3);이런식으로 바꾸기
        int ranPoint = Random.Range(0, spawnPoints.Length);//소환될 위치 //2.ranPoint 2개 받기
        GameObject enemy = objectManager.MakeObj(enemyObjs[ranEnemy]);
        enemy.transform.position = spawnPoints[ranPoint].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.up * speed[i]; //3.speed 변경
    }
}
