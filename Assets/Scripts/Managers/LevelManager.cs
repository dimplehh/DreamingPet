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
    string[] coinObjs;
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
    [SerializeField]
    GameObject Touch;

    public bool stop;
    int enemyCnt = 0;
    float[] t;
    float[] maxSpawnDelay;
    float[] speed;
    float maxT;
    float otherSpawnDelay = 5f;
    float t2;

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
        coinObjs = new string[] { "coin" };
        shieldpieceObjs = new string[] { "shieldpiece" };

        if(PlayerPrefs.GetInt("toyOption") == 1)
        {
            otherSpawnDelay = 4f;
        }
    }
    void Start()
    {//Level Design
        t = new float[8] { 0.0f, 5.0f, 15.0f, 30.0f, 30.0f, 40.0f, 40.0f, 40.0f };
        maxSpawnDelay = new float[8] { 5f, 3f, 1.5f, 1f, 1f, 0.8f, 0.8f, 0.6f };
        speed = new float[8] { 2.4f, 2.7f, 3.0f, 3.3f, 3.6f, 3.9f, 4.2f, 4.5f };

        maxT = t[t.Length - 1];
        t2 = otherSpawnDelay;
        level = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Touch.activeSelf == true)
            return;
        if (!stop)
            curSpawnDelay += Time.deltaTime;
        else
            curSpawnDelay = 0;

        if(!stop)
        {
            if(t2 > 0)
            {
                t2 -= Time.deltaTime;
            }
            else
            {
                if (!GetComponent<GameManager>().feverState)
                {
                    int random;
                    if (PlayerPrefs.GetInt("toyOption") == 4)
                        random = Random.Range(0, 4);
                    else
                        random = Random.Range(0, 3);
                    if (random == 0)
                        SpawnFever();
                    else if (random == 1)
                        SpawnCoin();
                    else if (random == 2 || random == 3)
                        SpawnHeart();
                }
                t2 = otherSpawnDelay;
            }
            if (t[i] > 0)
            {
                t[i] -= Time.deltaTime;
                if (GetComponent<GameManager>().feverState) realSpawnDelay = 0.6f;
                else realSpawnDelay = maxSpawnDelay[i];

                if (curSpawnDelay > realSpawnDelay)
                {
                    enemyCnt++;
                    if (enemyCnt % 3 == 0 && GetComponent<GameManager>().feverState) SpawnShieldPiece();
                    if (enemyCnt % 9 == 0 && !GetComponent<GameManager>().feverState && level >=3) SpawnRain();
                    else if (enemyCnt % 13 == 0 && !GetComponent<GameManager>().feverState && level >= 4) SpawnSpaceShip();
                    else SpawnEnemy();

                    curSpawnDelay = 0;
                }

                DeleteRain();
                DeleteEnemy();
                DeleteFever();
                DeleteSpaceShip();
                DeleteHeart();
                DeleteCoin();
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
        rigid.velocity = speed[i] >= 5.5f ? Vector2.up * 5.5f : Vector2.up * speed[i];
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
    
    private Dictionary<GameObject, Vector2> feverVelocities = new Dictionary<GameObject, Vector2>();
    public void StopFever()
    {
        GameObject[] targetPool = objectManager.GetTargetPool(feverObjs[0]);
        for (int index = 0; index < targetPool.Length; index++)
        {
            GameObject feverObject = targetPool[index];
            Rigidbody2D feverRigidbody = feverObject.GetComponent<Rigidbody2D>();
            if (stop)
            {
                if(!feverVelocities.ContainsKey(feverObject))
                {
                    feverVelocities[feverObject] = feverRigidbody.velocity;
                }
                feverRigidbody.velocity = Vector2.zero;
            }
            else
            {
                if(feverVelocities.TryGetValue(feverObject, out Vector2 previousVelocity))
                {
                    feverRigidbody.velocity = previousVelocity;
                    feverVelocities.Remove(feverObject);
                }
            }
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
        rigid.velocity = (speed[i] >= 5.5f && GetComponent<GameManager>().feverState)  ? Vector2.up * 5.5f : Vector2.up * speed[i];
        if (!GetComponent<GameManager>().feverState)
        {
            int a = Random.Range(0, 10);
            if (a == 9 && level >=7) StartCoroutine(InvicibleTime(enemy));//일정 확률로 사라졌다가 나타나는 구름 생성
        }
        else
        {
            int ranPoint3 = Random.Range(0, spawnPoints.Length);
            if (ranPoint3 != ranPoint && ranPoint3 != ranPoint2)
            {
                GameObject enemy3 = objectManager.MakeObj(enemyObjs[ranEnemy]);
                enemy3.transform.position = spawnPoints[ranPoint3].position;
                Rigidbody2D rigid3 = enemy3.GetComponent<Rigidbody2D>();
                rigid3.velocity = (speed[i] >= 5.5f && GetComponent<GameManager>().feverState) ? Vector2.up * 5.5f : Vector2.up * speed[i];
            }
        }
        if (Mathf.Abs(ranPoint - ranPoint2) >= 3 && level >=5)
        {
            GameObject enemy2 = objectManager.MakeObj(enemyObjs[ranEnemy]);
            enemy2.transform.position = spawnPoints[ranPoint2].position;
            Rigidbody2D rigid2 = enemy2.GetComponent<Rigidbody2D>();
            rigid2.velocity = (speed[i] >= 5.5f && GetComponent<GameManager>().feverState) ? Vector2.up * 5.5f : Vector2.up * speed[i];
        }
        else if(Mathf.Abs(ranPoint - ranPoint2) >= 4 && level >= 3)
        {
            GameObject enemy2 = objectManager.MakeObj(enemyObjs[ranEnemy]);
            enemy2.transform.position = spawnPoints[ranPoint2].position;
            Rigidbody2D rigid2 = enemy2.GetComponent<Rigidbody2D>();
            rigid2.velocity = Vector2.up * speed[i];
            rigid2.velocity = (speed[i] >= 5.5f && GetComponent<GameManager>().feverState) ? Vector2.up * 5.5f : Vector2.up * speed[i];
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
    private Dictionary<GameObject, Vector2> enemyVelocities = new Dictionary<GameObject, Vector2>();
    public void StopEnemy()
    {
        GameObject[] targetPool = objectManager.GetTargetPool(enemyObjs[0]);
        for (int index = 0; index < targetPool.Length; index++)
        {
            GameObject enemyObject = targetPool[index];
            Rigidbody2D enemyRigidbody = enemyObject.GetComponent<Rigidbody2D>();

            if (stop)
            {
                if (!enemyVelocities.ContainsKey(enemyObject))
                {
                    enemyVelocities[enemyObject] = enemyRigidbody.velocity;
                }

                enemyRigidbody.velocity = Vector2.zero;
            }
            else
            {
                if (enemyVelocities.TryGetValue(enemyObject, out Vector2 previousVelocity))
                {
                    enemyRigidbody.velocity = previousVelocity;
                    enemyVelocities.Remove(enemyObject);
                }
            }
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
        Rigidbody2D rigid = rain.GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.up * speed[i];

    }

    private Dictionary<GameObject, Vector2> rainVelocities = new Dictionary<GameObject, Vector2>();
    private Dictionary<GameObject, bool> rainAudioStates = new Dictionary<GameObject, bool>();

    public void StopRain()
    {
        GameObject[] targetPool = objectManager.GetTargetPool(rainObjs[0]);
        for (int index = 0; index < targetPool.Length; index++)
        {
            GameObject rainObject = targetPool[index];
            Rigidbody2D rainRigidbody = rainObject.GetComponent<Rigidbody2D>();
            AudioSource rainAudio = rainObject.GetComponent<AudioSource>();

            if (stop)
            {
                if (!rainVelocities.ContainsKey(rainObject))
                {
                    rainVelocities[rainObject] = rainRigidbody.velocity;
                }
                if (!rainAudioStates.ContainsKey(rainObject))
                {
                    rainAudioStates[rainObject] = rainAudio.isPlaying;
                }

                rainRigidbody.velocity = Vector2.zero;
                if (GetComponent<GameManager>().soundManager2.soundOn)
                {
                    rainAudio.Stop();
                }
            }
            else
            {
                if (rainVelocities.TryGetValue(rainObject, out Vector2 previousVelocity))
                {
                    rainRigidbody.velocity = previousVelocity;
                    rainVelocities.Remove(rainObject);
                }
                if (rainAudioStates.TryGetValue(rainObject, out bool audioState) && audioState)
                {
                    rainAudio.Play();
                }
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
        if (GetComponent<GameManager>().soundManager2.soundOn)
            GetComponent<GameManager>().soundManager2.EffectSoundPlay(bgList[2]);
        yield return new WaitForSeconds(2f);
        warnImage.SetActive(false);
        GetComponent<GameManager>().soundManager2.EffectSoundPlay(bgList[0]);
        Rigidbody2D rigid = spaceship.GetComponent<Rigidbody2D>();
        if (spaceship.transform.position.x > 0)
            rigid.velocity = Vector2.left * speed[i];
        else
            rigid.velocity = Vector2.right * speed[i];
    }

    private Dictionary<GameObject, Vector2> SpaceshipVelocities = new Dictionary<GameObject, Vector2>();
    public void StopSpaceShip()
    {
        GameObject[] targetPool = objectManager.GetTargetPool(spaceshipObjs[0]);
        for (int index = 0; index < targetPool.Length; index++)
        {
            GameObject spaceshipObject = targetPool[index];
            Rigidbody2D spaceshipRigidbody = spaceshipObject.GetComponent<Rigidbody2D>();
            if (stop)
            {
                if (!SpaceshipVelocities.ContainsKey(spaceshipObject))
                {
                    SpaceshipVelocities[spaceshipObject] = spaceshipRigidbody.velocity;
                }
                spaceshipRigidbody.velocity = Vector2.zero;
            }
            else
            {
                if(SpaceshipVelocities.TryGetValue(spaceshipObject,out Vector2 previousVelocity))
                {
                    spaceshipRigidbody.velocity = previousVelocity;
                    SpaceshipVelocities.Remove(spaceshipObject);
                }
            }
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

    private Dictionary<GameObject, Vector2> heartVelocities = new Dictionary<GameObject, Vector2>();
    public void StopHeart()
    {
        GameObject[] targetPool = objectManager.GetTargetPool(heartObjs[0]);
        for (int index = 0; index < targetPool.Length; index++)
        {
            GameObject heartObject = targetPool[index];
            Rigidbody2D heartRigidbody = heartObject.GetComponent<Rigidbody2D>();
            if (stop)
            {
                if (!heartVelocities.ContainsKey(heartObject))
                {
                    heartVelocities[heartObject] = heartRigidbody.velocity;
                }
                heartRigidbody.velocity = Vector2.zero;
            }
            else
            {
                if(heartVelocities.TryGetValue(heartObject, out Vector2 previousVelocity))
                {
                    heartRigidbody.velocity = previousVelocity;
                    heartVelocities.Remove(heartObject);
                }
            }
        }
    }

    public void DeleteCoin()
    {
        int ranEnemy = 0;
        objectManager.DelObj(coinObjs[ranEnemy]);
    }

    public void SpawnCoin()
    {
        int ranEnemy = 0;
        int ranPoint = Random.Range(0, spawnPoints.Length);
        GameObject coin= objectManager.MakeObj(coinObjs[ranEnemy]);
        coin.transform.position = spawnPoints[ranPoint].position;

        Rigidbody2D rigid = coin.GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.up * speed[i];
    }

    private Dictionary<GameObject, Vector2> coinVelocities = new Dictionary<GameObject, Vector2>();
    public void StopCoin()
    {
        GameObject[] targetPool = objectManager.GetTargetPool(coinObjs[0]);
        for (int index = 0; index < targetPool.Length; index++)
        {
            GameObject coinObject = targetPool[index];
            Rigidbody2D coinRigidbody = coinObject.GetComponent<Rigidbody2D>();
            if (stop)
            {
                if (!coinVelocities.ContainsKey(coinObject))
                {
                    coinVelocities[coinObject] = coinRigidbody.velocity;
                }
                coinRigidbody.velocity = Vector2.zero;
            }
            else
            {
                if (coinVelocities.TryGetValue(coinObject, out Vector2 previousVelocity))
                {
                    coinRigidbody.velocity = previousVelocity;
                    coinVelocities.Remove(coinObject);
                }
            }
        }
    }
}
