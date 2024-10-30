using UnityEngine;

public class DamageParticle : MonoBehaviour
{
    public int damage = 1; // Damage amount

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NewEnemy"))
        {
            // This can be removed since damage handling is now in the Enemy script
        }
        Destroy(gameObject); // Destroy the particle after it hits
    }
}