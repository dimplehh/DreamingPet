using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; //고정한다는 의미의 static을 붙여 Managers class의 instance만듦
    public static Managers Instance { get { init(); return s_instance; } } //다른 모든 class들이 공통으로 사용 가능하도록 
                                                                           //public으로 init()함수를 실행하고 인스턴스를 return하는 기능하는 애를 만듦
    GameManager _game = new GameManager();
    ResourceManager _resource = new ResourceManager();
    //ObjectManager _object = new ObjectManager();

    public static GameManager Game { get { return Instance._game; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    //public static ObjectManager Object { get { return Instance._object; } }
    void Start()
    {
        init();
    }

    void Update()
    {

    }

    static void init()
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
        }
    }
}