using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class ShopList //상점 panel에 있는 물건들 (SerializeField로 지정. ->현재처럼 일일이 지정 말고 더 좋은 방법 있긴 할듯)
{
	public int id;
	public Image prefab;
    public Image mak;
    public GameObject click;
}

[System.Serializable]
public class Items //아이템 저장 정보(ID, 이름, 가격, 구매여부)
{
	public int id;
	public string name;
	public int cost;
	public bool isBuy = false;
	public Items(int id, string name, int cost, bool isBuy)
	{
		this.id = id;
		this.name = name;
		this.cost = cost;
		this.isBuy = isBuy;
	}
}

public class ShopManager : MonoBehaviour
{
	List<Items> Items = new List<Items>();
	[SerializeField]
	ShopList[] shopList = new ShopList[6];
	[SerializeField]
	TMP_Text coinText;
	int coin;
    public int index = 0;

    private void Start()//임시. 차후 JSON으로 관리하는것이 좋을것
    {
		Items.Add(new Items(0, "bone", 0, false));
		Items.Add(new Items(1, "specialbone", 10, false));
		Items.Add(new Items(2, "twig", 3, false));
		Items.Add(new Items(3, "ball", 5, false));
		Items.Add(new Items(4, "doll", 8, false));
		Items.Add(new Items(5, "super", 5, false));
	}

    public void Save() //저장함수 - 파일스트림으로 저장후 직렬화
    {
		FileStream fs = File.Create(Application.persistentDataPath + "/Items.dat");
		BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize(fs, Items);
		fs.Close();
    }

    public void Select(int index)
    {
        shopList[this.index].click.SetActive(false);
        shopList[index].click.SetActive(true);
        this.index = index;
    }

	public void Buy() //구매 - 매 panel 의 이미지에 붙어있는 함수이며, 현재 가진 코인개수와 비교하여 구매 가능하면 구매.
    {
        int index = this.index;
		if(Items != null)
        {
			int coin = PlayerPrefs.GetInt("TotalCoin", 0);
			if (coin > Items[index].cost && Items[index].isBuy == false)
            {
				PlayerPrefs.SetInt("TotalCoin", coin - Items[index].cost);
				PlayerPrefs.Save();
				coinText.text = string.Format("{0:n0}", PlayerPrefs.GetInt("TotalCoin", 0));
				Items[index].isBuy = true;
                shopList[index].mak.color = new Color(0,0,0,0);
				Save();
			}
        }
    }

	public bool Load() //맨 처음 HomeSceme로드 시 불러오는 함수
    {
        string path = Application.persistentDataPath + "/Items.dat";
        // Checking if the file exists
        if (File.Exists(path))
        {
            FileStream fs = File.Open(path, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            if (fs.Length > 0)
            {
				Items = (List<Items>)bf.Deserialize(fs);
				Reinstantiate();
				fs.Close();
                return true;
            }
        }
        return false;
    }

	public void Reinstantiate() //Load 함수에서 쓰임, 모든 Item 보면서 구매했을 시
	{
		for (int i = 0; i < Items.Count; i++)
		{
			if(Items[i].isBuy)
                shopList[i].mak.color = new Color(0, 0, 0, 0);
        }
	}
}
