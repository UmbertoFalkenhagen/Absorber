using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject linkedDoor;
    public bool isOpen = false;
    public Material openMat;
    public Material closedMat;

    private RoomManager roomManager;
    private Renderer objectRenderer;
    private Collider coll;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsLinked()
    {
        return linkedDoor != null;
    }

    public void SetLinkedDoor(GameObject otherDoor)
    {
        linkedDoor = otherDoor;
    }

    public void OpenDoor()
    {
        if (linkedDoor != null)
        {
            gameObject.SetActive(false); // Hides the door
            linkedDoor.SetActive(false); // Hides the linked door
        }
    }

    public void CloseDoor()
    {
        gameObject.SetActive(true); // Shows the door
        if (linkedDoor != null)
        {
            linkedDoor.SetActive(true); // Shows the linked door
        }
    }
}
