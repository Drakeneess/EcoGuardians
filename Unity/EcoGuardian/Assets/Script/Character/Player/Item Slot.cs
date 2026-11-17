using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    private GameObject itemObjectPrefab;
    private GameObject itemObject;
    private Item item;
    private Dictionary<Item, float> regenTimeItem = new Dictionary<Item, float>();
    private Dictionary<Item, bool> canRegenItem = new Dictionary<Item, bool>();

    public void SetItem(GameObject newItemObject)
    {
        if (itemObject != null)
        {
            // Si ya hay un item, destruirlo
            Destroy(itemObject);
            itemObject = null;
        }

        // Solo agregamos el nuevo ítem si es válido
        if (newItemObject != null)
        {
            RegenItem(newItemObject); // Generar el nuevo ítem
            
        }
    }

    private void Update()
    {
    }

    private void RegenItems()
    {
        // Iterar sobre los items y sus respectivos tiempos de regeneración
        foreach (KeyValuePair<Item, float> entry in regenTimeItem)
        {
            if (entry.Key != null && canRegenItem[entry.Key])
            {
                // Solo aumentar el tiempo si la regeneración está permitida
                regenTimeItem[entry.Key] += Time.deltaTime;

                if (regenTimeItem[entry.Key] >= entry.Key.spawnRate)
                {
                    // Si el tiempo de regeneración se ha alcanzado, regenerar el item
                    RegenItem(itemObjectPrefab);
                    regenTimeItem[entry.Key] = 0f; // Resetear el contador de tiempo
                    canRegenItem[entry.Key] = false; // Desactivar la regeneración hasta que se agote el tiempo
                }
            }
        }
    }

    private void ResetRegen()
    {
        if (item != null && item.IsGrabbed)
        {
            // Si el item ha sido recogido, reiniciar el tiempo de regeneración
            regenTimeItem[item] = 0f; // Resetear el tiempo de regeneración
            canRegenItem[item] = true; // Volver a permitir la regeneración
        }
    }

    private void RegenItem(GameObject newItemObject)
    {
        // Instanciar el nuevo item
        itemObject = Instantiate(newItemObject);
        itemObject.transform.SetParent(transform);
        itemObject.transform.localPosition = Vector3.zero;
        itemObject.transform.localRotation = Quaternion.identity;
        item = itemObject.GetComponent<Item>();
    }
}
