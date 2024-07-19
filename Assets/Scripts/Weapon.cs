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

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        RotateGun();
        speedBuilet -= Time.deltaTime;
        if (Input.GetMouseButton(0) && speedBuilet < 0)
        {
            FireBuilet();
            if (audioManager != null)
            {
                audioManager.PlaySoundEffect(1);
            }
        }
    }

    void RotateGun()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg; // Chuyển đổi từ radian sang độ

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
        rd.AddForce(transform.right * AttackBuilet, ForceMode2D.Impulse);
    }
}
