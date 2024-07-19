using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    EnemyController enemyControll; // Biến để lưu trữ tham chiếu
    public float damageAmount = 10f; // Lượng sát thương gây ra


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyControll = collision.GetComponent<EnemyController>();

        
            if (enemyControll != null)
            {
                enemyControll.TakeDamageEnemy(damageAmount);
                Destroy(this.gameObject);
            }
        }
    }
}
