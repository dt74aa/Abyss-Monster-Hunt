using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    public float currentExp;  // Số kinh nghiệm hiện tại
    public float maxExp = 100; // Kinh nghiệm tối đa để lên cấp
    public int level = 1;      // Cấp độ hiện tại của người chơi

    public Exp ExpBar;        // Tham chiếu đến thanh EXP
    public GameObject Weapon;

    private void Start()
    {
        currentExp = 0f;  // Bắt đầu với 0 EXP
        ExpBar.UpdateExp(currentExp, maxExp);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("exp"))
        {
            Destroy(collision.gameObject);  // Phá hủy vật phẩm EXP khi nhặt
            TangExp(10);  // Tăng 10 điểm EXP (có thể điều chỉnh)
        }
    }

    public void TangExp(float amount)
    {
        currentExp += amount;  // Tăng EXP cho người chơi

        // Kiểm tra nếu EXP đã đạt tối đa
        if (currentExp >= maxExp)
        {
            LevelUp();  // Tăng cấp
            Time.timeScale = 0f;
            Weapon.SetActive(true);
        }

        ExpBar.UpdateExp(currentExp, maxExp);  // Cập nhật thanh EXP
    }

    void LevelUp()
    {
        level++;  // Tăng cấp độ lên 1
        currentExp -= maxExp;  // Giữ lại phần dư nếu EXP vượt quá maxExp
        maxExp *= 1.2f;  // Tăng mức EXP yêu cầu cho cấp độ tiếp theo (tùy chỉnh tỷ lệ tăng)

        ExpBar.UpdateExp(currentExp, maxExp);  // Cập nhật giao diện EXP
        Debug.Log("Level Up! New Level: " + level);
    }
}
