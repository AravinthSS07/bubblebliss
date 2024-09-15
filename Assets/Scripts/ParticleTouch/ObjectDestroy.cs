using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroy : MonoBehaviour
{
    public float destroyTime = 1f; // Time to destroy the object

    void Start()
    {
        // Destroy the object after the specified time
        Destroy(gameObject, destroyTime);
    }
}
