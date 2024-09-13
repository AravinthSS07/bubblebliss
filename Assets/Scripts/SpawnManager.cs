using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] germPrefabs; // Array to hold different types of germ prefabs
    public float spawnInterval = 2f;
    private bool isGameStarted = false; // Variable to track whether the game has started

    // Variables to define the spawn zone for a portrait screen
    public float spawnZoneWidth = 5f;  // Example width
    public float spawnZoneHeight = 10f; // Example height

    void Start()
    {
        // Do not start spawning germs until the game begins
    }

    // Function to be called when the game starts
    public void StartSpawning()
    {
        isGameStarted = true;
        InvokeRepeating("SpawnGerm", 0f, spawnInterval);
    }

    void SpawnGerm()
    {
        if (isGameStarted && germPrefabs.Length > 0)
        {
            // Calculate the spawn area bounds
            float spawnZoneHalfWidth = spawnZoneWidth / 2f;
            float spawnZoneHalfHeight = spawnZoneHeight / 2f;

            // Generate a random position within the spawn zone
            Vector2 spawnPosition = new Vector2(
                Random.Range(-spawnZoneHalfWidth, spawnZoneHalfWidth),
                Random.Range(-spawnZoneHalfHeight, spawnZoneHalfHeight)
            );

            // Randomly select a germ prefab from the array
            GameObject germPrefab = germPrefabs[Random.Range(0, germPrefabs.Length)];

            // Instantiate the selected germ prefab at the calculated position
            Instantiate(germPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
