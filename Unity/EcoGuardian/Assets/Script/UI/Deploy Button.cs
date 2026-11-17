using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeployButton : MonoBehaviour
{
    public Transform bandage; // Referencia al objeto que rotará
    private Button button;
    private bool isLoading = false; // Para evitar múltiples clics

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked); // Asignar el evento al botón
    }

    void Update()
    {
        if (isLoading)
        {
            RotateBandage(); // Rotar el objeto mientras carga
        }
    }

    private void OnButtonClicked()
    {
        Debug.Log("Iniciando carga de escena...");
        if (!isLoading)
        {
            StartCoroutine(LoadSceneAsync(1)); // Cambiar a la escena 1
        }
    }

    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        isLoading = true;

        // Inicia la carga asíncrona de la escena
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        asyncLoad.allowSceneActivation = false; // Evita que la escena se active automáticamente

        while (!asyncLoad.isDone)
        {
            // `asyncLoad.progress` refleja entre 0 y 0.9 durante la carga
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // Normaliza el progreso
            Debug.Log($"Progreso de carga: {progress * 100}%");

            // Verificar si la carga llegó al 90%
            if (asyncLoad.progress >= 0.9f)
            {
                Debug.Log("Carga completada al 90%, esperando 5 segundos antes de activar la escena...");
                asyncLoad.allowSceneActivation = true; // Activar la escena
            }

            yield return null;
        }
    }

    private void RotateBandage()
    {
        float rotationSpeed = 3f; // Velocidad de rotación en grados por segundo
        bandage.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}
