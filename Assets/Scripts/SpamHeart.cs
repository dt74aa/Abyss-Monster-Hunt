using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamHeart : MonoBehaviour
{

    public GameObject heartPrefab;  // Prefab của đối tượng heart
    public float spawnInterval = 5f; // Thời gian giữa mỗi lần spawn
    public Vector2 spawnAreaMin;     // Góc dưới bên trái của khu vực spawn
    public Vector2 spawnAreaMax;     // Góc trên bên phải của khu vực spawn

    private void Start()
    {
        // Gọi hàm SpawnHeart mỗi 'spawnInterval' giây, bắt đầu sau 0 giây
        InvokeRepeating("SpawnHeart", 0f, spawnInterval);
    }

    private void SpawnHeart()
    {
        // Tạo một vị trí ngẫu nhiên trong phạm vi đã định
        Vector2 randomPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        // Tạo đối tượng heart tại vị trí ngẫu nhiên
        Instantiate(heartPrefab, randomPosition, Quaternion.identity);
    }
}

