using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Sword : DeployWeapon
{
    public Transform weaponSocket;

    private XRGrabInteractable xRGrab;
    private bool isActive = false; // Estado del arma
    private bool isHeld = false;
    private Rigidbody rb;

    protected override void Start()
    {
        base.Start();
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
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Al soltar el objeto, desactivamos el arma
        isHeld = false;
        rb.isKinematic=false;
    }

    private void Update() 
    {
        // Verificar si el botón derecho está presionado y si la espada está sostenida
        bool shouldActivate = InputController.RightAction() > 0 && isHeld;

        // Solo cambia el estado si hay un cambio en el valor
        if (shouldActivate && !isActive)
        {
            SetWeaponState(true);
            isActive = true; // Actualiza el estado interno
        }
        else if (!shouldActivate && isActive)
        {
            SetWeaponState(false);
            isActive = false; // Actualiza el estado interno
        }
    }
    private void OnCollisionEnter(Collision other) {
        if(isHeld || transform.parent!=null) return;
        if (other.gameObject.CompareTag("Ground")){
            // Ajustar la posición y rotación de la espada al socket
            transform.SetParent(weaponSocket);
            transform.position = weaponSocket.position;
            transform.rotation = weaponSocket.rotation;
            rb.isKinematic=true;
        }
    }
}
