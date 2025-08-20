using UnityEngine;

public class FlyWander : MonoBehaviour
{
    public Transform playerHead;      // Assign this to the camera/player's head
    public Vector3 offsetFromPlayerHead = Vector3.zero; // Offset from player head
    public float wanderRadius = 0.7f; // How far from the player the fly can wander
    public float wanderSpeed = 1.5f;  // How fast the fly moves
    public float jitter = 0.3f;       // How "twitchy" the movement is
    public float flyHeight = 1.6f;    // Approx. head-height in meters

    private Vector3 currentTarget;

    void Start()
    {
        if (playerHead == null)
            playerHead = Camera.main.transform;  // fallback
        Camera cam = Object.FindFirstObjectByType<Camera>();
        playerHead = cam.transform;
        PickNewTarget();
    }

    void Update()
    {
        // Occasionally pick a new target
        if (Vector3.Distance(transform.position, currentTarget) < 0.16f || Random.value < 0.01f)
        {
            PickNewTarget();
        }

        // Move toward current target with jitter
        Vector3 dir = (currentTarget - transform.position).normalized;
        dir += Random.insideUnitSphere * jitter;  // Adds twitchiness
        dir = dir.normalized;

        transform.position += dir * wanderSpeed * Time.deltaTime;
        transform.LookAt(playerHead); // Optional: always face player
    }

    void PickNewTarget()
    {
        // Center with user-defined offset
        Vector3 center = playerHead.position + offsetFromPlayerHead + playerHead.forward * (wanderRadius * 0.6f);
        center.y = playerHead.position.y + offsetFromPlayerHead.y + flyHeight - 1.6f; // Adjust Y for offset if needed

        // Random point in the area
        Vector2 circle = Random.insideUnitCircle * wanderRadius;
        Vector3 randomOffset = playerHead.right * circle.x + playerHead.up * circle.y;

        currentTarget = center + randomOffset;
    }
    void OnDrawGizmosSelected()
    {
        if (playerHead != null)
        {
            // Apply the offset just like in PickNewTarget
            Vector3 center = playerHead.position + offsetFromPlayerHead + playerHead.forward * (wanderRadius * 0.6f);
            center.y = playerHead.position.y + offsetFromPlayerHead.y + flyHeight - 1.6f; // Match target logic

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(center, wanderRadius);
        }
    }
}