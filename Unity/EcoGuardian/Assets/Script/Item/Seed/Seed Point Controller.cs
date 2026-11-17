using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPointController : MonoBehaviour
{
    public GameObject seedPointPrefab;
    public Transform seedPointParent;
    private static GameObject seedPoint;
    private static Transform seedPointParentObject;
    public static SeedPointController instance{get; private set;}

    private void Start() {
        seedPoint=seedPointPrefab;
        seedPointParentObject = seedPointParent;
    }
    
    public static SeedPoint SpawnSeedPoint(Vector3 position){
        position.y=position.y-0.5f;
        GameObject newPlantPoint=Instantiate(seedPoint, position, Quaternion.Euler(0,0,0));
        newPlantPoint.transform.SetParent(seedPointParentObject);

        return newPlantPoint.GetComponent<SeedPoint>();
    }
}
