using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    private AudioClip shieldImpact;
    private AudioSource audioSource;
    private void Start() {
        
        audioSource=GetComponent<AudioSource>();
        shieldImpact = SoundController.GetSound(4);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Projectile")){
            if(audioSource!=null && shieldImpact!=null){
                audioSource.PlayOneShot(shieldImpact);
            }
            Destroy(other.gameObject);
        }
    }
}
