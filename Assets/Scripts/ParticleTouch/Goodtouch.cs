using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goodtouch : MonoBehaviour
{
    public GameObject objectToSpawn; // Reference to the prefab to spawn

    void Update()
    {
        // Check if there is at least one touch
        if (Input.touchCount > 0)
        {
            // Get the first touch
            Touch touch = Input.GetTouch(0);

            // Check if the touch just began
            if (touch.phase == TouchPhase.Began)
            {
                // Convert touch position to world position
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane));
                //touchPosition.z = 0; // Ensure the z position is 0 for 2D

                // Spawn the object at the touch position
                GameObject spawnedObject = Instantiate(objectToSpawn, touchPosition, Quaternion.identity);

                // Destroy the object after 1 second
                //Destroy(spawnedObject, 1f);
            }
        }
    }
}
