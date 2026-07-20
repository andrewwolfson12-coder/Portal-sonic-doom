using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed = 25f; // top linear speed
    public float accel = 60f;    // how fast we reach target speed
    public float airControl = 0.5f;
    public float jumpForce = 7f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckDistance = 0.15f;
    public LayerMask groundMask;

    [Header("Boosters")]
    public float boosterMultiplier = 1.8f;

    Rigidbody rb;
    Vector3 inputMove;
    bool wantsJump;
    bool onGround;

    // Teleport immunity - handled by Teleportable component on player
    Teleportable teleportable;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        teleportable = GetComponent<Teleportable>();
    }

    void Update()
    {
        // Desktop input (WASD / arrow keys)
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // For mobile you can set inputMove from a virtual joystick script that writes to this API
        inputMove = new Vector3(h, 0f, v).normalized;

        if (Input.GetButtonDown("Jump")) wantsJump = true;
    }

    void FixedUpdate()
    {
        CheckGround();

        Vector3 localTargetVel = transform.TransformDirection(inputMove) * maxSpeed;
        Vector3 currentVel = rb.velocity;

        // Preserve vertical velocity
        Vector3 targetVel = new Vector3(localTargetVel.x, currentVel.y, localTargetVel.z);

        float control = onGround ? 1f : airControl;
        Vector3 newVel = Vector3.MoveTowards(currentVel, targetVel, accel * control * Time.fixedDeltaTime);
        rb.velocity = newVel;

        if (wantsJump && onGround)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            wantsJump = false;
        }
    }

    void CheckGround()
    {
        RaycastHit hit;
        Vector3 origin = groundCheck != null ? groundCheck.position : transform.position;
        if (Physics.SphereCast(origin, 0.25f, Vector3.down, out hit, groundCheckDistance + 0.1f, groundMask, QueryTriggerInteraction.Ignore))
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
    }

    // Called by booster triggers in the scene to apply a speed boost (e.g., rings/launchers)
    public void ApplyBooster(Vector3 direction, float force)
    {
        rb.AddForce(direction.normalized * force * boosterMultiplier, ForceMode.VelocityChange);
        // Clamp to max speed to avoid runaway
        Vector3 horiz = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (horiz.magnitude > maxSpeed * 2f)
        {
            Vector3 clamped = horiz.normalized * maxSpeed * 2f;
            rb.velocity = new Vector3(clamped.x, rb.velocity.y, clamped.z);
        }
    }

    // Public API used by portal teleporter to set position/velocity safely
    public void TeleportTo(Vector3 position, Quaternion rotation, Vector3 velocity)
    {
        rb.position = position;
        rb.rotation = rotation;
        rb.velocity = velocity;

        // reset interpolation to avoid visual jitter
        rb.Sleep();
        rb.WakeUp();

        // notify teleportable
        if (teleportable != null) teleportable.OnTeleported();
    }
}
