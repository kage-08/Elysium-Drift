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
    [SerializeField] private float crouchHeight = 1.0f;
    [SerializeField] private float standHeight = 1.8f;
    [SerializeField] private float crouchSpeedMultiplier = 0.5f;

    private Rigidbody rb;
    private PlayerStatus status;

    private bool isDash;
    private bool isJump;
    private bool isCrouch;
    private float pitch;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        status = GetComponent<PlayerStatus>();
        rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        HandleDash();
        HandleCrouch();
        HandleJump();
    }

    void FixedUpdate()
    {
        HandleMove();
    }

    private void HandleMove()
    {
        float speed = moveSpeed;
        if (isDash) speed *= dashMultiplier;
        if (isCrouch) speed *= crouchSpeedMultiplier;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 move = (transform.forward * v + transform.right * h).normalized;

        rb.linearVelocity = new Vector3(
            move.x * speed,
            rb.linearVelocity.y,
            move.z * speed
        );
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    private void HandleDash()
    {
        if (Input.GetKey(KeyCode.LeftShift) && status.CanDash())
        {
            isDash = true;
            status.ConsumeStamina();
        }
        else
        {
            isDash = false;
            status.RecoverStamina();
        }
    }

    private void HandleJump()
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

    private void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouch = !isCrouch;
            Vector3 pos = cameraPivot.localPosition;
            pos.y = isCrouch ? crouchHeight : standHeight;
            cameraPivot.localPosition = pos;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isJump = false;
    }
}
