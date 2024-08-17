using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Thêm thư viện này để sử dụng UI

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

    // Cảm ứng
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;

    // Button UI cho Roll
    public Button rollButton;

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

        // Gán sự kiện nhấn nút cho rollButton
        if (rollButton != null)
        {
            rollButton.onClick.AddListener(HandleRollButtonClick);
        }
    }

    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        // Di chuyển trên điện thoại
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                touchEndPos = touch.position;

                Vector2 direction = touchEndPos - touchStartPos;
                direction.Normalize();

                moveInput = new Vector3(direction.x, direction.y, 0);
            }
        }

        // Cập nhật vị trí nhân vật
        transform.position += moveInput * moveSpeed * Time.deltaTime;

        if (anim != null)
        {
            anim.SetFloat("Speed", moveInput.sqrMagnitude);

            if (Input.GetKeyDown(KeyCode.Space) && canRoll)
            {
                StartCoroutine(Roll());
            }
        }

        // Xoay hướng nhân vật dựa trên hướng di chuyển
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

    // Phương thức cho nút RollButton
    public void HandleRollButtonClick()
    {
        if (canRoll)
        {
            StartCoroutine(Roll());
        }
    }
}
