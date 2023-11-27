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

    public void HandleDoorState()
    {
        if (GameManager.Instance.CurrentState == GameManager.GameState.MoveFree && IsLinked())
        {
            OpenDoor();
        }
        else if (GameManager.Instance.CurrentState == GameManager.GameState.Fighting)
        {
            CloseDoor();
        }
    }

    public bool IsLinked()
    {
        return linkedDoor != null;
    }

    private void OpenDoor()
    {
        // Logic to 'open' the door (e.g., deactivate, animate, etc.)
        gameObject.SetActive(false);
    }

    private void CloseDoor()
    {
        // Logic to 'close' the door
        gameObject.SetActive(true);
    }

    // Optionally, methods to set and unset linked doors
}
