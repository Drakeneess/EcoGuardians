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

    public Transform slideTransform;    // Transform de la corredera
    public float slideLimitMin = -0.1f; // Límite mínimo para el movimiento de la corredera
    public float slideLimitMax = 0.1f;  // Límite máximo para el movimiento de la corredera

    private Battery battery;               // Referencia a la batería (suponiendo que la tienes)
    private Rigidbody rb;
    private float nextFireTime = 0f;     // Tiempo para el próximo disparo
    private XRGrabInteractable xRGrab;
    private bool isHeld = false;

    // Start is called before the first frame update
    void Start()
    {
        // Inicialización si es necesario
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
        rb.isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (battery != null)
        {
            // Verificar si se debe disparar y si el tiempo actual es mayor o igual al tiempo para el próximo disparo
            if (InputController.RightAction() > 0 && Time.time >= nextFireTime && battery.GetCurrentEnergy() > 0 && isHeld)
            {
                Shoot();
                nextFireTime = Time.time + fireRate; // Actualiza el tiempo para el próximo disparo
            }
        }

        // Llamar a la función para mover la corredera
        MoveSlide();
    }

    private void Shoot()
    {
        // Instanciar el proyectil en el punto de disparo
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        // Aquí puedes agregar lógica adicional como aplicar fuerza al proyectil si es necesario
        battery.DrainEnergy();
    }

    // Función para mover la corredera con limitación en el eje Z
    private void MoveSlide()
    {
        if (slideTransform != null)
        {
            // Obtener la posición local de la corredera
            float localZ = slideTransform.localPosition.z;

            // Limitar el movimiento de la corredera en el eje Z
            if (localZ < slideLimitMin)
            {
                localZ = slideLimitMin;  // No permitir que se mueva por debajo del límite mínimo
            }
            else if (localZ > slideLimitMax)
            {
                localZ = slideLimitMax;  // No permitir que se mueva por encima del límite máximo
            }

            // Aplicar la posición limitada
            slideTransform.localPosition = new Vector3(slideTransform.localPosition.x, slideTransform.localPosition.y, localZ);
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
        if(isHeld || transform.parent!=null) return;
        if (other.gameObject.CompareTag("Ground")){
            // Ajustar la posición y rotación de la espada al socket
            transform.SetParent(weaponSocket);
            transform.position = weaponSocket.position;
            transform.rotation = weaponSocket.rotation;
            rb.isKinematic = true;
        }
    }
}
