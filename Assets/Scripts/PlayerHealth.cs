using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public HealthBar healthBar;
    //menu
    public GameObject MenuEndGame;

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
            MenuEndGame.SetActive(true);
        }

        // Cập nhật thanh máu sau khi nhận sát thương
        healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("mau"))
        {
            AnMau anMauScript = collision.gameObject.GetComponent<AnMau>();  // Lấy tham chiếu đến script AnMau từ đối tượng va chạm
            if (anMauScript != null)
            {
                anMauScript.AnMauHoi();  // Gọi hàm AnMauHoi() từ script AnMau
                HoiMau((int)(maxHealth / 10));  // Hồi 1/10 máu
            }
            Destroy(collision.gameObject);  // Xóa đối tượng máu
        }
    }

    public void HoiMau(int amount)
    {
        currentHealth += amount;  // Hồi thêm máu
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;  // Giới hạn máu tối đa
        }
        healthBar.UpdateHealth(currentHealth, maxHealth);
    }
}
