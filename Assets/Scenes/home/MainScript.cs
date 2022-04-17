using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{   
    public RawImage fullscreenRawImage;
    public Texture fcTextureSmaller;
    public Texture fcTextureLarger;

    void Start(){
        // Check if the last tab was recorded and load it if yes. Otherwise set "home" as a last tab
        if(PlayerPrefs.HasKey("lastTab")){
            this.GetComponent<LoadWebpage>().LoadNewPage(PlayerPrefs.GetString("lastTab"));
        } else{
            SavePreferenceLastTab("home");
        }

        // Decides which icon should be active in Toggle Fullscreen Button based on current Fullscreen mode
        if(Screen.fullScreen==false){
            fullscreenRawImage.texture=fcTextureLarger;
        } else{
            fullscreenRawImage.texture=fcTextureSmaller;
        }

        // Checks for saved preferences and sets them if they are not present
        if(!PlayerPrefs.HasKey("vsync")){
            PlayerPrefs.SetInt("vsync", 1);
        }
        if(!PlayerPrefs.HasKey("qualityIndex")){
            PlayerPrefs.SetInt("qualityIndex", 1);
            QualitySettings.SetQualityLevel(1);
        }
    }

    // Saves the last tab you visited
    public void SavePreferenceLastTab(string tab){
        if(tab!="confirmQuit"){
            PlayerPrefs.SetString("lastTab", tab);
        }
    }

    // Toggles the fullscreen mode
    public void FullScreenToggle(){
        if(Screen.fullScreen==false){
            Screen.fullScreen=true;
            fullscreenRawImage.texture=fcTextureSmaller;
        } else{
            Screen.fullScreen=false;
            fullscreenRawImage.texture=fcTextureLarger;
        }
    }

    // Opens the specified link
    public void OpenLink(string link){
        Application.OpenURL(link);
    }
}
