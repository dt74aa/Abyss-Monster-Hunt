using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAl : MonoBehaviour
{
    public Seeker seeker;
    Path path;
    Transform target;
    Coroutine moveCoroutine;
    public float moveSpeed;
    public float nextWPDistance;
    public SpriteRenderer characterSR;
    bool reachDestination = false;
    public bool roaming = true;
    public bool updateContinuesPath;


    // bắn
    public bool isShootable = false;
    public GameObject bullet;
    public float bulletSpeed;
    public float timeBtwFire;
    private float fireCoolDown;

    //va chạm người chơi
    PlayerHealth playerHealth; // Biến để lưu trữ tham chiếu đến PlayerHealth
    public float damageAmount = 10f; // Lượng sát thương gây ra cho người chơi


 


    private void Start()
    {
        if (seeker == null)
        {
            Debug.LogError("Seeker không được gán trong Inspector");
            return;
        }

        InvokeRepeating("CalculatePath", 0f, 0.5f);
        reachDestination = true;
    }

    private void Update()
    {
        if (isShootable)
        {
            fireCoolDown -= Time.deltaTime;

            if (fireCoolDown < 0)
            {
                fireCoolDown = timeBtwFire;
                EnemyFireBullet();
            }
        }
    }

    void EnemyFireBullet()
    {
        if (bullet == null)
        {
            Debug.LogError("Bullet prefab chưa được gán trong Inspector");
            return;
        }

        var bullettmp = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D rd = bullettmp.GetComponent<Rigidbody2D>();
        Vector3 playerPos = FindObjectOfType<Player>().transform.position;
        Vector3 direction = playerPos - transform.position;
        rd.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
    }

    void CalculatePath()
    {
        Vector2 target = FindTarget();

        if (seeker.IsDone() && (reachDestination || updateContinuesPath))
        {
            seeker.StartPath(transform.position, target, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (p.error)
        {
            Debug.LogError("Pathfinding error: " + p.errorLog);
            return;
        }
        path = p;

        MoveToTarget();
    }

    void MoveToTarget()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }

    IEnumerator MoveToTargetCoroutine()
    {
        int currentWP = 0;
        reachDestination = false;
        while (currentWP < path.vectorPath.Count)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - (Vector2)transform.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;

            transform.position += (Vector3)force;

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);
            if (distance < nextWPDistance)
            {
                currentWP++;
            }

            yield return null;
        }

        reachDestination = true;
    }

    Vector2 FindTarget()
    {
        Player player = FindObjectOfType<Player>();
        if (player == null)
        {
            Debug.LogError("Không tìm thấy đối tượng Player trong cảnh");
            return Vector2.zero;
        }

        Vector3 playerPos = player.transform.position;

        if (roaming)
        {
            return (Vector2)playerPos + (Random.Range(1f, 5f) * new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized);
        }
        else
        {
            return playerPos;
        }
    }



    //va chạm 
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
