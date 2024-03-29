using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    public static Managers Instance { get { init(); return s_instance; } }
    GameManager _game = new GameManager();
    CoinManager _coin = new CoinManager();
    ResourceManager _resource = new ResourceManager();
    ObjectManager _object = new ObjectManager();
    ScoreManager _score = new ScoreManager();
    AdmobManager _Ad = new AdmobManager();

    public static GameManager Game { get { return Instance._game; } }
    public static CoinManager Coin { get { return Instance._coin; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static ObjectManager Object { get { return Instance._object; } }
    public static ScoreManager Score { get { return Instance._score; } }
    public static AdmobManager Ad { get { return Instance._Ad; } }


    void Start()
    {
        //init();
    }

    static void init()//int함수 시행 되는거..?
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
            Managers.Ad.init();
        }
        
    }
    
}