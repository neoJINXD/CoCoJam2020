using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    public float speed = 0.125f;
    public Vector3 offset;
    
    void FixedUpdate() 
    {
        Vector3 newPos = player.position + offset;
        Vector3 finalPos = Vector3.Lerp(transform.position, newPos, speed);
        transform.position = finalPos;
    }

}
