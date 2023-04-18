using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    string[] enemyObjs;
    [SerializeField]
    string[] feverObjs;
    [SerializeField]
    string[] rainObjs;
    [SerializeField]
    string[] spaceshipObjs;
    [SerializeField]
    string[] heartObjs;
    [SerializeField]
    string[] shieldpieceObjs;
    [SerializeField]
    Transform[] spawnPoints;
    [SerializeField]
    Transform[] spacespawnPoints;
    [SerializeField]
    ObjectManager objectManager;
    [SerializeField]
    GameObject warnImage;
    [SerializeField]
    GameObject surpriseImage;
    public AudioClip[] bgList;

    public bool stop;
    int enemyCnt = 0;
    float[] t;
    float[] maxSpawnDelay;
    float[] speed;
    float maxT;

    float curSpawnDelay;
    int i;
    int level;

    float realSpawnDelay;
    void Awake()
    {
        enemyObjs = new string[] { "enemy" };
        feverObjs = new string[] { "fever" };
        rainObjs = new string[] { "rain" };
        spaceshipObjs = new string[] { "spaceship" };
        heartObjs = new string[] { "heart" };
        shieldpieceObjs = new string[] { "shieldpiece" };
    }
    void Start()
    {//Level Design
        t = new float[8] { 0.0f, 5.0f, 15.0f, 30.0f, 30.0f, 40.0f, 40.0f, 40.0f };
        maxSpawnDelay = new float[8] { 5f, 4f, 3f, 2f, 1f, 1f, 0.8f, 0.6f };
        speed = new float[8] { 2.4f, 2.7f, 3.0f, 3.3f, 3.6f, 3.9f, 4.2f, 4.5f };

        maxT = t[t.Length - 1];
        level = 0;
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
            if (GetComponent<GameManager>().feverState) realSpawnDelay = 0.6f;
            else realSpawnDelay = maxSpawnDelay[i];

            if (curSpawnDelay > realSpawnDelay)
            {
                enemyCnt++;
                if (enemyCnt % 3 == 0 && GetComponent<GameManager>().feverState) SpawnShieldPiece();
                else if (enemyCnt % 5 == 0 && !GetComponent<GameManager>().feverState) SpawnRain();
                else if (enemyCnt % 8 == 0 && !GetComponent<GameManager>().feverState) SpawnSpaceShip();
                else if (enemyCnt % 6 == 0 && !GetComponent<GameManager>().feverState) SpawnFever();
                else if (enemyCnt % 11 == 0 && !GetComponent<GameManager>().feverState) SpawnHeart();
                else SpawnEnemy();

                curSpawnDelay = 0;
            }

            DeleteRain();
            DeleteEnemy();
            DeleteFever();
            DeleteSpaceShip();
            DeleteHeart();
            DeleteShieldPiece();
        }
        else
        {
            if (i < t.Length - 1)
                i++;
            else
            {
                t[i] = maxT;
                speed[i] += 0.3f;
            }
            level++;
            Debug.Log("Level:" + level + " (Time:" + t[i] + " / SpawnDelay:" + maxSpawnDelay[i] + " / Speed:" + speed[i] + ")");
        }
    }

    public void DeleteShieldPiece()
    {
        int ranEnemy = 0;
        objectManager.DelObj(shieldpieceObjs[ranEnemy]);
    }

    public void SpawnShieldPiece()
    {
        int ranEnemy = 0;
        int ranPoint = Random.Range(0, spawnPoints.Length);
        GameObject shieldPiece = objectManager.MakeObj(shieldpieceObjs[ranEnemy]);
        shieldPiece.transform.position = spawnPoints[ranPoint].position;

        Rigidbody2D rigid = shieldPiece.GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.up * speed[i];
    }

    public void StopShieldPiece()
    {
        GameObject[] targetPool = objectManager.GetTargetPool(shieldpieceObjs[0]);
        for (int index = 0; index < targetPool.Length; index++)
        {
            if (stop)
                targetPool[index].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            else
                targetPool[index].GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 2f);
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
        fever.transform.position = spawnPoints[ranPoint].position;

        Rigidbody2D rigid = fever.GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.up * speed[i];
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
        int ranPoint2 = Random.Range(0, spawnPoints.Length);

        GameObject enemy = objectManager.MakeObj(enemyObjs[ranEnemy]);
        enemy.transform.position = spawnPoints[ranPoint].position;
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        if(!GetComponent<GameManager>().feverState)
        {
            int a = Random.Range(0, 6);
            if (a == 4) rigid.velocity = Vector2.up * speed[i] * 1.5f;//일정 확률로 빠른 구름 생성
            else
                rigid.velocity = Vector2.up * speed[i];
            if (a == 5) StartCoroutine(InvicibleTime(enemy));//일정 확률로 사라졌다가 나타나는 구름 생성
        }
        else
        {
            rigid.velocity = Vector2.up * speed[i];
            int ranPoint3 = Random.Range(0, spawnPoints.Length);
            if (ranPoint3 != ranPoint && ranPoint3 != ranPoint2)
            {
                GameObject enemy3 = objectManager.MakeObj(enemyObjs[ranEnemy]);
                enemy3.transform.position = spawnPoints[ranPoint3].position;
                Rigidbody2D rigid3 = enemy3.GetComponent<Rigidbody2D>();
                rigid3.velocity = Vector2.up * speed[i];
            }
        }

        if (Mathf.Abs(ranPoint - ranPoint2) >= 3)
        {
            GameObject enemy2 = objectManager.MakeObj(enemyObjs[ranEnemy]);
            enemy2.transform.position = spawnPoints[ranPoint2].position;
            Rigidbody2D rigid2 = enemy2.GetComponent<Rigidbody2D>();
            rigid2.velocity = Vector2.up * speed[i];
        }
    }
    IEnumerator InvicibleTime(GameObject gm)
    {
        float a = 1f;
        while (a > 0f)
        {
            a -= Time.deltaTime / 1;
            gm.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, a);
            yield return null;
        }
        gm.layer = 7;
        yield return new WaitForSeconds(1f);
        gm.layer = 3;
        while (a < 1f)
        {
            a += Time.deltaTime / 1;
            gm.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, a);
            yield return null;
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
                targetPool[index].GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 2f);
        }
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

        AudioSource audioSource = rain.GetComponent<AudioSource>();
        if (GetComponent<GameManager>().soundManager2.soundOn)
            audioSource.Play();
        //GetComponent<GameManager>().EffectSoundPlay(bgList[1]);
        Rigidbody2D rigid = rain.GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.up * speed[i];

    }

    public void StopRain()
    {
        GameObject[] targetPool = objectManager.GetTargetPool(rainObjs[0]);
        for (int index = 0; index < targetPool.Length; index++)
        {
            if (stop)
            {
                targetPool[index].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                if (GetComponent<GameManager>().soundManager2.soundOn)
                    targetPool[index].GetComponent<AudioSource>().Stop();
            }
            else
            {
                targetPool[index].GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 2f);
            }
        }
    }

    public void DeleteSpaceShip()
    {
        int ranEnemy = 0;
        objectManager.DelObj(spaceshipObjs[ranEnemy]);
    }

    public void SpawnSpaceShip()
    {
        StartCoroutine(startWarning());
    }

    IEnumerator startWarning()
    {
        int ranEnemy = 0;
        int ranPoint = Random.Range(0, spacespawnPoints.Length);
        GameObject spaceship = objectManager.MakeObj(spaceshipObjs[ranEnemy]);
        spaceship.transform.position = spacespawnPoints[ranPoint].position;
        GetComponent<GameManager>().EffectSoundPlay(bgList[2]);
        warnImage.transform.position = new Vector3(0.0f, spaceship.transform.position.y,0.0f);
        if (0 <= ranPoint && ranPoint < 3)
        {
            warnImage.transform.rotation = Quaternion.Euler(0,0,0);
            surpriseImage.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            warnImage.transform.rotation = Quaternion.Euler(0, 0, 180);
            surpriseImage.transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        warnImage.SetActive(true);
        yield return new WaitForSeconds(2f);
        warnImage.SetActive(false);
        GetComponent<GameManager>().EffectSoundPlay(bgList[0]);
        Rigidbody2D rigid = spaceship.GetComponent<Rigidbody2D>();
        if (spaceship.transform.position.x > 0)
            rigid.velocity = Vector2.left * speed[i];
        else
            rigid.velocity = Vector2.right * speed[i];
    }

    public void StopSpaceShip()
    {
        GameObject[] targetPool = objectManager.GetTargetPool(spaceshipObjs[0]);
        for (int index = 0; index < targetPool.Length; index++)
        {
            if (stop)
                targetPool[index].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            else
                targetPool[index].GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 2f);
        }
    }

    public void DeleteHeart()
    {
        int ranEnemy = 0;
        objectManager.DelObj(heartObjs[ranEnemy]);
    }

    public void SpawnHeart()
    {
        int ranEnemy = 0;
        int ranPoint = Random.Range(0, spawnPoints.Length);
        GameObject heart = objectManager.MakeObj(heartObjs[ranEnemy]);
        heart.transform.position = spawnPoints[ranPoint].position;

        Rigidbody2D rigid = heart.GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.up * speed[i];
    }

    public void StopHeart()
    {
        GameObject[] targetPool = objectManager.GetTargetPool(heartObjs[0]);
        for (int index = 0; index < targetPool.Length; index++)
        {
            if (stop)
                targetPool[index].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            else
                targetPool[index].GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 2f);
        }
    }
}
