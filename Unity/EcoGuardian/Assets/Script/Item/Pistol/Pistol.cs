using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Pistol : MonoBehaviour
{
    public Transform shootPoint;         // Punto desde donde se dispara
    public GameObject projectilePrefab;   // Prefab del proyectil
    public float fireRate = 0.5f;        // Tiempo de espera entre disparos
    public Transform weaponSocket;
    public LayerMask groundLayer;
    public GameObject pointer;
    public ParticleSystem muzzleFlash;

    private Battery battery;               // Referencia a la batería (suponiendo que la tienes)
    private Rigidbody rb;
    private float nextFireTime = 0f;     // Tiempo para el próximo disparo
    private XRGrabInteractable xRGrab;
    private bool isHeld = false;
    private AudioClip shootSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        shootSound=SoundController.GetSound(2);

        // Inicialización si es necesario
        xRGrab = GetComponent<XRGrabInteractable>();
        pointer.SetActive(false);
        rb = GetComponent<Rigidbody>();
        // Suscribir los eventos de agarre y soltar
        if (xRGrab != null)
        {
            xRGrab.selectEntered.AddListener(OnGrab);
            xRGrab.selectExited.AddListener(OnRelease);
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Al agarrar el objeto, activamos el arma
        isHeld = true;
        pointer.SetActive(true);
        transform.SetParent(null);
        if(args.interactorObject.transform.CompareTag("RightHand")){
            HandDomainController.SetHandDomain(HandDomainController.HandDomain.Right);
        }
        else if(args.interactorObject.transform.CompareTag("LeftHand")){
            HandDomainController.SetHandDomain(HandDomainController.HandDomain.Left);
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Al soltar el objeto, desactivamos el arma
        isHeld = false;
        rb.isKinematic = false;
        pointer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (battery != null)
        {
            // Verificar si se debe disparar y si el tiempo actual es mayor o igual al tiempo para el próximo disparo
            if (InputController.AttackButton() > 0 && Time.time >= nextFireTime && battery.GetCurrentEnergy() > 0 && isHeld)
            {
                Shoot();
                nextFireTime = Time.time + fireRate; // Actualiza el tiempo para el próximo disparo
            }
        }
        if(transform.position.x<-200|| transform.position.y<-200|| transform.position.z<-200) BackHolder();
    }

    private void Shoot()
    {
        // Instanciar el proyectil en el punto de disparo
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        // Aquí puedes agregar lógica adicional como aplicar fuerza al proyectil si es necesario
        battery.DrainEnergy();
        muzzleFlash.Play();
        PlayShootSound();
    }
    private void PlayShootSound()
    {
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    public void SetBattery(Battery newbattery)
    {
        battery = newbattery;
    }

    public void ReleaseBattery()
    {
        battery = null;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") && !isHeld){
            BackHolder();
        }
    }
    public bool HasBattery(){
        return battery != null;
    }
    private void BackHolder(){
        // Ajustar la posición y rotación de la espada al socket
        transform.SetParent(weaponSocket);
        transform.position = weaponSocket.position;
        transform.rotation = weaponSocket.rotation;
        rb.isKinematic = true;
    }
}
