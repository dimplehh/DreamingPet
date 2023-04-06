using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Loading : MonoBehaviour
{
    public Slider progressbar;
    public TMP_Text loadingText;
    public Image foot1, foot2, foot3, foot4;
    
    private void Start() 
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync("SampleScene");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;
            if (progressbar.value < 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime*0.7f);
                if (progressbar.value < 0.3) loadingText.text = "Loading.";
                else if (progressbar.value >=0.3 && progressbar.value < 0.6) loadingText.text = "Loading..";
                else loadingText.text = "Loading...";

                if (progressbar.value > 0.1)
                    foot1.gameObject.SetActive(true);
                if (progressbar.value > 0.3)
                    foot2.gameObject.SetActive(true);
                if (progressbar.value > 0.5)
                    foot3.gameObject.SetActive(true);
                if (progressbar.value > 0.7)
                    foot4.gameObject.SetActive(true);


            }
            else if (progressbar.value >= 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
            }

            if(progressbar.value >= 1f)
            {
                operation.allowSceneActivation = true; 
            }
        }  
    }
}
