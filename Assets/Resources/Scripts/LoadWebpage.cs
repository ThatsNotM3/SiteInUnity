using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadWebpage : MonoBehaviour
{   
    private GameObject addressInputFieldGameObject;
    private Slider progressBarSlider;

    void Start(){
        // Finds the progress bar slider and checks if its value equals 1. If yes, it deactivates the slider so it won't just stay there and won't be visible
        progressBarSlider=GameObject.Find("ProgressBar").GetComponent<Slider>();
        if(progressBarSlider.value==1f){
            progressBarSlider.gameObject.SetActive(false);
        }
    }

    // This function loads new scene(tab)
    // See Brackeys' video about loading screens and progress bar - https://youtu.be/YMj2qPq9CP8
    public void LoadNewPage(string page){
        AsyncOperation operation = SceneManager.LoadSceneAsync(page);
        StartCoroutine(ProgressSlider(operation));
        addressInputFieldGameObject=GameObject.Find("AddressBarInputField");
        addressInputFieldGameObject.GetComponent<TMPro.TMP_InputField>().text="www.siteInUnity.tm3/"+page;

        GameObject.Find("MainUICanvas").GetComponent<MainScript>().SavePreferenceLastTab(page);
    }
    // This coroutine is responsible for the progress bar slider
    IEnumerator ProgressSlider(AsyncOperation operation){
        progressBarSlider.gameObject.SetActive(true);
        while(!operation.isDone){
            float progress=Mathf.Clamp01(operation.progress/.9f);
            progressBarSlider.value=progress;
            yield return null;
        }
        progressBarSlider.gameObject.SetActive(false);
    }
}
