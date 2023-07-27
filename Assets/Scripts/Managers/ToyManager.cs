using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyManager : MonoBehaviour
{
    public ToyDatabase toyDB;

    public SpriteRenderer toySpriteRenderer;

    private int toyOption = 0;

    private int selectToy = 0;
    
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("toyOption"))
        {
            toyOption = 0;
        }
        else
        {
            Load();
        }
        UpdateToy(toyOption);
    }

    public void ToyChange()
    {
        Debug.Log(toyDB.GetToy(selectToy).isBuy);
        if (toyDB.GetToy(selectToy).isBuy == true)
        {
            Debug.Log("토이체인지");
            toyOption = selectToy;
            UpdateToy(toyOption);
            
            Save();
        }
    }

    private void UpdateToy(int selectedOption)
    {
        Toy toy = toyDB.GetToy(selectedOption);
        toySpriteRenderer.sprite = toy.toySprite;
        // 애니메이션 클립 업데이트 필요
    }

    private void Load()
    {
        toyOption = PlayerPrefs.GetInt("toyOption");
    }
    private void Save()
    {
        PlayerPrefs.SetInt("toyOption", toyOption);
    }
    public void SelectToy(int num)
    {
        this.selectToy = num;
    }
}
