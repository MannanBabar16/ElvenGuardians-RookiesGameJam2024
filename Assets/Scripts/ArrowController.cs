using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {
    public static Action onSplitHit;
    public static Action onHealthHit;
    private float damage;

    public void SetDamage(float damage) {
        this.damage = damage;
    }

    public float GetDamage() {
        return damage;
    }
    void OnCollisionEnter(Collision collision) {
     
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("SplitPowerUps")) {
            onSplitHit?.Invoke();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("HealthPowerUps")) {
            onHealthHit?.Invoke();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.CompareTag("ArrowKiller")) {
            Destroy(gameObject);
        }
    }
}
