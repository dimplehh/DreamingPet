using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSpeed : MonoBehaviour
{
    /*프로토타입 속도 조절에만 쓰이는 스크립트이다.*/
    public Slider speedSlider;
    public TMP_Text speedText;
    // Start is called before the first frame update
    void Start()
    {
        speedSlider.value = 1f;
        speedText.text = "2";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnSliderChanged()
    {
        if (GameObject.FindWithTag("Player")) {
            GameObject.FindWithTag("Player").GetComponent<MovePet>().speed
                = 2 * speedSlider.value;
            speedText.text = (2 * speedSlider.value).ToString("F2");
        }
    }
}
