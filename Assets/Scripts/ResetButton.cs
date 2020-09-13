using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ResetButton : MonoBehaviour
{
    public GameObject Manager;
    public void Restart()
    {
        Manager.GetComponent<StateManager>().audioManager.PlaySound("Shop");
        SceneManager.LoadScene("Shop");
        // print("reee");
        Manager.GetComponent<StateManager>().shopMenu.SetActive(true);
        Manager.GetComponent<StateManager>().helpBut.SetActive(true);
        Manager.GetComponent<StateManager>().NoDeathScreen();
        Manager.GetComponent<StateManager>().ResetHealth();
        Manager.GetComponent<StateManager>().ResetStats();
    }
}
