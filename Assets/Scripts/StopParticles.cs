using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopParticles : MonoBehaviour
{
    public float bloodTime;
    void Start() {
        Invoke("KillParticles", bloodTime);
    }
    private void KillParticles()
    {
        Destroy(gameObject);
    }
}
