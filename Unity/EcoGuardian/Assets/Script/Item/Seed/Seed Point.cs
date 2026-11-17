using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeedPoint : MonoBehaviour
{
    public GameObject plantPrefab;
    private bool seeded;
    private GameObject plant;
    
    public bool GetSeeded(){
        return seeded;
    }

    private void OnTriggerEnter(Collider other) {
        if(seeded) return;
        if(!seeded && other.TryGetComponent(out Seed seed)){
            if(seed.IsGrabbed){
                seeded = true;
                plant = Instantiate(plantPrefab, transform.position, Quaternion.identity);
                plant.transform.SetParent(transform);
                Destroy(seed.gameObject);
            }
        }
    }
}
