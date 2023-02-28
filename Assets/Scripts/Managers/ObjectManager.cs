using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    GameObject[] enemy;
    //GameObject[] coin;
    GameObject[] targetPool;

    private void Awake()
    {
        enemy = new GameObject[10];
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
    }

    public GameObject MakeObj(string type)
    {
        switch(type)
        {
            case "enemy":
                targetPool = enemy;
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
}
