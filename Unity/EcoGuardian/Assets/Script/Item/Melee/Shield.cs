using System.Collections;
using UnityEngine;

public class Shield : DeployWeapon
{
    private bool isActive = false; // Estado del escudo
    private bool wasButtonPressed = false; // Para verificar el estado previo del botón

    protected override void Start()
    {
        base.Start();
        
    }

    void Update()
    {
        // Verificar si el botón izquierdo está presionado
        bool shouldActivate = InputController.LeftAction() > 0.1; // Ajusta el umbral según sea necesario

        // Solo cambia el estado si hay un cambio en el valor
        if (shouldActivate && !wasButtonPressed && !isActive)  // Aseguramos que el cambio de estado solo ocurra cuando se activa el botón
        {
            SetWeaponState(true);
            isActive = true; // Actualiza el estado interno
            wasButtonPressed = true; // Actualiza el estado del botón
        }
        else if (!shouldActivate && wasButtonPressed)  // Solo cambia el estado si el botón se ha soltado
        {
            SetWeaponState(false);
            isActive = false; // Actualiza el estado interno
            wasButtonPressed = false; // Actualiza el estado del botón
        }
    }
}
