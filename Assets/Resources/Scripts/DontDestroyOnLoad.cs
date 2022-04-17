using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour{
    private static DontDestroyOnLoad instance = null;
    public static DontDestroyOnLoad Instance
    {
        get { return instance; }
    }
    
    void Awake()
    {
        if (instance != null && instance != this){
            Destroy(this.gameObject);
            return;
        }
        else{
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}