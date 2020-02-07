using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class YellowCar : Car
{  
    // Create the tyoe and name
    public YellowCar()
    {
       carData = new CarData();
       carData.type = CarType.YelloWCar;
       carData.name = "Yellow Car";
    }

    // Create particle system on enter
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        
        if(other.tag == "YellowParking")
        {
            GameObject confettiGO = (GameObject)(Instantiate(Resources.Load("PSConfetti")));
            ParticleSystem confettiPS = confettiGO.GetComponent<ParticleSystem>();
            confettiPS.transform.position = other.transform.position;  
            float PSDuration = confettiPS.duration + confettiPS.startLifetime;  
            Destroy(confettiGO, PSDuration);         
        }
    }
}