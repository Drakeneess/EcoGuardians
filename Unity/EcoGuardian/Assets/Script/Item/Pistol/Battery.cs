using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Battery : MonoBehaviour
{
    public float energyCapacity = 100f;        // Capacidad máxima de la batería
    public float energyDrainRate = 3f;          // Tasa de consumo de energía por segundo
    public GameObject indicator;                 // Objeto para indicar la energía restante

    private float actualEnergy;                  // Energía actual de la batería
    private Rigidbody rb;
    private Pistol pistol;
    private XRGrabInteractable xRGrab;


    // Start is called before the first frame update
    void Start()
    {
        xRGrab = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
        // Suscribir los eventos de agarre y soltar
        if (xRGrab != null)
        {
            xRGrab.selectEntered.AddListener(OnGrab);
            xRGrab.selectExited.AddListener(OnRelease);
        }
        actualEnergy = energyCapacity;           // Inicializar energía actual en la capacidad máxima
        UpdateIndicator();                       // Actualizar el indicador al inicio
    }
    private void OnGrab(SelectEnterEventArgs args)
    {
        if(pistol!=null){
            transform.SetParent(null);
            pistol.ReleaseBattery();
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if(pistol!=null){
            rb.isKinematic=false;
            transform.SetParent(null);
        }
    }

    public void ReleaseBattery()
    {
        // Aquí puedes agregar cualquier lógica adicional si es necesario
        pistol = null;  // Desvinculamos la batería de la pistola
        rb.isKinematic=false;
        transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateIndicator();                       // Actualizar el indicador de energía
    }

    public void DrainEnergy()
    {
        if (actualEnergy > 0)                    // Solo drenar si hay energía disponible
        {
            actualEnergy -= energyDrainRate; // Drenar energía
            if (actualEnergy < 0)                // No permitir que la energía sea negativa
            {
                actualEnergy = 0;
            }
        }
    }

    private void UpdateIndicator()
    {
        if (indicator != null)
        {
            // Asumiendo que el indicador es un objeto que puede cambiar su escala o color
            // Aquí puedes ajustar cómo quieres representar la energía restante
            float energyPercentage = actualEnergy / energyCapacity; // Calcular porcentaje de energía
            indicator.transform.localScale = new Vector3(1, 1,energyPercentage); // Ajustar escala
        }
    }

    public float GetCurrentEnergy()
    {
        return actualEnergy;                      // Método para obtener la energía actual (opcional)
    }

    public void Recharge(float amount)
    {
        actualEnergy += amount;                   // Método para recargar la batería
        if (actualEnergy > energyCapacity)        // Asegurarse de no sobrepasar la capacidad
        {
            actualEnergy = energyCapacity;
        }
    }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Battery Holder")) {

            pistol=other.gameObject.GetComponentInParent<Pistol>();
            pistol.SetBattery(this);
            transform.SetParent(other.transform);
            transform.localPosition = new Vector3(0,0,0);
            transform.localRotation = Quaternion.Euler(-90,0,0);
            rb.isKinematic=true;
        }
    }
}
