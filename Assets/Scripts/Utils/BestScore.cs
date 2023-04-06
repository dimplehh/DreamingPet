using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BestScore : MonoBehaviour
{
    public Image image;
    public TMP_Text text;
    public bool gameover;
    private void Update()
    {
        float textWidth = text.preferredWidth;
        float imageWidth = image.rectTransform.rect.width;

        float xPos = text.rectTransform.anchoredPosition.x - textWidth / 2f - imageWidth / 2f;
        float yPos = image.rectTransform.anchoredPosition.y;
        if(gameover)
            image.rectTransform.anchoredPosition = new Vector2(xPos-10.0f, yPos);
        else
            image.rectTransform.anchoredPosition = new Vector2(xPos - 170.0f, yPos);
    }
}
