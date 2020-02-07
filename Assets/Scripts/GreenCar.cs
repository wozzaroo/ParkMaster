using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GreenCar : Car
{  
    // Create the tyoe and name
    public GreenCar()
    {
       carData = new CarData();
       carData.type = CarType.GreenCar;
       carData.name = "Green Car";
    }

    // Create particle system on enter
     public override void OnTriggerEnter(Collider other)
    {
       base.OnTriggerEnter(other);
        
       if(other.tag == "GreenParking")
       {
            GameObject confettiGO = (GameObject)(Instantiate(Resources.Load("PSConfetti")));
            ParticleSystem confettiPS = confettiGO.GetComponent<ParticleSystem>();
            confettiPS.transform.position = other.transform.position;  
            float PSDuration = confettiPS.duration + confettiPS.startLifetime;  
            Destroy(confettiGO, PSDuration);   
       }
    }
}