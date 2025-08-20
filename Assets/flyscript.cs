using UnityEngine;
using UnityEngine.UI;

public class FlyController : MonoBehaviour
{
    [Header("Fly Movement Settings")]
    public Transform playerHead;                  // Assign manually or will default to main camera
    public Vector3 offsetFromPlayerHead = Vector3.zero;
    public float wanderRadius = 0.7f;
    public float wanderSpeed = 1.5f;
    public float jitter = 0.3f;
    public float flyHeight = 1.6f;

    [Header("Fly Respawn & UI")]
    public GameObject flyPrefab;                  // Assign your Fly prefab
    public Text scoreText;                        // Assign a UI Text
    public int score = 0;

    private Vector3 currentTarget;
    private bool rightHandIn = false;
    private bool leftHandIn = false;

    private static FlyController currentFly;

    void Start()
    {
        if (playerHead == null)
            playerHead = Camera.main.transform;

        if (currentFly != null && currentFly != this)
        {
            Destroy(gameObject);  // Only one fly at a time
            return;
        }

        currentFly = this;
        PickNewTarget();
    }

    void Update()
    {
        // Fly movement
        if (Vector3.Distance(transform.position, currentTarget) < 0.1f || Random.value < 0.01f)
        {
            PickNewTarget();
        }

        Vector3 dir = (currentTarget - transform.position).normalized;
        dir += Random.insideUnitSphere * jitter;
        dir.Normalize();

        transform.position += dir * wanderSpeed * Time.deltaTime;
        transform.LookAt(playerHead);

        // Hand check
        if (leftHandIn && rightHandIn)
        {
            KillAndRespawn();
        }
    }

    void PickNewTarget()
    {
        Vector3 center = playerHead.position + offsetFromPlayerHead + playerHead.forward * (wanderRadius * 0.6f);
        center.y = playerHead.position.y + offsetFromPlayerHead.y + flyHeight - 1.6f;

        Vector2 circle = Random.insideUnitCircle * wanderRadius;
        Vector3 randomOffset = playerHead.right * circle.x + playerHead.up * circle.y;

        currentTarget = center + randomOffset;
    }

    void KillAndRespawn()
    {
        score++;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        // Reset hand flags
        rightHandIn = false;
        leftHandIn = false;

        // Spawn new fly
        if (flyPrefab != null)
        {
            Instantiate(flyPrefab, GetSpawnPosition(), Quaternion.identity);
        }

        Destroy(gameObject);  // Destroy this fly
    }

    Vector3 GetSpawnPosition()
    {
        Vector3 center = playerHead.position + offsetFromPlayerHead + playerHead.forward * (wanderRadius * 0.6f);
        center.y = playerHead.position.y + offsetFromPlayerHead.y + flyHeight - 1f;
        return center + Random.insideUnitSphere * wanderRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("leftHand")) leftHandIn = true;
        if (other.CompareTag("rightHand")) rightHandIn = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("leftHand")) leftHandIn = false;
        if (other.CompareTag("rightHand")) rightHandIn = false;
    }

    void OnDrawGizmosSelected()
    {
        if (playerHead != null)
        {
            Vector3 center = playerHead.position + offsetFromPlayerHead + playerHead.forward * (wanderRadius * 0.6f);
            center.y = playerHead.position.y + offsetFromPlayerHead.y + flyHeight - 1.6f;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(center, wanderRadius);
        }
    }
}