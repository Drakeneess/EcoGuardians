using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Sword : DeployWeapon
{
    public Transform weaponSocket;

    private XRGrabInteractable xRGrab;
    private bool isActive = false; // Estado del arma
    private bool isHeld = false;
    private Rigidbody rb;

    // Referencias a los efectos de sonido
    private AudioClip activateSound;  // Sonido al encender
    private AudioClip deactivateSound; // Sonido al apagar

    protected override void Start()
    {
        base.Start();
        
        activateSound=SoundController.GetSound(0);
        deactivateSound = SoundController.GetSound(0);

        xRGrab = GetComponent<XRGrabInteractable>();
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
        transform.SetParent(null);
        if(args.interactorObject.transform.CompareTag("RightHand"))
        {
            HandDomainController.SetHandDomain(HandDomainController.HandDomain.Right);
        }
        else if(args.interactorObject.transform.CompareTag("LeftHand"))
        {
            HandDomainController.SetHandDomain(HandDomainController.HandDomain.Left);
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Al soltar el objeto, desactivamos el arma
        isHeld = false;
        rb.isKinematic = false;
    }

    private void Update() 
    {
        // Verificar si el botón derecho está presionado y si la espada está sostenida
        bool shouldActivate = InputController.AttackButton() > 0 && isHeld;

        // Solo cambia el estado si hay un cambio en el valor
        if (shouldActivate && !isActive)
        {
            SetWeaponState(true);
            isActive = true; // Actualiza el estado interno
            PlayActivateSound(); // Reproducir sonido al encender
        }
        else if (!shouldActivate && isActive)
        {
            SetWeaponState(false);
            isActive = false; // Actualiza el estado interno
            PlayDeactivateSound(); // Reproducir sonido al apagar
        }
        if(transform.position.x<-200|| transform.position.y<-200|| transform.position.z<-200) BackHolder();

    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Ground") && !isHeld)
        {
            BackHolder();
        }
    }

    // Reproducir el sonido de activación
    private void PlayActivateSound()
    {
        if (audioSource != null && activateSound != null)
        {
            audioSource.PlayOneShot(activateSound);
        }
    }

    // Reproducir el sonido de desactivación
    private void PlayDeactivateSound()
    {
        if (audioSource != null && deactivateSound != null)
        {
            audioSource.PlayOneShot(deactivateSound);
        }
    }
    private void BackHolder(){
        // Ajustar la posición y rotación de la espada al socket
        transform.SetParent(weaponSocket);
        transform.position = weaponSocket.position;
        transform.rotation = weaponSocket.rotation;
        rb.isKinematic = true;
    }
}
