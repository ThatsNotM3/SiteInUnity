using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class confirmQuitScript : MonoBehaviour
{   
    // Just Quits the Program
    public void Quit(){
        Application.Quit();
    }

    // Calls the "LoadNewPage" function which loads the last tab you visited before opening quit dialogue
    public void GoBack(){
        this.GetComponent<LoadWebpage>().LoadNewPage(PlayerPrefs.GetString("lastTab"));
    }
}
