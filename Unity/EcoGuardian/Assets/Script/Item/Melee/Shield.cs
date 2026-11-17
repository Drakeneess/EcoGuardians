using System.Collections;
using UnityEngine;

public class Shield : DeployWeapon
{
    [Header("Energy Control")]
    public float maxEnergy = 100f; // Energía máxima del escudo
    public float energyDrainRate = 1f; // Tasa de consumo de energía
    public float energyRechargeRate = 1f; // Tasa de recarga de energía
    public GameObject energyBar; // Barra de energía (3D)
    public float rechargeThreshold = 0.2f; // Porcentaje mínimo de recarga para reactivarse (20%)

    private float currentEnergy; // Energía actual
    private bool isActive = false; // Estado del escudo
    private bool wasButtonPressed = false; // Para verificar el estado previo del botón
    private AudioClip shieldSound; // Sonido del escudo
    private Vector3 originalScale; // Escala original de la barra de energía
    private bool isEnergyDepleted = false; // Verifica si la energía llegó a 0

    protected override void Start()
    {
        base.Start();
        currentEnergy = maxEnergy; // Inicializa la energía al máximo

        if (energyBar != null)
        {
            originalScale = energyBar.transform.localScale; // Guarda la escala original
        }

        shieldSound = SoundController.GetSound(3); // Asigna el sonido del escudo
    }

    private void PlayShieldSound()
    {
        if (audioSource != null && shieldSound != null)
        {
            audioSource.PlayOneShot(shieldSound);
        }
    }

    void Update()
    {
        // Verificar si el botón izquierdo está presionado
        bool shouldActivate = InputController.ShieldAction() > 0.1; // Ajusta el umbral según sea necesario

        // Solo cambia el estado si hay un cambio en el valor
        if (shouldActivate && !wasButtonPressed && !isActive && CanActivateShield()) // Activa el escudo
        {
            ActivationState(true);
        }
        else if ((!shouldActivate && wasButtonPressed) || currentEnergy <= 0) // Desactiva el escudo
        {
            ActivationState(false);
        }

        // Administrar energía según el estado del escudo
        ManageEnergy();
    }

    private void OnEnable()
    {
        SetWeaponState(false);
    }

    private void ActivationState(bool state)
    {
        SetWeaponState(state);
        isActive = state; // Actualiza el estado interno
        wasButtonPressed = state; // Actualiza el estado del botón
        PlayShieldSound();
    }

    private void ManageEnergy()
    {
        if (isActive)
        {
            // Reducir energía mientras el escudo esté activo
            currentEnergy -= energyDrainRate * Time.deltaTime;
            currentEnergy = Mathf.Max(currentEnergy, 0); // Evitar valores negativos

            // Marca el escudo como agotado si la energía llega a 0
            if (currentEnergy == 0)
            {
                isEnergyDepleted = true;
            }
        }
        else
        {
            // Recargar energía si el escudo está inactivo
            currentEnergy += energyRechargeRate * Time.deltaTime;
            currentEnergy = Mathf.Min(currentEnergy, maxEnergy); // Evitar superar el máximo

            // Si la energía supera el umbral, permite reactivar el escudo
            if (currentEnergy >= maxEnergy * rechargeThreshold)
            {
                isEnergyDepleted = false; // Se ha recargado lo suficiente
            }
        }

        // Actualizar barra de energía
        UpdateEnergyBar();
    }

    private void UpdateEnergyBar()
    {
        if (energyBar != null)
        {
            // Escala proporcional al nivel de energía
            float scaleFactor = currentEnergy / maxEnergy;
            Vector3 newScale = originalScale;
            newScale.x *= scaleFactor; // Ajusta la escala en el eje X (o el eje correspondiente)
            energyBar.transform.localScale = newScale;

            // Asegura que la barra se mantenga en la posición correcta
            Vector3 positionOffset = energyBar.transform.localPosition;
            positionOffset.x = (originalScale.x - newScale.x) / 2f; // Ajusta el centro de la barra
            energyBar.transform.localPosition = positionOffset;
        }
    }

    private bool CanActivateShield()
    {
        // Verifica si el escudo puede activarse
        // Si la energía está agotada, solo permite reactivarlo si ha recargado al menos el 20%
        if (isEnergyDepleted)
        {
            return currentEnergy >= maxEnergy * rechargeThreshold;
        }

        // Si la energía no está agotada, puede activarse normalmente
        return true;
    }
}
