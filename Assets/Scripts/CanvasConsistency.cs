using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasConsistency : MonoBehaviour
{
    private static CanvasConsistency instance;
    void Awake() 
    {
        if (CanvasConsistency.instance != null && instance != this)
        {   
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }
}
