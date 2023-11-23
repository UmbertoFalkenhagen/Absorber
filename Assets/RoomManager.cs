using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameManager gm;
    public GameObject player;
    public bool isFinished = false;
    public bool hasPlayerEntered = false;
    public string layerName; // The layer name we are interested in
    public int layer;
    public List<GameObject> objectsInLayer;

    private List<DoorManager> doors;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        layer = LayerMask.NameToLayer(layerName);
        doors = new List<DoorManager>(GetComponentsInChildren<DoorManager>());
    }

    // Update is called once per frame
    void Update()
    {
        objectsInLayer = new List<GameObject>();

        foreach (Transform child in transform)
        {
            if (child.gameObject.layer == layer)
            {
                objectsInLayer.Add(child.gameObject);
            }
        }

        if (objectsInLayer.Count == 0)
        {
            isFinished = true;
            foreach (var door in doors)
            {
                Debug.Log("Door opened");
                door.OpenDoor();
            }
        }
    }

    public void CloseDoors()
    {
        foreach (var door in doors)
        {
            door.CloseDoor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
        }
        
    }

}
