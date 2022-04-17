using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SettingsScript : MonoBehaviour
{
    public TMPro.TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    public TMPro.TMP_Dropdown qualityDropdown;
    public Toggle vsyncToggle;

    void Start(){
        // Video Section

        // Fill the resolution dropdown with available resolutions
        // See Brackeys' video - https://www.youtube.com/watch?v=YOaYQrN1oYQ
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex=0;
        for(int i = 0; i < resolutions.Length; i++){
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height){
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value=currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // In this section we are getting saved preferences and setting values in every dropdown, toggle, etc.
        qualityDropdown.value=PlayerPrefs.GetInt("qualityIndex");
        if(PlayerPrefs.GetInt("vsync")==1){
            vsyncToggle.isOn=true;
        } else{
            vsyncToggle.isOn=false;
        }
    }

    // Sets the resolution chosen in the resolution dropdown
    public void Resolution(int resolutionIndex){
        if(Time.timeSinceLevelLoad>=1){
            Resolution resolution = resolutions[resolutionIndex];
            // Checks if the program is in fullscreen mode, so that the fullscreen mode won't be toggled when changing the resolution
            if(Screen.fullScreen==true){
                Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.ExclusiveFullScreen);
            }
            else{
                Screen.SetResolution(resolution.width, resolution.height, false);
            }
        }
    }

    // Sets the quality level chosen in the quality dropdown
    public void Quality(int qualityIndex){
        if(Time.timeSinceLevelLoad>=1){
            QualitySettings.SetQualityLevel(qualityIndex);
            PlayerPrefs.SetInt("qualityIndex", qualityIndex);
        }
    }

    // Toggle vertical synchronization based on the vsyncToggle value
    public void SetVSync(bool state){
        if(state==false){
            QualitySettings.vSyncCount = 0;
            PlayerPrefs.SetInt("vsync", 0);
        } else {
            QualitySettings.vSyncCount = 1;
            PlayerPrefs.SetInt("vsync", 1);
        }
    }
}
