using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamExp : MonoBehaviour
{
    public GameObject Exp1Prefab;
    public GameObject Exp2Prefab;  // Prefab của đối tượng heart
    public float spawnInterval = 3f; // Thời gian giữa mỗi lần spawn
    public Vector2 mapBoundsMin;    // Góc dưới bên trái của bản đồ
    public Vector2 mapBoundsMax;    // Góc trên bên phải của bản đồ
    public int maxHearts = 100;     // Số lượng tối đa các đối tượng heart có thể xuất hiện trên bản đồ

    private int currentHeartCount = 0; // Đếm số lượng heart hiện tại trên bản đồ

    private void Start()
    {
        StartCoroutine(SpawnExp());
    }

    private IEnumerator SpawnExp()
    {
        while (true)
        {
            // Kiểm tra nếu số lượng heart hiện tại chưa đạt giới hạn
            if (currentHeartCount < maxHearts)
            {
                SpawnExp1();
                SpawnExp2();
            }

            // Chờ đợi trước khi sinh heart tiếp theo
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnExp1()
    {
        // Tạo một vị trí ngẫu nhiên trong phạm vi toàn bản đồ
        Vector2 randomPosition = new Vector2(
            Random.Range(mapBoundsMin.x, mapBoundsMax.x),
            Random.Range(mapBoundsMin.y, mapBoundsMax.y)
        );

        // Tạo đối tượng heart tại vị trí ngẫu nhiên
        Instantiate(Exp1Prefab, randomPosition, Quaternion.identity);
        currentHeartCount++; // Tăng số lượng heart hiện tại
    }
    private void SpawnExp2()
    {
        // Tạo một vị trí ngẫu nhiên trong phạm vi toàn bản đồ
        Vector2 randomPosition = new Vector2(
            Random.Range(mapBoundsMin.x, mapBoundsMax.x),
            Random.Range(mapBoundsMin.y, mapBoundsMax.y)
        );

        // Tạo đối tượng heart tại vị trí ngẫu nhiên
        Instantiate(Exp1Prefab, randomPosition, Quaternion.identity);
        currentHeartCount++; // Tăng số lượng heart hiện tại
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Nếu player chạm vào heart, phá hủy nó và giảm số lượng heart hiện tại
        if (collision.CompareTag("Player") && collision.CompareTag("exp"))
        {
            Destroy(collision.gameObject);
            currentHeartCount--;
        }
    }
}
