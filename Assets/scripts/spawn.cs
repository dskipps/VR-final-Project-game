using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    // Reference to the VR camera rig
    public GameObject vrCameraRig;

    // Reference to the spawn position (Empty GameObject)
    public GameObject spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        // Set the VR camera rig's position to the spawn point's position
        if (vrCameraRig != null && spawnPoint != null)
        {
            vrCameraRig.transform.position = spawnPoint.transform.position;
            vrCameraRig.transform.rotation = spawnPoint.transform.rotation; // Optional, if you want to match rotation
        }
        else
        {
            Debug.LogError("VR Camera Rig or Spawn Point is not assigned!");
        }
    }
}
