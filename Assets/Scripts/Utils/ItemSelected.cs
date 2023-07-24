using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelected : MonoBehaviour
{
    [SerializeField] ToyDatabase toyDB;
    [SerializeField] ShopManager shopManager;
    public void ToySelect()
    {
        if (toyDB.GetToy(shopManager.index).isBuy == true)
        {
            foreach (Transform child in transform)
            {
                child.GetChild(0).gameObject.SetActive(false);
            }
            transform.GetChild(PlayerPrefs.GetInt("toyOption")).GetChild(0).gameObject.SetActive(true);
        }
    }
}
