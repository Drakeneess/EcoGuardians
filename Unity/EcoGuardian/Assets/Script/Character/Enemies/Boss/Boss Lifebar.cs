using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossLifebar : MonoBehaviour
{
    private Boss _boss; // Referencia al jefe
    public Boss boss
    {
        get { return _boss; }
        set
        {
            _boss = value;
            ActivateLifebar(); // Activar la barra cuando se asigna el jefe
        }
    }

    private Image lifebar; // Referencia al componente de imagen de la barra de vida

    private float maxLife;
    private float currentLife;
    private bool isAnimating = false; // Para evitar que múltiples animaciones se superpongan

    private void Awake()
    {
        lifebar = GetComponent<Image>();
        lifebar.fillAmount = 0; // Asegurarse de que la barra esté vacía al inicio
    }

    void Update()
    {
        if (_boss != null && !isAnimating)
        {
            UpdateLifebar();
        }
    }

    public void ActivateLifebar()
    {
        if (_boss != null)
        {
            maxLife = _boss.maxHealt; // Inicializar la vida máxima
            currentLife = _boss.currentHealth;
            StartCoroutine(AnimateLifebarFill()); // Iniciar la animación de llenado
        }
    }

    public void UpdateLifebar()
    {
        if (_boss != null)
        {
            currentLife = _boss.currentHealth;
            lifebar.fillAmount = currentLife / maxLife; // Actualizar el tamaño de la barra en tiempo real
        }
    }

    private IEnumerator AnimateLifebarFill()
    {
        isAnimating = true;

        float targetFill = currentLife / maxLife;
        float fillSpeed = 0.5f; // Velocidad de animación (más alto = más rápido)

        while (lifebar.fillAmount < targetFill)
        {
            lifebar.fillAmount += fillSpeed * Time.deltaTime;
            yield return null; // Espera al siguiente frame
        }

        lifebar.fillAmount = targetFill; // Asegurarse de que termine en el valor exacto
        isAnimating = false;
    }
}
