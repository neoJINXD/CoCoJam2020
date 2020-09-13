using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ResetButton : MonoBehaviour
{
    public GameObject Manager;
    public void Restart()
    {
        SceneManager.LoadScene("Shop");
        // print("reee");
        Manager.GetComponent<StateManager>().NoDeathScreen();
    }
}
