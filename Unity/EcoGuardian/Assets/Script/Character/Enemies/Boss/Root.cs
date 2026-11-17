using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    public GameObject indicatorPrefab; // Prefab del indicador
    public float warningTime = 2f; // Tiempo que el indicador permanece antes de que la raíz aparezca
    public float growthSpeed = 20f; // Velocidad de crecimiento de la raíz
    public float timeOnGround = 3f; // Tiempo que la raíz permanece en el suelo antes de regresar
    private GameObject indicator; // Referencia al indicador instanciado
    private bool isGrowing = false; // Controla si la raíz está creciendo
    private bool isReturning = false; // Controla si la raíz está regresando a su posición original

    // Start is called before the first frame update
    void Start()
    {
        // Colocar el indicador en la posición de la raíz
        Vector3 indicatorPosition = transform.position;
        indicatorPosition.y = 0f; // Asegurar que el indicador esté al nivel del suelo
        indicator = Instantiate(indicatorPrefab, indicatorPosition, Quaternion.Euler(90, 0, 0));

        // Ajustar la escala del indicador
        Vector3 scale = transform.localScale;
        scale.z = 1; // Fijar el tamaño en Y a 1
        indicator.transform.localScale = scale;

        // Iniciar el proceso de advertencia y crecimiento
        StartCoroutine(ShowIndicatorAndGrow());
    }

    // Corrutina para mostrar el indicador y hacer crecer la raíz
    private IEnumerator ShowIndicatorAndGrow()
    {
        // Esperar el tiempo de advertencia
        yield return new WaitForSeconds(warningTime);

        // Destruir el indicador
        if (indicator != null)
        {
            Destroy(indicator);
        }

        // Iniciar el crecimiento de la raíz
        isGrowing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrowing)
        {
            // Mover la raíz hacia el punto z = 0
            Vector3 targetPosition = transform.position;
            targetPosition.y = 0;

            // Mover la raíz de forma violenta (rápido hacia el objetivo)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, growthSpeed * Time.deltaTime);

            // Detener el crecimiento si ya alcanzó el objetivo
            if (transform.position.y == 0)
            {
                isGrowing = false;

                // Iniciar el tiempo en el suelo
                StartCoroutine(TimeOnGround());
            }
        }
        else if (isReturning)
        {
            // Regresar la raíz al punto original (en este caso, z = 0)
            Vector3 originalPosition = transform.position;
            originalPosition.y = -13; // Ajustar la posición inicial antes de la raíz
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, growthSpeed * Time.deltaTime);

            // Cuando regresa completamente, destruirla
            if (transform.position.y == -13)
            {
                Destroy(gameObject); // Destruir el objeto raíz
            }
        }
    }

    // Corutina para el tiempo que la raíz permanece en el suelo
    private IEnumerator TimeOnGround()
    {
        // Esperar el tiempo que la raíz debe estar en el suelo
        yield return new WaitForSeconds(timeOnGround);

        // Iniciar el regreso al punto inicial
        isReturning = true;
    }
}
