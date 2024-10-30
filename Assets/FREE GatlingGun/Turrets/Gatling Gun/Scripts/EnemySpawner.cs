using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Include this to use UI elements

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public float minSpawnTime = 1f; // Minimum time between spawns
    public float maxSpawnTime = 3f; // Maximum time between spawns
    public float spawnXPosition = -10f; // X position to spawn enemies off-screen
    public float spawnYPosition = 0f; // Y position for spawning
    public float spawnZPosition = 0f; // Z position for spawning

    private Coroutine spawnCoroutine; // Reference to the coroutine
    public Button spawnButton; // Reference to the button

    public int maxEnemies = 10; // Maximum number of enemies to spawn
    private int currentEnemyCount = 0; // Current number of spawned enemies

    private void Start()
    {
        // Optionally, you can keep the button active initially
    }

    public void StartSpawning()
    {
        // Start the spawning coroutine if it's not already running
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnEnemies());
            DisableButton(); // Disable the button after starting spawning
        }
    }

    private void DisableButton()
    {
        if (spawnButton != null)
        {
            spawnButton.gameObject.SetActive(false); // Hide the button
        }
    }

    private void EnableButton()
    {
        if (spawnButton != null)
        {
            spawnButton.gameObject.SetActive(true); // Show the button
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (currentEnemyCount < maxEnemies)
        {
            // Wait for a random interval between minSpawnTime and maxSpawnTime
            float spawnDelay = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnDelay);

            // Instantiate the enemy at the specified off-screen position
            SpawnEnemy();
        }

        // If max enemies reached, stop spawning and enable the button again
        EnableButton();
        spawnCoroutine = null; // Reset the coroutine reference
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(spawnXPosition, spawnYPosition, spawnZPosition);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        currentEnemyCount++; // Increment the count of spawned enemies
    }
}