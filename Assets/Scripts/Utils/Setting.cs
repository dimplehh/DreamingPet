using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    [SerializeField]
    GameObject panel;
    public void ClickSetting()
    {
        panel.SetActive(true);
    }
    public void ClickClose()
    {
        panel.SetActive(false);
    }
}
