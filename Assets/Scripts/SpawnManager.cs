using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject germPrefab;
    public float spawnInterval = 2f;
    private bool isGameStarted = false; // Variable to track whether the game has started

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
        if (isGameStarted)
        {
            float screenWidth = Camera.main.aspect * Camera.main.orthographicSize;
            float screenHeight = Camera.main.orthographicSize;

            Vector2 spawnPosition = new Vector2(Random.Range(-screenWidth, screenWidth), Random.Range(-screenHeight, screenHeight));
            Instantiate(germPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
