using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {
    public float speed=2;
    public float shootInterval = 4f;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    public float maxHealth = 100f;
    public Slider healthBar;
    private Transform target;
    private bool canShoot = false;
    private float currentHealth;
    public TextMeshProUGUI healthText;
    public float damage = 20f;


    void Start() {
        target = GameObject.FindWithTag("Tower").transform;
        currentHealth = maxHealth;
        healthText.text = currentHealth.ToString();

        if (healthBar != null) {
            healthBar.minValue = 0;
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
        }
        StartCoroutine(Shoot());
    }

    void Update() {
        if (target != null) {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }

     
        if (healthBar != null) {
            Vector3 healthBarPos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2, 0));
            healthBar.transform.position = healthBarPos;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Tower")) {
            // Put Game over screen here
            SceneManager.LoadScene("Level 1");
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Arrow")) {
            TakeDamage(damage);
            Destroy(collision.gameObject);
            Destroy(gameObject);

        }
    }

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("EnemyShootLine")) {
            canShoot = true;
        }
    }

    IEnumerator Shoot() {
        while (true) {
            if (canShoot) {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.layer = LayerMask.NameToLayer("EnemyBullets");
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.velocity = -transform.forward * bulletSpeed;
            }
            yield return new WaitForSeconds(shootInterval);
        }
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        healthText.text = currentHealth.ToString();

        if (healthBar != null) {
            healthBar.value = currentHealth;
        }
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }

    }
}
