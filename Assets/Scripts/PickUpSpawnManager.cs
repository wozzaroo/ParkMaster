using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawnManager : MonoBehaviour
{
    public Transform[] diamondPickUpPoints;
    public GameObject diamondPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0 ; i < diamondPickUpPoints.Length; i++)
        {
            Instantiate(diamondPrefab, diamondPickUpPoints[i].transform.position, Quaternion.identity);
        }

    }
}
