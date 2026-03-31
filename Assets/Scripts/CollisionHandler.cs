using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    // References to other scripts on the player
    private PlayerHealth playerHealth;
    private PlayerController playerController;

    void Start()
    {
        // Gets the PlayerHealth and PlayerController scripts attached to the same object
        playerHealth = GetComponent<PlayerHealth>();
        playerController = GetComponent<PlayerController>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Checks if the player hit an obstacle
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Removes 1 health point from the player
            playerHealth.TakeDamage(1);

            // Destroys the obstacle after collision
            Destroy(collision.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Health pickup restores 1 health point
        if (other.CompareTag("PickUp_Health"))
        {
            playerHealth.RestoreHealth(1);
            Destroy(other.gameObject);
        }

        // Speed pickup gives the player a temporary speed boost
        else if (other.CompareTag("PickUp_Speed"))
        {
            playerController.SpeedBoost(5f);
            Destroy(other.gameObject);
        }

        // Slow motion pickup slows the game down for a few seconds
        else if (other.CompareTag("PickUp_SlowMo"))
        {
            playerController.SlowMotion(4f);
            Destroy(other.gameObject);
        }
    }
}