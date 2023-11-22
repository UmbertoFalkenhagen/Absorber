using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject roomPrefab;    // Assign your room prefab in the inspector
    public int gridWidth = 5;        // Width of the grid
    public int gridHeight = 5;       // Height of the grid

    private float roomWidth;         // Width of each room
    private float roomHeight;        // Height of each room

    void Start()
    {
        CalculateRoomDimensions();
        GenerateGrid();
    }

    void CalculateRoomDimensions()
    {
        if (roomPrefab != null)
        {
            Renderer renderer = roomPrefab.GetComponentInChildren<Renderer>();
            if (renderer != null)
            {
                Bounds bounds = renderer.bounds;
                roomWidth = bounds.size.x;
                roomHeight = bounds.size.z;
            }
            else
            {
                Debug.LogError("Room prefab does not have a Renderer component.");
            }
        }
        else
        {
            Debug.LogError("Room prefab is not assigned.");
        }
    }

    void GenerateGrid()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 roomPosition = new Vector3(x * roomWidth, 0, y * roomHeight);
                Instantiate(roomPrefab, roomPosition, Quaternion.identity);
            }
        }
    }
}
