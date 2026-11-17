using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLifebarUI : MonoBehaviour
{
    public static EnemyLifebarUI Instance { get; private set; }

    [Header("UI Elements")]
    public Image healthBarImage; // Barra de vida del enemigo
    public TextMeshProUGUI[] nameText; // Nombre del enemigo

    private static Enemy currentEnemy; // Referencia al enemigo activo
    private static float maxHealth;
    private static float currentHealth;

    private void Awake()
    {
        // Verifica si ya existe una instancia
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Elimina cualquier instancia duplicada
        }
        else
        {
            Instance = this; // Asigna esta instancia como la principal
            DontDestroyOnLoad(gameObject); // Evita que se destruya al cargar nuevas escenas
        }
    }

    private void Start()
    {
        // Buscar el hijo con el tag "LifeBar" y asignarlo a healthBarImage
        if (healthBarImage == null)
        {
            // Busca el hijo con el tag "LifeBar"
            Transform lifeBarTransform = transform.Find("LifeBar");

            if (lifeBarTransform != null)
            {
                healthBarImage = lifeBarTransform.GetComponent<Image>();
            }

            if (healthBarImage == null)
            {
                Debug.LogError("No se encontró la imagen de la barra de vida con el tag 'LifeBar'.");
            }
        }

        // Asignar el texto si es necesario
        if (nameText == null)
        {
            nameText = GetComponentsInChildren<TextMeshProUGUI>();
        }

        HideLifebar(); // Ocultar inicialmente la barra de vida
    }

    private void Update()
    {
        // Actualiza la barra de vida continuamente si hay un enemigo activo
        if (currentEnemy != null)
        {
            UpdateHealthBar();
        }
    }

    public static void SetEnemy(Enemy newEnemy)
    {
        // Configura el enemigo actual y actualiza la barra de vida
        if (newEnemy == null)
        {
            Instance.HideLifebar();
            return;
        }

        currentEnemy = newEnemy;
        maxHealth = currentEnemy.maxHealt;
        currentHealth = currentEnemy.currentHealth;

        // Actualizar la interfaz
        Instance.nameText[0].text = currentEnemy.enemyName;
        Instance.nameText[1].text = currentEnemy.enemyName;

        Instance.ShowLifebar();
        Instance.UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (currentEnemy == null) return;

        currentHealth = currentEnemy.currentHealth;

        // Calcula el porcentaje de vida y actualiza la barra
        float healthPercentage = currentHealth / maxHealth;
        healthBarImage.fillAmount = Mathf.Clamp01(healthPercentage);
    }

    private void ShowLifebar()
    {
        // Hacer visible la barra de vida
        gameObject.SetActive(true);
    }

    private void HideLifebar()
    {
        // Ocultar la barra de vida
        gameObject.SetActive(false);
    }

    public static void ClearEnemy()
    {
        // Limpia la referencia al enemigo actual y oculta la barra de vida
        currentEnemy = null;
        Instance.HideLifebar();
    }
}
