using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 5; // Current health of the enemy
    public int maxHealth = 5; // Maximum health of the enemy
    public float moveSpeed = 2f; // Movement speed
    public ParticleSystem hitEffect; // Effect when hit

    // Health increment values
    public int baseHealthIncrement = 1; // Base increment per respawn
    public int additionalHealthIncrement = 1; // Additional increment for each respawn
    private int respawnCount = 0; // Count of how many times the enemy has respawned

    // Store the initial spawn position
    private Vector3 spawnPosition;

    // Reference to the ScoreManager
    private ScoreManager scoreManager;

    private void Start()
    {
        // Set the initial spawn position to the enemy's current position
        spawnPosition = transform.position;

        // Find the ScoreManager in the scene
        scoreManager = FindObjectOfType<ScoreManager>();

        // Ensure there's a collider for collision detection
        if (!TryGetComponent<Collider>(out _))
        {
            gameObject.AddComponent<BoxCollider>().isTrigger = true; // Add a trigger collider if none exists
        }
    }

    private void Update()
    {
        // Move the enemy constantly in the negative X direction
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is a projectile
        if (other.CompareTag("Projectile"))
        {
            // Get the damage amount from the projectile
            int damage = other.GetComponent<DamageParticle>().damage;
            TakeDamage(damage); // Take damage based on projectile hit
            Destroy(other.gameObject); // Destroy the projectile after hit
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        // Play hit effect if assigned
        if (hitEffect != null)
        {
            hitEffect.transform.position = transform.position; // Position it at the enemy's location
            hitEffect.Play();
        }

        // Check if the enemy is dead
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy has died!");

        // Increase maximum health with each respawn
        respawnCount++;
        maxHealth += baseHealthIncrement + (additionalHealthIncrement * respawnCount); // Increment health

        // Add points to the score manager
        if (scoreManager != null)
        {
            scoreManager.AddPoints(1); // Increase score by 1 for each respawn
        }

        health = maxHealth; // Reset current health to maximum

        // Respawn at the original position
        transform.position = spawnPosition;
    }
}