using UnityEngine;

public class RoomPopulator : MonoBehaviour
{
    public GameObject obstaclePrefab;    // The obstacle prefab
    public GameObject enemyPrefab;       // The enemy prefab
    public int maxObstacles = 10;        // Maximum number of obstacles
    public int maxEnemies = 5;           // Maximum number of enemies
    public float minDistanceBetweenObstacles = 2f;  // Minimum distance between obstacles
    public float minDistanceBetweenEnemies = 3f;
    private Bounds roomBounds;

    void Start()
    {
        MeshRenderer floorRenderer = GetComponentInChildren<MeshRenderer>();
        if (floorRenderer != null)
        {
            roomBounds = floorRenderer.bounds;
            PopulateRoom();
            SpawnEnemies();
        }
        else
        {
            Debug.LogError("No MeshRenderer (floor) found in the children of the room!");
        }
    }

    void PopulateRoom()
    {
        int createdObstacles = 0;

        for (int i = 0; i < 1000; i++) // Arbitrary large number to limit iterations
        {
            if (createdObstacles >= maxObstacles)
                break;

            Vector3 randomPosition = new Vector3(
                Random.Range(roomBounds.min.x + 1, roomBounds.max.x - 1),
                roomBounds.min.y,
                Random.Range(roomBounds.min.z + 1, roomBounds.max.z - 1)
            );

            bool canPlace = true;

            // Check for existing obstacles
            foreach (Transform child in transform)
            {
                if (child.gameObject != obstaclePrefab && Vector3.Distance(child.position, randomPosition) < minDistanceBetweenObstacles)
                {
                    canPlace = false;
                    break;
                }
            }

            if (canPlace)
            {
                // Adjust Y position to account for the obstacle's height
                MeshRenderer obstacleRenderer = obstaclePrefab.GetComponent<MeshRenderer>();
                if (obstacleRenderer)
                {
                    float halfHeight = obstacleRenderer.bounds.extents.y;
                    randomPosition.y += halfHeight;
                }

                // Instantiate with a rotation of 90 degrees around the X-axis
                Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
                Instantiate(obstaclePrefab, randomPosition, rotation, transform);
                createdObstacles++;

            }
        }
    }
    void SpawnEnemies()
    {
        int createdEnemies = 0;

        for (int i = 0; i < 1000; i++) // Arbitrary large number to limit iterations
        {
            if (createdEnemies >= maxEnemies)
                break;

            Vector3 randomPosition = new Vector3(
                Random.Range(roomBounds.min.x + 1, roomBounds.max.x - 1),
                roomBounds.min.y,
                Random.Range(roomBounds.min.z + 1, roomBounds.max.z - 1)
            );

            bool canPlace = true;

            // Check for existing obstacles and enemies
            foreach (Transform child in transform)
            {
                if (child.gameObject != enemyPrefab && Vector3.Distance(child.position, randomPosition) < minDistanceBetweenEnemies)
                {
                    canPlace = false;
                    break;
                }
            }

            if (canPlace)
            {
                // Adjust Y position to account for the obstacle's height
                MeshRenderer enemyRenderer = enemyPrefab.GetComponent<MeshRenderer>();
                if (enemyRenderer)
                {
                    float halfHeight = enemyRenderer.bounds.extents.z;
                    randomPosition.z += halfHeight;
                }

                // Instantiate with a rotation of 90 degrees around the X-axis
                Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
                Instantiate(enemyPrefab, randomPosition, rotation, transform);
                createdEnemies++;
            }
        }
    }
}