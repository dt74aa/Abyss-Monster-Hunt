using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public HealthBar healthBar;

    private void Start()
    {
        currentHealth = maxHealth;

        healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        // Giảm máu hiện tại bởi lượng sát thương
        currentHealth -= damage;

        // Nếu máu hiện tại nhỏ hơn 0, đặt lại thành 0
        if (currentHealth <= 0)
        {
           Destroy(gameObject);
        }

        // Cập nhật thanh máu sau khi nhận sát thương
        healthBar.UpdateHealth(currentHealth, maxHealth);
    }
}
