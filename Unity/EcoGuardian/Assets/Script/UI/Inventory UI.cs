using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Belt belt;
    public Button[] itemButtons;

    // Start is called before the first frame update
    void Start()
    {
        // Inicializar los botones de ítems y agregar listeners
        for (int i = 0; i < itemButtons.Length; i++)
        {
            int index = i; // Capturar la variable para el delegado
            itemButtons[i].onClick.AddListener(() => OnItemButtonClicked(index));
        }
    }

    // Método llamado cuando se hace clic en un botón de ítem
    void OnItemButtonClicked(int index)
    {
        belt.InitializeItemSpawning(index);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
