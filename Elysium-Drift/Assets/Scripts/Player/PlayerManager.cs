using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashMultiplier = 1.8f;
    [SerializeField] private float jumpPower = 5f;

    [Header("Camera")]
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private float mouseSensitivity = 2.5f;
    [SerializeField] private float minPitch = -80f;
    [SerializeField] private float maxPitch = 80f;

    [Header("Crouch")]
    [SerializeField] private float crouchHeight = 0f;
    [SerializeField] private float standHeight = 1f;
    [SerializeField] private float crouchSpeedMultiplier = 0.5f;

    private Rigidbody rb;

    private bool isDash;
    private bool isJump;
    private bool isCrouch;

    private float pitch; // 上下視点用

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Rigidbodyで回転しないように

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        HandleDashInput();
        HandleCrouchInput();
        HandleJumpInput();
    }

    void FixedUpdate()
    {
        HandleMove();
    }

    // =====================
    // 移動（慣性なし）
    // =====================
    private void HandleMove()
    {
        float speed = moveSpeed;

        if (isDash)
            speed *= dashMultiplier;

        if (isCrouch)
            speed *= crouchSpeedMultiplier;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 moveDir =
            transform.forward * v +
            transform.right * h;

        Vector3 velocity = moveDir.normalized * speed;
        velocity.y = rb.linearVelocity.y; // Y方向はジャンプ用に保持

        rb.linearVelocity = velocity;
    }

    // =====================
    // 視点操作（一人称・上下対応）
    // =====================
    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        // 左右（プレイヤー本体）
        transform.Rotate(Vector3.up * mouseX);

        // 上下（カメラ）
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    // =====================
    // ジャンプ
    // =====================
    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            rb.linearVelocity = new Vector3(
                rb.linearVelocity.x,
                jumpPower,
                rb.linearVelocity.z
            );
            isJump = true;
        }
    }

    // =====================
    // ダッシュ
    // =====================
    private void HandleDashInput()
    {
        isDash = Input.GetKey(KeyCode.LeftShift);
    }

    // =====================
    // しゃがみ（トグル）
    // =====================
    private void HandleCrouchInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouch = !isCrouch;

            Vector3 camPos = cameraPivot.localPosition;
            camPos.y = isCrouch ? crouchHeight : standHeight;
            cameraPivot.localPosition = camPos;
        }
    }

    // =====================
    // 着地判定
    // =====================
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
        }
    }
}
