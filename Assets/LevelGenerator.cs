using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject roomPrefab;
    public int minRoomNumber = 8;
    public int maxRoomNumber = 12;

    private List<GameObject> placedRooms = new List<GameObject>();
    private float roomWidth;    // Width of the room prefab
    private float roomHeight;   // Height of the room prefab

    [System.Obsolete]
    void Start()
    {
        
    }

    public void GenerateLevel()
    {
        CalculateRoomDimensions();

        int roomsToCreate = Random.Range(minRoomNumber, maxRoomNumber + 1);
        Vector3 initialPosition = Vector3.zero; // Starting at the origin
        GameObject firstRoom = Instantiate(roomPrefab, initialPosition, Quaternion.identity);
        placedRooms.Add(firstRoom);

        for (int i = placedRooms.Count; i < roomsToCreate; i++)
        {
            GameObject newRoom = PlaceRoomNextTo(placedRooms[Random.Range(0, placedRooms.Count)]);
            if (newRoom != null)
            {
                placedRooms.Add(newRoom);
            }
            else
            {
                // If no placement was possible, break early
                break;
            }
        }

        LinkRoomDoors();

        PositionPlayerAndCamera();
    }

    void CalculateRoomDimensions()
    {
        Transform floorTransform = roomPrefab.transform.Find("Floor");
        if (floorTransform != null)
        {
            Renderer floorRenderer = floorTransform.GetComponent<Renderer>();
            if (floorRenderer != null)
            {
                roomWidth = floorRenderer.bounds.size.x;
                roomHeight = floorRenderer.bounds.size.z;
            }
            else
            {
                Debug.LogError("Floor object does not have a Renderer component.");
            }
        }
        else
        {
            Debug.LogError("Floor object not found in the room prefab.");
        }
    }

    void PositionPlayerAndCamera()
    {
        // Move the player to the center of the first room
        GameObject player = GameObject.FindWithTag("Player");
        Transform floorTransform = placedRooms[0].transform.Find("Floor");
        if (player != null && placedRooms.Count > 0)
        {


            //Debug.Log(floorTransform.name);
            if (floorTransform != null)
            {
                player.transform.position = floorTransform.Find("PlayerFixPoint").position;
            }
            else
            {
                Debug.LogWarning("Floor object not found in the first room.");
            }
        }
        else
        {
            Debug.LogWarning("Player object not found or no rooms placed.");
        }

        // Move the main camera to the CameraFixPoint of the first room
        Transform cameraFixPoint = floorTransform.Find("CameraFixPoint");
        if (cameraFixPoint != null)
        {
            Camera.main.transform.position = cameraFixPoint.position;
            //Camera.main.transform.rotation = cameraFixPoint.rotation;
        }
        else
        {
            Debug.LogWarning("CameraFixPoint not found in the first room.");
        }
    }

    GameObject PlaceRoomNextTo(GameObject existingRoom)
    {
        List<int> directions = new List<int> { 0, 1, 2, 3 }; // N, S, E, W
        Shuffle(directions); // Randomize the order of directions

        foreach (int direction in directions)
        {
            Vector3 newPosition = CalculateNewPosition(existingRoom.transform.position, direction);
            if (IsPositionFree(newPosition))
            {
                return Instantiate(roomPrefab, newPosition, Quaternion.identity);
            }
        }

        return null; // No free position found in any direction
    }

    private void LinkRoomDoors()
    {
        DoorManager[] allDoors = FindObjectsOfType<DoorManager>();

        foreach (var door in allDoors)
        {
            if (!door.IsLinked())
            {
                foreach (var otherDoor in allDoors)
                {
                    if (otherDoor != door && !otherDoor.IsLinked() && Vector3.Distance(door.transform.position, otherDoor.transform.position) < 2f)
                    {
                        door.linkedDoor = otherDoor.gameObject;
                        otherDoor.linkedDoor =door.gameObject;
                        break; // Stop looking for another door once a link is established
                    }
                }
            }
        }
    }

    Vector3 CalculateNewPosition(Vector3 existingPosition, int direction)
    {
        switch (direction)
        {
            case 0: // North
                return new Vector3(existingPosition.x, existingPosition.y, existingPosition.z + roomHeight);
            case 1: // South
                return new Vector3(existingPosition.x, existingPosition.y, existingPosition.z - roomHeight);
            case 2: // East
                return new Vector3(existingPosition.x + roomWidth, existingPosition.y, existingPosition.z);
            case 3: // West
                return new Vector3(existingPosition.x - roomWidth, existingPosition.y, existingPosition.z);
            default:
                return existingPosition;
        }
    }

    bool IsPositionFree(Vector3 position)
    {
        foreach (GameObject room in placedRooms)
        {
            if (Vector3.Distance(room.transform.position, position) < Mathf.Min(roomWidth, roomHeight))
            {
                return false; // Position is already taken
            }
        }
        return true; // Position is free
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
