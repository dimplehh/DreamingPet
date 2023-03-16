using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;
    [SerializeField]
    GameObject feverPrefab;

    GameObject[] enemy;
    GameObject[] fever;
    //GameObject[] coin;
    GameObject[] targetPool;

    private void Awake()
    {
        enemy = new GameObject[10];
        fever = new GameObject[5];
        //coin = new GameObject[10];
        Generate();
    }

    void Generate()
    {
        for(int index = 0; index < enemy.Length; index++)
        {
            enemy[index] = Instantiate(enemyPrefab);
            //Managers.Resource.Instantiate(); //차후 이 코드로 바꾸기
            enemy[index].SetActive(false);
        }
        for (int index = 0; index < fever.Length; index++)
        {
            fever[index] = Instantiate(feverPrefab);
            //Managers.Resource.Instantiate(); //차후 이 코드로 바꾸기
            fever[index].SetActive(false);
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
        }
        for (int index = 0; index < targetPool.Length; index++)
        {
            if (targetPool[index].activeSelf && Camera.main.WorldToViewportPoint(targetPool[index].transform.position).y >= 1.3)
                targetPool[index].SetActive(false);
        }
    }
}
