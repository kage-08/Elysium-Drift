using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 6f;
    public float jumpPower = 5f;

    [Header("Dash")]
    public float dashMultiplier = 2f;
    public float dashStaminaCost = 20f;

    [Header("Look")]
    public float mouseSensitivity = 3f;
    public Transform cameraPivot;

    [Header("Crouch")]
    public float crouchHeight = 1f;
    public float standHeight = 1.8f;

    Rigidbody rb;
    PlayerStatus status;

    bool isGrounded;
    bool isDash;
    bool isCrouch;

    float rotX;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        status = GetComponent<PlayerStatus>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Look();
        DashInput();
        CrouchInput();
        Jump();
    }

    void FixedUpdate()
    {
        Move();
        RecoverStamina();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        float speed = moveSpeed;

        if (isDash && status.stamina > 0f)
        {
            speed *= dashMultiplier;
            status.stamina -= dashStaminaCost * Time.fixedDeltaTime;
        }

        Vector3 dir = transform.forward * v + transform.right * h;
        rb.linearVelocity = new Vector3(dir.x * speed, rb.linearVelocity.y, dir.z * speed);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void DashInput()
    {
        isDash = Input.GetKey(KeyCode.LeftShift) && status.stamina > 0f;
    }

    void CrouchInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouch = !isCrouch;
            cameraPivot.localPosition =
                new Vector3(0, isCrouch ? crouchHeight : standHeight, 0);
        }
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, -85f, 85f);

        cameraPivot.localRotation = Quaternion.Euler(rotX, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    void RecoverStamina()
    {
        if (!isDash)
        {
            status.stamina += 30f * Time.fixedDeltaTime;
            status.stamina = Mathf.Min(status.stamina, status.maxStamina);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }
}
