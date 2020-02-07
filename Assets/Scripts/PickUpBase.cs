using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum PickUpType : int
{
    Coin, // Not used
    Diamond, 
    Gold   // Not used
}

struct PickUpData {
    public PickUpType type;
}

 public class PickUpBase : MonoBehaviour
{
    float rotationSpeed = 100;

    // Pickup and Audio managers
    [HideInInspector]public GameObject PickUpSpawnManager;
    [HideInInspector]public GameObject AudioManager;

    // Pickup and Audio managers scripts
    protected PickUpSpawnManager PickUpSpawnManagerScript;
    protected AudioManager AudioManagerScript;
    public PickUpBase()
    {
    }
  
    // Get the managers
    void Start()
    {     
         PickUpSpawnManager = GameObject.Find("PickUpSpawnManager");
         PickUpSpawnManagerScript = PickUpSpawnManager.GetComponent<PickUpSpawnManager>();
         AudioManager = GameObject.Find("AudioManager");
         AudioManagerScript = AudioManager.GetComponent<AudioManager>();
    }

    // Rotate this pickup
    void Update()
    {
        transform.Rotate (0.0f, Time.deltaTime * rotationSpeed, 0.0f, Space.World);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
    }
}
