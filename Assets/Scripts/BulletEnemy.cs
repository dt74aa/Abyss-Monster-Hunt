using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    PlayerHealth playerHealth; // Biến để lưu trữ tham chiếu đến PlayerHealth
    public float damageAmount = 10f; // Lượng sát thương gây ra cho người chơi


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu đối tượng va chạm có tag là "Player"
        if (collision.CompareTag("Player"))
        {
            // Lấy component PlayerHealth từ đối tượng va chạm
            playerHealth = collision.GetComponent<PlayerHealth>();

            // Nếu tìm thấy PlayerHealth, gây sát thương cho người chơi
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                Destroy(this.gameObject);
            }
        }


    }
}
