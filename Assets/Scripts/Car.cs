using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

// Type of Car
public enum CarType 
{
    
    RedCar,
    
    GreenCar,

    BlueCar,

    YelloWCar,
    None
   
}

// Struct for type and name
public struct CarData {
    public CarType type;

    public string name;
}


public class Car : MonoBehaviour
{
    public CarData carData;

    protected GameObject carSelecter;
    protected CarSelect carSelect;

    
    void Start()
    {
        carSelecter = GameObject.Find("CarSelecter");
        carSelect = carSelecter.GetComponent<CarSelect>();
    }
    // On Trigger enter
    public virtual void OnTriggerEnter(Collider other)
    {
    }
}
