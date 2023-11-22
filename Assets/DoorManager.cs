using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject linkedDoor;
    public bool isLinked = false;
    public bool isOpen = false;
    public Material openMat;
    public Material closedMat;

    private RoomManager roomManager;
    private Renderer objectRenderer;
    private Collider coll;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider>();
        objectRenderer = GetComponent<Renderer>();
        roomManager = GetComponentInParent<RoomManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (roomManager.isFinished)
        {
            isOpen = true;
        }
        if (isOpen)
        {
            objectRenderer.material = openMat;
            coll.isTrigger = true;
        } else
        {
            coll.isTrigger = true;
            objectRenderer.material = closedMat;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }
}
