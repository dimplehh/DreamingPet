using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    string[] enemyObjs;
    [SerializeField]
    string[] feverObjs;
    [SerializeField]
    string[] rainObjs;
    [SerializeField]
    Transform[] spawnPoints;
    [SerializeField]
    ObjectManager objectManager;
    public bool stop;
    int enemyCnt=0;
    float[] t;
    float[] maxSpawnDelay;
    float[] speed;
    float maxT;

    float curSpawnDelay;
    int i;
    int level;
    int feverCycle;
    void Awake()
    {
        enemyObjs = new string[] { "enemy" };
        feverObjs = new string[] { "fever" };
        rainObjs = new string[] { "rain" };
    }
    void Start()
    {//Level Design
        t = new float[6] { 10.0f, 20.0f, 30.0f, 40.0f, 50.0f, 60.0f };
        maxSpawnDelay = new float[6] { 5f, 4f, 3f, 2f, 1f, 1f };
        speed = new float[6] { 2.0f, 2.2f, 2.4f, 2.6f, 2.8f, 3.0f };

        maxT = t[t.Length - 1];
        level = 0;
        feverCycle = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
            curSpawnDelay += Time.deltaTime;
        else
            curSpawnDelay = 0;


        if (t[i] > 0)
        {
            t[i] -= Time.deltaTime;
            if (curSpawnDelay > maxSpawnDelay[i])
            {
                enemyCnt++;
                    
                if (enemyCnt % 5 == 0 && !GetComponent<GameManager>().feverState) SpawnRain();
                else SpawnEnemy();

                curSpawnDelay = 0;

                feverCycle -= 1;
                if(feverCycle == 0)
                {
                    if(!GetComponent<GameManager>().feverState)
                        SpawnFever();
                    feverCycle = 3;
                }
            }
            DeleteRain();
            DeleteEnemy();
            DeleteFever();
        }
        else
        {
            if (i < t.Length - 1)
                i++;
            else
            {
                t[i] = maxT;
                speed[i] += 0.2f;
            }
            level++;
            Debug.Log("Level:" + level + " (Time:" + t[i] + " / SpawnDelay:" + maxSpawnDelay[i] + " / Speed:" + speed[i] + ")");
        }
        
    }

    public void DeleteFever()
    {
        int ranEnemy = 0;
        objectManager.DelObj(feverObjs[ranEnemy]);
    }

    public void SpawnFever()
    {
        int ranEnemy = 0;
        int ranPoint = Random.Range(0, spawnPoints.Length);
        GameObject fever = objectManager.MakeObj(feverObjs[ranEnemy]);
        fever.transform.position = new Vector2(spawnPoints[ranPoint].position.x, spawnPoints[ranPoint].position.y - 3.0f);

        Rigidbody2D rigid = fever.GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.up * speed[i];
    }

    public void StopEnemy()
    {
        GameObject[] targetPool = objectManager.GetTargetPool(enemyObjs[0]);
        for (int index = 0; index < targetPool.Length; index++)
        {
            if (stop)
                targetPool[index].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            else
                targetPool[index].GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 2f);
        }
    }
    public void StopRain()
    {
        GameObject[] targetPool = objectManager.GetTargetPool(rainObjs[0]);
        for (int index = 0; index < targetPool.Length; index++)
        {
            if (stop)
            {
                targetPool[index].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                targetPool[index].transform.Find("Particle System").GetComponent<ParticleSystem>().Stop();
            }
            else
            {
                targetPool[index].GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 2f);
                targetPool[index].transform.Find("Particle System").GetComponent<ParticleSystem>().Play();
            }
        }
    }
    public void StopFever()
    {
        GameObject[] targetPool = objectManager.GetTargetPool(feverObjs[0]);
        for (int index = 0; index < targetPool.Length; index++)
        {
            if (stop)
                targetPool[index].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            else
                targetPool[index].GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 2f);
        }
    }

    public void DeleteEnemy()
    {
        int ranEnemy = 0;
        objectManager.DelObj(enemyObjs[ranEnemy]);
    }

    public void SpawnEnemy()
    {
        int ranEnemy = 0;
        int ranPoint = Random.Range(0, spawnPoints.Length);
        GameObject enemy = objectManager.MakeObj(enemyObjs[ranEnemy]);
        enemy.transform.position = spawnPoints[ranPoint].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.up * speed[i];
    }

    public void DeleteRain()
    {
        int ranEnemy = 0;
        objectManager.DelObj(rainObjs[ranEnemy]);
    }

    public void SpawnRain()
    {
        int ranEnemy = 0;
        int ranPoint = Random.Range(0, spawnPoints.Length);
        GameObject rain = objectManager.MakeObj(rainObjs[ranEnemy]);
        rain.transform.position = spawnPoints[ranPoint].position;

        Rigidbody2D rigid = rain.GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.up * speed[i];
        
    }
}
