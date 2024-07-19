using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpam : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab của kẻ địch
    public float spawnInterval = 2f; // Thời gian giữa các lần spawn
    public float spawnRadius = 10f; // Bán kính xung quanh điểm spawn để kẻ địch xuất hiện

    private void Start()
    {
        // Bắt đầu coroutine để spawn kẻ địch
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Gọi hàm SpawnEnemy
            SpawnEnemy();

            // Chờ một khoảng thời gian trước khi spawn kẻ địch tiếp theo
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        // Tạo một vị trí ngẫu nhiên xung quanh điểm spawn trong bán kính spawnRadius
        Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;

        // Tạo kẻ địch tại vị trí ngẫu nhiên
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
