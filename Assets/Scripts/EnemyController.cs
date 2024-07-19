using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  

    //máuu enemy
    public float currentHealth;
    public float maxHealth;

    public EnemyHealth healthEnemy;
    private void Start()
    {
        currentHealth = maxHealth;

        healthEnemy.UpdateEnemyHealth(currentHealth, maxHealth);
    }

    public void TakeDamageEnemy(float damage)
    {
        // Giảm máu hiện tại bởi lượng sát thương
        currentHealth -= damage;

        // Nếu máu hiện tại nhỏ hơn 0, đặt lại thành 0
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        // Cập nhật thanh máu sau khi nhận sát thương
        healthEnemy.UpdateEnemyHealth(currentHealth, maxHealth);
    }
}
