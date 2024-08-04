using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
    public float damage = 20f;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null) {
                playerController.TakeDamage(damage);
            }
            Destroy(gameObject); // Destroy the bullet
        }

        if (collision.gameObject.CompareTag("Tower")) {
            Destroy(gameObject);
        }
    }
}
