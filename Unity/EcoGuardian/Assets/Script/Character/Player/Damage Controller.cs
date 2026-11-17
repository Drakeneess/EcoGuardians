using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DamageController : MonoBehaviour
{
    public float maxValueVolume = 0.7f; // Máxima intensidad del Vignette
    public float minValueVolume = 0f; // Intensidad mínima (estado normal)
    public float volumeChangeSpeed = 0.1f; // Velocidad de cambio al regresar al estado normal
    public Volume volume; // Volume asignado en el Inspector
    private Vignette vignette; // Referencia al efecto Vignette
    private bool isTakingDamage = false; // Indica si el personaje está en estado de daño

    void Start()
    {
        // Asegúrate de que el Volume tenga el efecto Vignette activado
        if (volume.profile.TryGet(out vignette))
        {
            vignette.intensity.value = minValueVolume;
        }
        else
        {
            Debug.LogError("El efecto Vignette no está configurado en el Volume.");
        }
    }

    void Update()
    {
        if (!isTakingDamage && vignette != null && vignette.intensity.value > minValueVolume)
        {
            // Reducir la intensidad del Vignette gradualmente hacia el valor mínimo
            vignette.intensity.value = Mathf.MoveTowards(vignette.intensity.value, minValueVolume, volumeChangeSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage()
    {
        if (vignette != null)
        {
            // Incrementa la intensidad del Vignette al recibir daño
            vignette.intensity.value = maxValueVolume;
            isTakingDamage = true;

            // Simula un breve tiempo en estado de daño
            StartCoroutine(ResetDamageState());
        }
    }

    private IEnumerator ResetDamageState()
    {
        // Espera un breve periodo antes de permitir que la intensidad se reduzca
        yield return new WaitForSeconds(0.01f); // Tiempo de permanencia del Vignette
        isTakingDamage = false;
    }
}
