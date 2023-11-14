using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public bool isLinked = false;
    public bool isOpen = false;
    public Material openMat;
    public Material closedMat;

    private RoomManager roomManager;
    private Renderer objectRenderer;

    // Start is called before the first frame update
    void Start()
    {
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
        } else
        {
            objectRenderer.material = closedMat;
        }
    }
}
