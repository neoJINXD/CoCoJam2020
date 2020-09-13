using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordDisablew : MonoBehaviour
{
    void Update()
    {
        // print(transform.rotation.eulerAngles.ToString());
        if (transform.rotation.eulerAngles.z == 300.0f || transform.rotation.eulerAngles.z == 60.0f)
        {
            gameObject.SetActive(false);
            // print("Done");
        }
    }
}
