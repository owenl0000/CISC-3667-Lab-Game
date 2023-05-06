using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject balloonPrefab;
    public float spawnInterval = 2f; // Time between spawns
    public float spawnRadius = 2f; // Distance from spawner where balloons are spawned

    private float spawnTimer; // Timer to track when to spawn the next balloon

    void Start()
    {
        balloonRate(GameManager.Instance.difficultyLevel);
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        // Decrement the spawn timer
        spawnTimer -= Time.deltaTime;

        // If the timer has reached zero, spawn a balloon
        if (spawnTimer <= 0f)
        {
            SpawnBalloon();
            spawnTimer = spawnInterval;
        }
    }

    void SpawnBalloon()
    {
        // Calculate a random position within the spawn radius
        Vector2 spawnPosition = (Random.insideUnitCircle.normalized * spawnRadius) + (Vector2)transform.position;

        // Spawn the balloon at the random position
        Instantiate(balloonPrefab, spawnPosition, Quaternion.identity);
    }

    public void balloonRate(int level)
    {
        float[,] spawnIntervals = new float[3,3] {
            {2f, 0.5f, 0.5f},
            {1f, 0.5f, 0.5f},
            {0.5f, 0.5f, 0.5f}
        };

        int sceneIndex = GameManager.Instance.currentSceneIndex-1;
        float thisSpawnInterval = spawnIntervals[sceneIndex, level-1];

       spawnInterval = thisSpawnInterval;
    }
}
