using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab;
    public float spawnRadius = 2f; // Distance from spawner where balloons are spawned

    private float spawnTimer; // Timer to track when to spawn the next balloon
    public float spr1 = 5f;
    public float spr2 = 10f;

    void Start()
    {
        
        spawnTimer = Random.Range(spr1, spr2);
    }

    void Update()
    {
        // Decrement the spawn timer
        spawnTimer -= Time.deltaTime;

        // If the timer has reached zero, spawn a balloon and reset the timer with a new random interval
        if (spawnTimer <= 0f)
        {
            SpawnBird();
            spawnTimer = Random.Range(spr1, spr2);
        }
    }

    void SpawnBird()
    {
        // Calculate a random position within the spawn radius
        Vector2 spawnPosition = (Random.insideUnitCircle.normalized * spawnRadius) + (Vector2)transform.position;

        // Spawn the balloon at the random position
        Instantiate(birdPrefab, spawnPosition, Quaternion.identity);
    }
}
