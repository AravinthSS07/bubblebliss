using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject germPrefab;
    public float spawnInterval = 2f;

    void Start()
    {
        InvokeRepeating("SpawnGerm", 0f, spawnInterval);
    }

    void SpawnGerm()
    {
        float screenWidth = Camera.main.aspect * Camera.main.orthographicSize;
        float screenHeight = Camera.main.orthographicSize;

        Vector2 spawnPosition = new Vector2(Random.Range(-screenWidth, screenWidth), Random.Range(-screenHeight, screenHeight));
        Instantiate(germPrefab, spawnPosition, Quaternion.identity);
    }

}
