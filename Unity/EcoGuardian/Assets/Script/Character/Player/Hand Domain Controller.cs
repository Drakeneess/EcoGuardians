using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDomainController : MonoBehaviour
{
    // Singleton Instance
    public static HandDomainController Instance { get; private set; }

    // Enum para definir la mano
    public enum HandDomain { Left, Right }
    public HandDomain handDomainActual;

    // Transformaciones para los objetos
    public Transform watch;
    public Transform watchContent;
    public Transform shield;
    public Transform[] watchHolders;
    public Transform[] shieldHolders;

    // Estado previo de la mano
    private HandDomain previousHandDomain;

    // Métodos para la inicialización
    private void Awake()
    {
        // Configurar el Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // Configurar el dominio de la mano al iniciar
        previousHandDomain = handDomainActual; // Inicializar con el estado actual
        SetHandDomain(handDomainActual);
    }

    // Método público para establecer el dominio de la mano
    public static void SetHandDomain(HandDomain handDomain)
    {
        if (Instance.handDomainActual != handDomain) // Verificar si hay un cambio de mano
        {
            Instance.handDomainActual = handDomain;
            Instance.SwitchHand();
        }
    }

    // Cambiar la posición de los objetos en función del dominio de la mano
    private void SwitchHand()
    {
        if (handDomainActual == HandDomain.Left)
        {
            SwitchHolder(watch, watchHolders[1]);
            SwitchHolder(shield, shieldHolders[0]);
        }
        else if (handDomainActual == HandDomain.Right)
        {
            SwitchHolder(watch, watchHolders[0]);
            SwitchHolder(shield, shieldHolders[1]);
        }

        // Solo rotar el contenido si hay cambio de mano
        if (previousHandDomain != handDomainActual)
        {
            RotateWatchContent();
        }

        // Actualizar el estado previo
        previousHandDomain = handDomainActual;
    }

    // Método para cambiar el padre y reiniciar posición
    private void SwitchHolder(Transform obj, Transform holder)
    {
        if (obj == null || holder == null) return; // Verificar que las referencias no sean nulas

        // Cambiar el padre
        obj.SetParent(holder);
        obj.localPosition = Vector3.zero;

        // Restaurar la rotación original
        obj.localRotation = Quaternion.identity;
    }

    // Método para rotar el contenido del reloj
    private void RotateWatchContent()
    {
        if (watchContent != null)
        {
            // Rotar el contenido del reloj 180 grados en el eje Z
            watchContent.localRotation = Quaternion.Euler(
                watchContent.localRotation.eulerAngles.x,
                watchContent.localRotation.eulerAngles.y,
                watchContent.localRotation.eulerAngles.z + 180
            );
        }
    }

    public static bool IsRightHandDomain()
    {
        return Instance.handDomainActual == HandDomain.Right;
    }
}
