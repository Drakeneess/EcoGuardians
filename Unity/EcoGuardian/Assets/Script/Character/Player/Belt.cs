using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    public GameObject[] itemPrefabs; // Prefabs de los diferentes ítems
    public GameObject[] itemSlotsObject; // Espacios donde los ítems se generarán

    private Coroutine[] spawnCoroutines; // Corutinas para manejar la generación por cada espacio
    private GameObject[] activeItems; // Ítems activos en cada slot
    private ItemSlot[] itemSlots; // Referencia a los item slots

    // Start is called before the first frame update
    void Start()
    {
        itemSlots = new ItemSlot[itemSlotsObject.Length];
        activeItems = new GameObject[itemSlotsObject.Length];
        spawnCoroutines = new Coroutine[itemSlotsObject.Length];

        // Asignar referencias a los ItemSlot desde los objetos de slot
        for (int i = 0; i < itemSlotsObject.Length; i++)
        {
            itemSlots[i] = itemSlotsObject[i].GetComponent<ItemSlot>();
        }

        InitializeItemSpawning(0);
    }

    // Método para inicializar la generación de ítems
    public void InitializeItemSpawning(int index)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i] != null)
            {
                // Iniciar la generación para cada slot de ítem (si no hay un ítem activo)
                if (activeItems[i] == null)
                {
                    itemSlots[i].SetItem(itemPrefabs[index]);
                }
            }
        }
    }
}
