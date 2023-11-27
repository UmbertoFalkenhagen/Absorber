using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public bool hasInteractableObjects;
    public string layerName; // The layer name we are interested in
    public int layer;
    public List<GameObject> objectsInLayer;

    private Collider roomCollider;

    private void Start()
    {
        // Initialize hasInteractableObjects based on the presence of interactable objects in this room
        roomCollider = GetComponent<Collider>();
        layer = LayerMask.NameToLayer(layerName);
    }

    private void Update()
    {
        // Optionally, you can continuously check for the presence of interactable objects
        // and update the game state accordingly
        UpdateRoomState();
    }

    private void UpdateRoomState()
    {
        if (!CheckForInteractableObjects() && CheckPlayerPosition())
        {
            // If no interactable objects left, update the game state to MoveFree
            GameManager.Instance.ChangeState(GameManager.GameState.MoveFree);
        }
        else if (CheckPlayerPosition() && CheckForInteractableObjects())
        {

            // If the player is in the room and there are interactable objects, update to Fighting state
            GameManager.Instance.ChangeState(GameManager.GameState.Fighting);
        }
    }

    private bool CheckForInteractableObjects()
    {
        // Implement logic to check for interactable objects within the room.
        // This could be done by checking the number of children with a specific tag or component
        // Return true if interactable objects are present, false otherwise
        objectsInLayer = new List<GameObject>();

        foreach (Transform child in transform)
        {
            if (child.gameObject.layer == layer)
            {
                objectsInLayer.Add(child.gameObject);
            }
        }

        if (objectsInLayer.Count > 0)
        {
            hasInteractableObjects = true;

        }
        else
        {
            hasInteractableObjects = false;
        }

        return hasInteractableObjects;
    }

    private bool CheckPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && roomCollider.bounds.Contains(player.transform.position))
        {
            Transform floorTransform = transform.Find("Floor");
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
            return true;
        }

        return false;
    }

}
