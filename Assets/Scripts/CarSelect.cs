using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class CarSelect : MonoBehaviour
{
    // Screen touch vectors for creating path
    List<Vector3> screenPoints;

    // List of car transforms
    List<Transform> carsTransforms;

    // Car prefabs / paths to move on / spawn position / materials
    // parking points
    public Transform[] carPrefabs;
    public PathCreator[] carPaths;
    public Transform[] carSpawnPoints;
    public Material[] carMaterials;
    public GameObject[] parkingPoints;

    // Nummber of cars to spawn
    public int numOfCars;

    // Current car / inherited from parent Car class
    Car currentCar;

    // First point of path 
    Vector3 startPos;

    // Create new path point every 0.3 seconds
    float targetTime = 0.3f;

    // Path creator is looping or stop / car speed / travel distance
    // Pathcreator script and bezier curve
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    float distanceTravelled;
    PathCreator pathCreator;
    BezierPath bezierPath;

    // bools have points been added and has a car been selected
    bool isSelected;
    bool pointsAdded;

    void Start()
    {
        // Initialise lists / spawn cars / parking spots/ set materials
        carsTransforms = new List<Transform>();
	    screenPoints = new List<Vector3>();
        
        for(int i = 0; i < numOfCars; i++)
        {
            var car = Instantiate(carPrefabs[i], carSpawnPoints[i].position, Quaternion.identity);

            carsTransforms.Add(car);
            MeshRenderer carRenderer = carsTransforms[i].GetComponent<MeshRenderer>();
            carRenderer.material = carMaterials[i];
            parkingPoints[i].SetActive(true);
        }
    }

	void Update () {

        // Layer mask to select the car and then to add points to the floor
        int layerMask = 1 << 8;
        
        layerMask = ~layerMask;

        if (Input.touchCount > 0 &&  Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {
            var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if(Physics.Raycast(Ray, out hit ))
            {
                // Which car was touched / add first point to the cars path / wwe have selected a car
                if(hit.collider.gameObject.tag == "RedCar" || hit.collider.gameObject.tag == "GreenCar" || 
                   hit.collider.gameObject.tag == "BlueCar" || hit.collider.gameObject.tag == "YellowCar")
                { 
                    currentCar = hit.collider.GetComponent<Car>();
                    startPos = carsTransforms[(int)currentCar.carData.type].transform.position;
                    screenPoints.Add(startPos);
                   
                    isSelected = true;
                }
            }
        }

        // Car is selecred / start the timer for spawning new path point / add the position to the list
        // create the path curve / set rotation of normals to up / reset timer
        if(isSelected)
        {
            if (Input.touchCount > 0 &&  Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0))
            {
        
                var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                
                // Use the layermask
                if(Physics.Raycast(Ray, out hit, Mathf.Infinity, layerMask ))
                {
                    targetTime -= Time.deltaTime;
            
                    if (targetTime <= 0.0f)
                    {
                        screenPoints.Add(hit.point);

                        bezierPath = new BezierPath (screenPoints, false, PathSpace.xyz);
                        carPaths[(int)currentCar.carData.type].GetComponent<PathCreator> ().bezierPath = bezierPath;
                        bezierPath.GlobalNormalsAngle = 90;
                        targetTime = 0.3f ;           
                    }                      
                }
            }

            // Touch ended / we can create the path
            if (Input.touchCount > 0 &&  Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
            {   
                pointsAdded= true;
            }

            // Create the path / move and rotate car on path
            if(pointsAdded)
            {
                distanceTravelled += speed * Time.deltaTime;

                carsTransforms[(int)currentCar.carData.type].transform.position = carPaths[(int)currentCar.carData.type].path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                carsTransforms[(int)currentCar.carData.type].transform.rotation = carPaths[(int)currentCar.carData.type].path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                
                // if car has reached end / clear the lists / reset variables
                if(carsTransforms[(int)currentCar.carData.type].transform.position == screenPoints[screenPoints.Count -1])
                {
                    isSelected = false;
                    screenPoints.Clear();
                    distanceTravelled = 0.0f;
                    pointsAdded= false;
                }
            }
        }
    }
}
