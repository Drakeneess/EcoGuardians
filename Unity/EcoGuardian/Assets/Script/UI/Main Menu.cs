using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button[] buttons; // Botones principales del menú
    public Button[] storyButtons; // Botones que cuentan la historia
    public GameObject locomotion; // Objeto que se activa al iniciar el juego

    private int currentStoryIndex = 0; // Índice para rastrear el botón actual de la historia

    // Start is called before the first frame update
    void Start()
    {
        // Asignar funciones a los botones principales
        buttons[0].onClick.AddListener(StartGame);
        buttons[1].onClick.AddListener(EndGame);

        // Asignar funciones a los botones de la historia
        foreach (Button storyButton in storyButtons)
        {
            storyButton.onClick.AddListener(ShowNextStoryButton);
        }

        // Ocultar botones principales y mostrar solo el primer botón de la historia
        ShowStoryButtons();
        locomotion.SetActive(false);
    }

    private void ShowStoryButtons()
    {
        // Desactivar todos los botones principales
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(false);
        }

        // Activar solo el primer botón de la historia
        for (int i = 0; i < storyButtons.Length; i++)
        {
            storyButtons[i].gameObject.SetActive(i == 0);
        }

        currentStoryIndex = 0; // Reiniciar el índice de la historia
    }

    private void ShowNextStoryButton()
    {
        // Ocultar el botón actual de la historia
        storyButtons[currentStoryIndex].gameObject.SetActive(false);

        // Avanzar al siguiente botón, si hay uno
        currentStoryIndex++;
        if (currentStoryIndex < storyButtons.Length)
        {
            storyButtons[currentStoryIndex].gameObject.SetActive(true);
        }
        else
        {
            // Cuando se termina la historia, mostrar los botones principales
            ShowMainMenuButtons();
        }
    }

    private void ShowMainMenuButtons()
    {
        // Activar todos los botones principales
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(true);
        }
    }

    private void StartGame()
    {
        locomotion.SetActive(true);
        gameObject.SetActive(false);
    }

    private void EndGame()
    {
        Application.Quit();
    }
}
