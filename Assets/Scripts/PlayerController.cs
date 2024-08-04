using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public float maxHealth = 100f;
    public float damage;
    public Slider healthBar;
    public static System.Action<PlayerController> OnPlayerClicked;
    private float currentHealth;
    public TextMeshProUGUI healthText;

    void Start() {
        currentHealth = maxHealth;
        healthBar.minValue = 0;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        healthText.text = currentHealth.ToString();

    }

    public void Initialize(float initialHealth, float initialDamage) {
        maxHealth = initialHealth;
        currentHealth = initialHealth;
        damage = initialDamage;
        healthBar.minValue = 0;
        healthBar.maxValue = initialHealth;
        healthBar.value = initialHealth;
    }

    public void SetPlayer2() {
        Initialize(80, 40);
    }

    public void SetPlayer3() {
        Initialize(60, 30);
    }

    public void SetPlayer4() {
        Initialize(40, 20);
    }

    
    private void PlayerDie() {
        Destroy(gameObject);
    }

    public void TakeDamage(float damageAmount) {
        currentHealth -= damageAmount;
        healthBar.value = currentHealth;
        healthText.text = currentHealth.ToString();

        if (currentHealth <= 0) {
            PlayerDie();
        }
    }

    void OnMouseDown() {
        if (OnPlayerClicked != null) {
            OnPlayerClicked(this);
            Debug.Log("PlayerClicked");
        }
    }

    public void IncreaseHealth(float amount) {
        currentHealth += amount;
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
        healthBar.value = currentHealth;
        healthText.text= currentHealth.ToString();
    }

    public void SetArrowDamage() {
        
    }
}
