using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    public GameObject inventoryPanel;  // El panel del inventario que se activará o desactivará
    public InventoryButton inventoryButton;  // El botón de inventario
    private bool isInventoryOpen = false;  // Estado actual del inventario

    private bool buttonPressed = false;  // Para detectar si el botón A fue presionado
    private float cooldownTime = 0.2f;  // Tiempo de espera para evitar cambios rápidos
    private float lastInputTime = 0f;   // Último tiempo en que se detectó una entrada

    // Configuración inicial
    private void Start()
    {
        inventoryPanel.SetActive(false);  // Ocultar el panel al inicio
    }

    // Actualización por frame
    private void Update()
    {
        // Verificar si ha pasado suficiente tiempo desde la última entrada
        if (Time.time - lastInputTime > cooldownTime)
        {
            // Detectar si se ha presionado el botón A
            if (InputController.ButtonB() > 0.5f && !buttonPressed)
            {
                buttonPressed = true;
                ToggleInventory();
                lastInputTime = Time.time;
            }
            else if (InputController.ButtonA() <= 0.5f)
            {
                buttonPressed = false;
            }

            // Detectar si el botón de inventario ha cambiado de estado
            if (inventoryButton.GetIsActive())
            {
                ToggleInventory();
                inventoryButton.SetIsActive(false);  // Resetear el estado del botón después de activarlo
                lastInputTime = Time.time;
            }
        }
    }

    // Método para abrir o cerrar el inventario
    private void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryPanel.SetActive(isInventoryOpen);  // Cambiar el estado del panel
    }
}
