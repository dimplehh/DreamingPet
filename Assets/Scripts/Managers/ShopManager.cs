using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class ShopList //���� panel�� �ִ� ���ǵ� (SerializeField�� ����. ->����ó�� ������ ���� ���� �� ���� ��� �ֱ� �ҵ�)
{
	public int id;
	public Image prefab;
}

[System.Serializable]
public class Items //������ ���� ����(ID, �̸�, ����, ���ſ���)
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

    private void Start()//�ӽ�. ���� JSON���� �����ϴ°��� ������
    {
		Items.Add(new Items(0, "normal", 10, false));
		Items.Add(new Items(1, "special", 5, false));
		Items.Add(new Items(2, "common", 10, false));
		Items.Add(new Items(3, "good", 5, false));
		Items.Add(new Items(4, "great", 10, false));
		Items.Add(new Items(5, "super", 5, false));
	}

    public void Save() //�����Լ� - ���Ͻ�Ʈ������ ������ ����ȭ
    {
		FileStream fs = File.Create(Application.persistentDataPath + "/Items.dat");
		BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize(fs, Items);
		fs.Close();
    }

	public void Buy(int index) //���� - �� panel �� �̹����� �پ��ִ� �Լ��̸�, ���� ���� ���ΰ����� ���Ͽ� ���� �����ϸ� ����.
    {
		if(Items != null)
        {
			int coin = PlayerPrefs.GetInt("TotalCoin", 0);
			if (coin > Items[index].cost)
            {
				PlayerPrefs.SetInt("TotalCoin", coin - Items[index].cost);
				PlayerPrefs.Save();
				coinText.text = string.Format("{0:n0}", PlayerPrefs.GetInt("TotalCoin", 0));
				Items[index].isBuy = true;
				shopList[index].prefab.color = Color.black;
				Save();
			}
        }
    }

	public bool Load() //�� ó�� HomeSceme�ε� �� �ҷ����� �Լ�
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

	public void Reinstantiate() //Load �Լ����� ����, ��� Item ���鼭 �������� ��
	{
		for (int i = 0; i < Items.Count; i++)
		{
			if(Items[i].isBuy)
				shopList[i].prefab.color = Color.black;
		}
	}
}
