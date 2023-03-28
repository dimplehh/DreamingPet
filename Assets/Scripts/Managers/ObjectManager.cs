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

    GameObject[] enemy;
    GameObject[] fever;
    GameObject[] rain;
    GameObject[] spaceship;
    //GameObject[] coin;
    GameObject[] targetPool;

    private void Awake()
    {
        enemy = new GameObject[10];
        fever = new GameObject[5];
        rain = new GameObject[5];
        spaceship = new GameObject[5];
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
            //Managers.Resource.Instantiate(); //���� �� �ڵ�� �ٲٱ�
            fever[index].SetActive(false);
        }
        for (int index = 0; index < rain.Length; index++)
        {
            rain[index] = Instantiate(rainPrefab);
            //Managers.Resource.Instantiate(); //���� �� �ڵ�� �ٲٱ�
            rain[index].SetActive(false);
        }
        for (int index = 0; index < spaceship.Length; index++)
        {
            spaceship[index] = Instantiate(spaceshipPrefab);
            //Managers.Resource.Instantiate(); //���� �� �ڵ�� �ٲٱ�
            spaceship[index].SetActive(false);
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
        }
        
        for (int index = 0; index < targetPool.Length; index++)
        {
            if(targetPool[index].activeSelf && type == "spaceship"
                && Camera.main.WorldToViewportPoint(targetPool[index].transform.position).x <= -0.1)
                targetPool[index].SetActive(false);
            else if (targetPool[index].activeSelf && Camera.main.WorldToViewportPoint(targetPool[index].transform.position).y >= 1.3)
                targetPool[index].SetActive(false);
        }
    }
}
