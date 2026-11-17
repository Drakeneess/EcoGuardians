using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCalculator : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        print(Vector3.Distance(transform.position,target.position)/2);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
