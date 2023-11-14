using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject roomPrefab;
    public Camera mainCamera;
    public int minRoomNumber;
    public int maxRoomNumber;

    private List<GameObject> rooms = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateLevel()
    {
        int roomsToCreate = Random.Range(minRoomNumber, maxRoomNumber + 1);
        Vector3 nextRoomPosition = Vector3.zero; // Start at the origin

        for (int i = 0; i < roomsToCreate; i++)
        {
            GameObject newRoom = Instantiate(roomPrefab, nextRoomPosition, Quaternion.identity);
            rooms.Add(newRoom);

            if (i < roomsToCreate - 1) // If not the last room
            {
                nextRoomPosition = DecideNextRoomPosition(newRoom);
            }
        }
    }

    Vector3 DecideNextRoomPosition(GameObject currentRoom)
    {
        // Logic to decide the position of the next room
        // You can use currentRoom's bounds to calculate the position of the next room
        // Example: return currentRoom.transform.position + new Vector3(roomWidth, 0, 0); // For eastward room

        return Vector3.zero;
    }
}
