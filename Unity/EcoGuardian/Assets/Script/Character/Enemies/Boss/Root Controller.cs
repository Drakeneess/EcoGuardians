using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{
    public GameObject rootPrefab; // Prefab de la raíz
    public int minRoots = 3; // Cantidad mínima de raíces
    public int maxRoots = 10; // Cantidad máxima de raíces
    public float minSize = 0.5f; // Tamaño mínimo de las raíces
    public float maxSize = 2f; // Tamaño máximo de las raíces
    public Vector2 spawnAreaSize = new Vector2(10, 10); // Tamaño del área de generación alrededor del Transform actual

    // Start is called before the first frame update
    void Start()
    {
    }

    // Método para generar raíces aleatorias
    public void SpawnRandomRoots()
    {
        // Generar una cantidad aleatoria de raíces
        int rootCount = Random.Range(minRoots, maxRoots + 1);

        for (int i = 0; i < rootCount; i++)
        {
            // Generar una posición aleatoria dentro del área de generación alrededor del Transform actual
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                -13f, // Posición inicial de las raíces debajo del suelo
                Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
            );

            // Ajustar la posición aleatoria relativa al transform del objeto que contiene este script
            randomPosition += transform.position;

            // Instanciar la raíz
            GameObject newRoot = Instantiate(rootPrefab, randomPosition, Quaternion.Euler(-90, 0, 0));

            // Asignar un tamaño aleatorio a la raíz
            float randomSize = Random.Range(minSize, maxSize);
            newRoot.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
    }
}
