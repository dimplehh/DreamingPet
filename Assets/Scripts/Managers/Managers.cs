using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; //�����Ѵٴ� �ǹ��� static�� �ٿ� Managers class�� instance����
    public static Managers Instance { get { init(); return s_instance; } } //�ٸ� ��� class���� �������� ��� �����ϵ��� 
                                                                           //public���� init()�Լ��� �����ϰ� �ν��Ͻ��� return�ϴ� ����ϴ� �ָ� ����
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