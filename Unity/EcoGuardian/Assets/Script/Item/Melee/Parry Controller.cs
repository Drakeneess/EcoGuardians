using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryController : MonoBehaviour
{
    public enum ParryType { Parry, Reflex };
    public ParryType parryType = ParryType.Parry;
    private AudioClip effectSound;
    private AudioSource audioSource;
    private void Start() {
        audioSource=GetComponent<AudioSource>();
        if(parryType==ParryType.Parry){
            effectSound=SoundController.GetSound(5);
        }
        else if(parryType==ParryType.Reflex){
            effectSound=SoundController.GetSound(6);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Projectile"))
        {
            if(other.TryGetComponent(out Projectile projectile)){
                PlayEffectSound();
                if(parryType==ParryType.Parry){
                    Parry(projectile);
                }
                else if(parryType==ParryType.Reflex){
                    Reflex(projectile);
                }
            }

        }
    }
    private void PlayEffectSound(){
        if(audioSource!=null && effectSound!=null){
            audioSource.PlayOneShot(effectSound);
        }
    }
    private void Parry(Projectile projectile) {
        // Redirigir el proyectil hacia su origen
        Vector3 directionToOrigin = (projectile.origin - projectile.transform.position).normalized;
        projectile.SetDirection(directionToOrigin);
    }

    private void Reflex(Projectile projectile) {
        // Redirigir el proyectil en una dirección aleatoria
        Vector3 randomDirection = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;
        projectile.SetDirection(randomDirection);
    }

}
