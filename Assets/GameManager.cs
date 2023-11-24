using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool isFighting;

    public GameObject activeRoom;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetFightingStatus(bool status)
    {
        isFighting = status;
        UpdateDoors();
    }

    private void UpdateDoors()
    {
        DoorManager[] allDoors = FindObjectsOfType<DoorManager>();
        foreach (var door in allDoors)
        {
            if (isFighting || door.linkedDoor == null)
                door.CloseDoor();
            else
                door.OpenDoor();
        }
    }

    public void LinkDoors()
    {
        // Find all doors in the scene
        GameObject[] allDoors = GameObject.FindGameObjectsWithTag("Door");

        foreach (GameObject door in allDoors)
        {
            DoorManager doorManager = door.GetComponent<DoorManager>();
            if (doorManager != null && !doorManager.IsLinked())
            {
                GameObject closestDoor = FindClosestUnlinkedDoor(door, allDoors);
                if (closestDoor != null)
                {
                    // Link this door with the closest door
                    doorManager.SetLinkedDoor(closestDoor);
                    closestDoor.GetComponent<DoorManager>().SetLinkedDoor(door);
                }
            }
        }
    }

    private GameObject FindClosestUnlinkedDoor(GameObject door, GameObject[] allDoors)
    {
        GameObject closestDoor = null;
        float minDistance = 2.0f; // Max distance for linking doors

        foreach (GameObject otherDoor in allDoors)
        {
            if (otherDoor != door && otherDoor.GetComponent<DoorManager>() != null && !otherDoor.GetComponent<DoorManager>().IsLinked())
            {
                float distance = Vector3.Distance(door.transform.position, otherDoor.transform.position);
                if (distance < minDistance)
                {
                    closestDoor = otherDoor;
                    minDistance = distance;
                }
            }
        }

        return closestDoor;
    }

    //public void ChangeActiveRoom(GameObject newRoom)
    //{
    //    if (newRoom != activeRoom)
    //    {
    //        // Move the main camera to the CameraFixPoint of the active room
    //        Transform floorTransform = newRoom.transform.Find("Floor");
    //        Transform cameraFixPoint = floorTransform.Find("CameraFixPoint");
    //        if (cameraFixPoint != null)
    //        {
    //            Camera.main.transform.position = cameraFixPoint.position;
    //            //Camera.main.transform.rotation = cameraFixPoint.rotation;
    //        }
    //        else
    //        {
    //            Debug.LogWarning("CameraFixPoint not found in the first room.");
    //        }
    //    }
        
    //}
}
