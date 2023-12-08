using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;
    public float randomMovementRange = 5f;

    private Transform player;
    private Vector3 targetPosition;
    private float timeSinceLastTargetChange = 0f;
    public float timeBetweenTargetChanges = 4f;

    private void Start()
    {
        // Find the player object by tag. Make sure your player object has the "Player" tag.
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Check if the player object is found.
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure to tag your player object with the 'Player' tag.");
        }

        // Set an initial target position for random movement.
        SetRandomTargetPosition();
    }

    private void Update()
    {
        // Reset rotation values of x and z to 0.
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

        // Move towards the current target position.
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Rotate towards the player only on the Y-axis.
        if (player != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.y = 0f; // Ensure rotation only happens on the Y-axis
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Check if it's time to change the target position.
        timeSinceLastTargetChange += Time.deltaTime;
        if (timeSinceLastTargetChange >= timeBetweenTargetChanges)
        {
            // Set a new random target position.
            SetRandomTargetPosition();
            timeSinceLastTargetChange = 0f; // Reset the timer.
        }
    }

    private void SetRandomTargetPosition()
    {
        // Generate a new random position within the specified range.
        Vector3 randomDirection = Random.insideUnitCircle.normalized * randomMovementRange;
        targetPosition = transform.position - new Vector3(randomDirection.x, 0f, randomDirection.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the player (you might need to adjust the tag or layer).
        if (collision.gameObject.CompareTag("Player"))
        {
            // If collided with the player, set a new random target position opposite to the collision point.
            SetRandomTargetPosition();
        }
    }
}