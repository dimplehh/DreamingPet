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

    public GameObject Spawn(string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);
        return go;
    }

    void Awake()
    {
        enemyObjs = new string[] {"enemy"};
    }

    void Start()
    {
        Managers mg = Managers.Instance;//�̰� ���߿� ����� �� ���� ��(�̱��� Ŭ����)- �ڵ� ����� �ϴ� ��    
    }

    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > maxSpawnDelay) //��ȯ�� �� �ƴ�.
        {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.5f, 3f);
            curSpawnDelay = 0;
        }
    }

    void SpawnEnemy()
    {
        int ranEnemy = 0;//���߿� ��ֹ� ������ ����� Random.Range(0,3);�̷������� �ٲٱ�
        int ranPoint = Random.Range(0, spawnPoints.Length);//��ȯ�� ��ġ
        GameObject enemy = objectManager.MakeObj(enemyObjs[ranEnemy]);
        enemy.transform.position = spawnPoints[ranPoint].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.up * speed;
    }
}
