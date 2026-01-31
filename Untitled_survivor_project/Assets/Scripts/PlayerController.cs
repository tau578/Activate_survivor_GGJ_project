using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cameraTransform;

    [Header("Movement")]
    public float moveSpeed = 4.5f;
    public float mouseSensitivity = 130f;

    [Header("Collision")]
    public float skin = 0.08f;                // small offset to avoid sticking
    public LayerMask collisionMask = ~0;      // which layers block movement
    public float capsuleRadiusMinus = 0.02f;  // shrink radius for casts a bit

    private Rigidbody rb;
    private CapsuleCollider capsule;

    // cached input per frame
    private Vector2 inputMove; // x=Horizontal, y=Vertical
    private float yaw;         // we keep only yaw (left-right), no pitch

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();

        // Rigidbody setup for a character-like controller
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.freezeRotation = true; // freeze all rotation to avoid jitter
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Force the camera to look straight ahead (no pitch)
        if (cameraTransform != null)
            cameraTransform.localRotation = Quaternion.identity;

        // Optional: ensure CapsuleCollider uses a zero-friction PhysicMaterial
        // Assign via Inspector.
    }

    void Update()
    {
        // --- Read movement input in Update ---
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        inputMove = new Vector2(h, v).normalized;

        // --- Read mouse input, but only apply yaw (no pitch) ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        yaw += mouseX;

        // Apply yaw to body; camera pitch stays zero (classic boomer shooter)
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);

        // Ensure camera has no local pitch/roll at any time
        if (cameraTransform != null)
            cameraTransform.localRotation = Quaternion.identity;
    }

    void FixedUpdate()
    {
        HandleMovementPhysics();
    }

    void HandleMovementPhysics()
    {
        // Desired world-space direction using body forward/right
        Vector3 fwd = transform.forward;
        Vector3 right = transform.right;
        Vector3 desired = (right * inputMove.x + fwd * inputMove.y).normalized;

        // If no input, do not push into walls; keep gravity on Y
        if (desired.sqrMagnitude < 0.0001f)
        {
            // kill horizontal velocity to avoid micro-jitter near walls
            Vector3 vel = rb.linearVelocity;
            rb.linearVelocity = new Vector3(0f, vel.y, 0f);
            return;
        }

        // Slide along obstacles instead of pushing into them
        Vector3 projected = ProjectAlongObstacles(desired);

        // Move using MovePosition for smooth physics cooperation
        Vector3 targetPos = rb.position + projected * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(targetPos);
    }

    Vector3 ProjectAlongObstacles(Vector3 desired)
    {
        // CapsuleCast slightly ahead; if a wall is in front, project along its plane
        float radius = Mathf.Max(0.01f, capsule.radius - capsuleRadiusMinus);
        float halfHeight = Mathf.Max(radius, capsule.height * 0.5f - radius);

        Vector3 worldCenter = rb.position + capsule.center;
        Vector3 p1 = worldCenter + Vector3.up * (halfHeight - radius);
        Vector3 p2 = worldCenter - Vector3.up * (halfHeight - radius);

        float castDist = skin + 0.02f;
        if (Physics.CapsuleCast(p1, p2, radius, desired, out RaycastHit hit, castDist, collisionMask, QueryTriggerInteraction.Ignore))
        {
            // Remove the component into the normal to create sliding movement
            Vector3 slide = Vector3.ProjectOnPlane(desired, hit.normal).normalized;
            return slide;
        }

        return desired;
    }
}