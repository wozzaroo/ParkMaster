using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PickUpDiamond : PickUpBase
{
    // Set the struct of this pickup
    PickUpData pickUpData;
    public PickUpDiamond() 
    {
      pickUpData = new PickUpData();
      pickUpData.type = PickUpType.Diamond;
    }

    // On triiger play sound / destroy
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter( other);
        
        if(other.tag == "RedCar" || other.tag == "GreenCar" || other.tag == "BlueCar" || other.tag == "YellowCar")
        {
            AudioManagerScript.PlayPickUpSound(0);
            Destroy(gameObject);
        }
    }
}
