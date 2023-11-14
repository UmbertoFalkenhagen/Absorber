using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public bool isFinished = false;
    public bool hasPlayerEntered = false;
    public string layerName; // The layer name we are interested in
    public int layer;
    public List<GameObject> objectsInLayer;

    // Start is called before the first frame update
    void Start()
    {
        layer = LayerMask.NameToLayer(layerName);
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
        }
    }
}
