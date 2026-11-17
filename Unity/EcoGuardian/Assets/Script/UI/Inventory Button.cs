using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    private bool isActive;
    
    public bool GetIsActive(){
        return isActive;
    }
    public void SetIsActive(bool value){
        isActive = value;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Hand")) {
            isActive=!isActive;
        }
    }
}
