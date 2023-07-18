using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public TMP_Text coinText;
    public TMP_Text TotalCoinText;
    public int savedCoin= 0;
    public int coin = 0;
    public string KeyString = "TotalCoin";

    public void GenerateCoin()
    {
        savedCoin = PlayerPrefs.GetInt(KeyString);
        PlayerPrefs.Save();
        coinText.text = coin.ToString();
        TotalCoinText.text = savedCoin.ToString();
    }

    //public void UpdateCoin(GameObject gameObject)
    //{
    //    if (gameObject.CompareTag("Player"))
    //    {
    //        coin += 10;
    //        gameObject.GetComponent<Player>().coin = coin;
    //        Debug.Log(coinText.text);
    //        coinText.text = string.Format("{0:n0}", gameObject.GetComponent<Player>().coin);
    //        savedCoin += 10;
    //        PlayerPrefs.SetInt(KeyString, savedCoin);
    //        PlayerPrefs.Save();
    //        TotalCoinText.text = savedCoin.ToString();
    //    }
    //}
}
