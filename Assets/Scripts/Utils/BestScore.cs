using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BestScore : MonoBehaviour
{
    public Image image, image2;
    public TMP_Text text, text2;
    
    public bool gameover;
    private void Update()
    {
        if (gameover)
        {
            float textWidth = text.preferredWidth;
            float imageWidth = image.rectTransform.rect.width;

            float xPos = text.rectTransform.anchoredPosition.x - textWidth / 2f - imageWidth / 2f;
            float yPos = image.rectTransform.anchoredPosition.y;
            image.rectTransform.anchoredPosition = new Vector2(xPos - 10.0f, yPos);
        }
        else
        {
            float textWidth2 = text2.preferredWidth;
            float imageWidth2 = image2.rectTransform.rect.width;

            float xPos2 = text2.rectTransform.anchoredPosition.x - textWidth2 / 2f - imageWidth2 / 2f;
            float yPos2 = image2.rectTransform.anchoredPosition.y;
            image2.rectTransform.anchoredPosition = new Vector2(xPos2, yPos2);
        }
        
    }
}
