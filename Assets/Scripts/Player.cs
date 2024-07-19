using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Di chuyển
    public float moveSpeed;
    private Vector3 moveInput;

    // Lăn
    public float RollAdd = 3f;
    private float RollTime;
    public float timeroll;
    private bool canRoll = true; // Kiểm tra xem có thể lăn hay không

    // Animation
    private Animator anim;
    private Rigidbody2D rd;
    public SpriteRenderer characterSR;

    private AudioManager audioManager;

    void Start()
    {
        anim = characterSR.GetComponent<Animator>();
        rd = GetComponent<Rigidbody2D>();
        audioManager = FindObjectOfType<AudioManager>();

        // Nếu bạn muốn phát âm thanh khi bắt đầu game từ Player
         if (audioManager != null)
         {
             audioManager.PlaySoundEffect(0);
         }
    }

    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        transform.position += moveInput * moveSpeed * Time.deltaTime;

        if (anim != null)
        {
            anim.SetFloat("Speed", moveInput.sqrMagnitude);

            if (Input.GetKeyDown(KeyCode.Space) && canRoll)
            {
                StartCoroutine(Roll());
            }
        }

        if (moveInput.x != 0)
        {
            if (moveInput.x > 0)
                characterSR.transform.localScale = new Vector3(1, 1, 0);
            else
                characterSR.transform.localScale = new Vector3(-1, 1, 0);
        }
    }

    private IEnumerator Roll()
    {
        anim.SetBool("Roll", true);
        moveSpeed += RollAdd;
        RollTime = timeroll;
        canRoll = false;

        yield return new WaitForSeconds(timeroll);

        anim.SetBool("Roll", false);
        moveSpeed -= RollAdd;

        yield return new WaitForSeconds(2f - timeroll);

        canRoll = true;
    }
}
