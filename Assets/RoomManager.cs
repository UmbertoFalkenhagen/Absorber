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
    private Collider roomCollider;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        layer = LayerMask.NameToLayer(layerName);
        roomCollider = GetComponent<Collider>();
        CheckInitialPlayerPosition();

    }

    private void CheckInitialPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && roomCollider.bounds.Contains(player.transform.position))
        {
            
            CheckForEnemies();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            CheckForEnemies();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetFightingStatus(false);
        }
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

        if (objectsInLayer.Count == 0 && GameManager.Instance.activeRoom == gameObject)
        {
            CheckForEnemies();
        }
    }

    private void CheckForEnemies()
    {
        foreach (GameObject child in objectsInLayer)
        {
            if (child.layer == layer)
            {
                GameManager.Instance.SetFightingStatus(true);
                return;
            }
        }

        GameManager.Instance.SetFightingStatus(false);
    }

}
