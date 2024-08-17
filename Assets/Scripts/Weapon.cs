using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject builet;
    public Transform builetPos;
    public float SpeedBuilet;
    public float AttackBuilet;
    private float speedBuilet;
    public Transform muzzle;

    private AudioManager audioManager;

    // Biến thêm vào để tìm kẻ thù
    public float detectionRadius = 10f;  // Phạm vi tìm kẻ thù
    private Transform targetEnemy;      // Kẻ thù được nhắm đến

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        FindClosestEnemy();  // Tìm kẻ thù gần nhất
        RotateGun();         // Xoay súng về phía kẻ thù

        speedBuilet -= Time.deltaTime;

        if (targetEnemy != null && speedBuilet < 0)
        {
            FireBuilet();  // Bắn về phía kẻ thù
            if (audioManager != null)
            {
                audioManager.PlaySoundEffect(1);
            }
        }
    }

    // Tìm kẻ thù gần nhất trong phạm vi phát hiện
    void FindClosestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        float closestDistance = Mathf.Infinity;
        targetEnemy = null;

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))  // Chỉ tìm những đối tượng có tag "Enemy"
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    targetEnemy = hit.transform;  // Cập nhật kẻ thù gần nhất
                }
            }
        }
    }

    // Xoay súng về phía kẻ thù
    void RotateGun()
    {
        if (targetEnemy == null) return; // Không có kẻ thù nào để nhắm

        Vector3 enemyPos = targetEnemy.position;
        Vector2 lookDir = enemyPos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;

        // Lật súng dựa trên góc quay
        if (angle > 90 || angle < -90)
            transform.localScale = new Vector3(1, -1, 1); // Giữ lại trục z
        else
            transform.localScale = new Vector3(1, 1, 1); // Giữ lại trục z
    }

    void FireBuilet()
    {
        speedBuilet = SpeedBuilet;
        GameObject builetM = Instantiate(builet, builetPos.position, Quaternion.identity);
        Instantiate(muzzle, builetPos.position, transform.rotation, transform);
        Rigidbody2D rd = builetM.GetComponent<Rigidbody2D>();

        // Bắn đạn về phía kẻ thù
        Vector2 direction = (targetEnemy.position - builetPos.position).normalized;
        rd.AddForce(direction * AttackBuilet, ForceMode2D.Impulse);
    }

    // Vẽ phạm vi phát hiện kẻ thù
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
