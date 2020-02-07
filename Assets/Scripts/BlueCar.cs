using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlueCar : Car
{  
    // Create the tyoe and name
    public BlueCar()
    {
       carData = new CarData();
       carData.type = CarType.BlueCar;
       carData.name = "Blue Car";
    }

    // Create particle system on enter
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        
        if(other.tag == "BlueParking")
        {
            GameObject confettiGO = (GameObject)(Instantiate(Resources.Load("PSConfetti")));
            ParticleSystem confettiPS = confettiGO.GetComponent<ParticleSystem>();
            confettiPS.transform.position = other.transform.position;  
            float PSDuration = confettiPS.duration + confettiPS.startLifetime;  
            Destroy(confettiGO, PSDuration);   
        }
    }
}