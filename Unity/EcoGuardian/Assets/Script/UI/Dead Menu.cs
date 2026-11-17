using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{
    public static DeadMenu instance { get; private set; } // Instancia estática

    public Button[] buttons; // Botones del menú
    public Player player; // Referencia al jugador

    private void Awake()
    {
        // Configurar la instancia estática y asegurar que no se duplique
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        // Configurar listeners para los botones
        if (buttons.Length >= 3)
        {
            buttons[0].onClick.AddListener(RespawnPlayer);
            buttons[1].onClick.AddListener(Reset);
            buttons[2].onClick.AddListener(BackMenu);
            buttons[3].onClick.AddListener(ExitGame);
        }
        else
        {
            Debug.LogError("No hay suficientes botones asignados en el inspector.");
        }

        // Asegurarse de que el menú está inactivo al inicio
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        // Limpiar la instancia al destruir el objeto
        if (instance == this)
        {
            instance = null;
        }
    }

    // Método para reactivar al jugador
    private void RespawnPlayer()
    {
        if (player != null)
        {
            player.Respawn();
            instance.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("No se asignó el jugador en el inspector.");
        }
    }
    private void Reset(){
        SceneManager.LoadScene(1);
    }

    // Método para regresar al menú principal
    private void BackMenu()
    {
        SceneManager.LoadScene(0); // Cambiar al índice de la escena del menú
    }

    // Método para cerrar el juego
    private void ExitGame()
    {
        Application.Quit();
    }

    // Método estático para mostrar el menú de muerte
    public static void ShowMenu()
    {
        if (instance != null)
        {
            instance.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("DeadMenu no está inicializado.");
        }
    }
}
