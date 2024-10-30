using UnityEngine;
using UnityEngine.UI; // Include for UI

public class GatlingGun : MonoBehaviour
{
    private Transform go_target;

    public Transform go_baseRotation;
    public Transform go_GunBody;
    public Transform go_barrel;

    public float barrelRotationSpeed;
    private float currentRotationSpeed;

    public float firingRange = 15f; // Initial firing range
    public ScoreManager scoreManager; // Reference to ScoreManager
    public Button upgradeRangeButton; // Reference to the UI Button

    public GameObject bulletPrefab; // Prefab for the bullet particle
    public float bulletSpeed = 20f; // Speed of the bullet
    public int damagePerBullet = 10; // Damage dealt by each bullet

    private void Start()
    {
        upgradeRangeButton.onClick.AddListener(UpgradeRange); // Link button to upgrade method
        UpdateButtonInteractability(); // Update button state at start
    }

    private void Update()
    {
        CheckForEnemies();
        AimAndFire();
        UpdateButtonInteractability(); // Check if the button can be interacted with
    }

    private void CheckForEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, firingRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("NewEnemy")) // Back to "NewEnemy"
            {
                go_target = hitCollider.transform;
                return; // Exit the loop after finding the first enemy
            }
        }
        go_target = null; // No target found
    }

    private void AimAndFire()
    {
        if (go_target != null)
        {
            // Aim and fire logic here
            currentRotationSpeed = barrelRotationSpeed;

            // Fire logic
            Fire();
        }
        else
        {
            currentRotationSpeed = 0; // Stop rotating
        }
    }

    private void Fire()
    {
        // Instantiate the bullet particle and set its direction and speed
        GameObject bullet = Instantiate(bulletPrefab, go_barrel.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        Vector3 direction = (go_target.position - go_barrel.position).normalized;
        bulletRb.velocity = direction * bulletSpeed;

        // Set the bullet to destroy itself after a certain time
        Destroy(bullet, 2f); // Adjust time as necessary
    }

    public void UpgradeRange()
    {
        if (scoreManager.SpendPoints(scoreManager.scorePerRangeUpgrade))
        {
            firingRange += 5f; // Increase the range by 5
            Debug.Log("Gatling Gun range upgraded! New range: " + firingRange);
        }
        else
        {
            Debug.Log("Not enough score to upgrade range!");
        }
    }

    private void UpdateButtonInteractability()
    {
        if (scoreManager != null && upgradeRangeButton != null)
        {
            upgradeRangeButton.interactable = scoreManager.GetScore() >= scoreManager.scorePerRangeUpgrade;
        }
    }
}