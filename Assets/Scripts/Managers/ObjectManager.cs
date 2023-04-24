using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;
    [SerializeField]
    GameObject feverPrefab;
    [SerializeField]
    GameObject rainPrefab;
    [SerializeField]
    GameObject spaceshipPrefab;
    [SerializeField]
    GameObject heartPrefab;
    [SerializeField]
    GameObject shieldpiecePrefab;

    GameObject[] enemy;
    GameObject[] fever;
    GameObject[] rain;
    GameObject[] spaceship;
    GameObject[] heart;
    GameObject[] shieldpiece;
    //GameObject[] coin;
    GameObject[] targetPool;

    GameManager gameManager;

    private void Awake()
    {
        enemy = new GameObject[20];
        fever = new GameObject[5];
        rain = new GameObject[5];
        spaceship = new GameObject[5];
        heart = new GameObject[5];
        shieldpiece = new GameObject[10];
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        //coin = new GameObject[10];
        Generate();
    }

    void Generate()
    {
        for(int index = 0; index < enemy.Length; index++)
        {
            enemy[index] = Instantiate(enemyPrefab);
            //Managers.Resource.Instantiate(); //���� �� �ڵ�� �ٲٱ�
            enemy[index].SetActive(false);
        }
        for (int index = 0; index < fever.Length; index++)
        {
            fever[index] = Instantiate(feverPrefab);
            fever[index].SetActive(false);
        }
        for (int index = 0; index < rain.Length; index++)
        {
            rain[index] = Instantiate(rainPrefab);
            rain[index].SetActive(false);
        }
        for (int index = 0; index < spaceship.Length; index++)
        {
            spaceship[index] = Instantiate(spaceshipPrefab);
            spaceship[index].SetActive(false);
        }
        for (int index = 0; index < heart.Length; index++)
        {
            heart[index] = Instantiate(heartPrefab);
            heart[index].SetActive(false);
        }
        for (int index = 0; index < shieldpiece.Length; index++)
        {
            shieldpiece[index] = Instantiate(shieldpiecePrefab);
            shieldpiece[index].SetActive(false);
        }
    }

    public GameObject[] GetTargetPool(string type)
    {
        switch (type)
        {
            case "enemy":
                targetPool = enemy;
                break;
            case "fever":
                targetPool = fever;
                break;
            case "rain":
                targetPool = rain;
                break;
            case "spaceship":
                targetPool = spaceship;
                break;
            case "heart":
                targetPool = heart;
                break;
            case "shieldpiece":
                targetPool = shieldpiece;
                break;
        }
        return targetPool;
    }

    public GameObject MakeObj(string type)
    {
        switch(type)
        {
            case "enemy":
                targetPool = enemy;
                break;
            case "fever":
                targetPool = fever;
                break;
            case "rain":
                targetPool = rain;
                break;
            case "spaceship":
                targetPool = spaceship;
                break;
            case "heart":
                targetPool = heart;
                break;
            case "shieldpiece":
                targetPool = shieldpiece;
                break;
        }
        
        for (int index = 0; index < targetPool.Length; index++)
        {
            
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }
        return null;
    }

    public void DelObj(string type)
    {
        switch(type)
        {
            case "enemy":
                targetPool = enemy;
                break;
            case "fever":
                targetPool = fever;
                break;
            case "rain":
                targetPool = rain;
                break;
            case "spaceship":
                targetPool = spaceship;
                break;
            case "heart":
                targetPool = heart;
                break;
            case "shieldpiece":
                targetPool = shieldpiece;
                break;
        }
        
        for (int index = 0; index < targetPool.Length; index++)
        {
            if(targetPool[index].activeSelf && gameManager.clean)
                targetPool[index].SetActive(false);
            else if(targetPool[index].activeSelf &&( type != "enemy" && type != "shieldpiece") && gameManager.feverState)
                targetPool[index].SetActive(false);
            else if(targetPool[index].activeSelf && type == "spaceship"
                && (Camera.main.WorldToViewportPoint(targetPool[index].transform.position).x <= -0.5f ||
                Camera.main.WorldToViewportPoint(targetPool[index].transform.position).x >= 1.5f))
            {
                gameManager.EffectSoundStop(gameManager.GetComponent<LevelManager>().bgList[0]);
                targetPool[index].SetActive(false);
            }
            else if (targetPool[index].activeSelf && Camera.main.WorldToViewportPoint(targetPool[index].transform.position).y >= 2.5)
                targetPool[index].SetActive(false);
        }
    }
}
