using System.Collections;
using UnityEngine;

public class DeployWeapon : MonoBehaviour
{
    public Transform deployable; // Arma desplegable
    public float transitionDuration = 0.5f; // Duración de la transición

    private Coroutine transitionCoroutine;

    protected virtual void Start()
    {
        SetWeaponState(false);
    }

    // Controla el estado del arma (activar o desactivar)
    protected void SetWeaponState(bool isActive)
    {
        if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);
        
        Vector3 targetScale = isActive ? Vector3.one : Vector3.zero;
        deployable.gameObject.SetActive(true); // Asegura que el objeto esté activo antes de escalar
        
        transitionCoroutine = StartCoroutine(SmoothScale(targetScale, isActive));
    }

    private IEnumerator SmoothScale(Vector3 targetScale, bool finalState)
    {
        Vector3 initialScale = deployable.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            deployable.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        deployable.localScale = targetScale;

        // Desactivar el objeto si la escala objetivo es cero
        if (!finalState)
        {
            deployable.gameObject.SetActive(false);
        }
    }

    protected virtual void TryActivateWeapon()
    {
        // Este método será implementado en las clases derivadas para verificar condiciones específicas
    }
}
