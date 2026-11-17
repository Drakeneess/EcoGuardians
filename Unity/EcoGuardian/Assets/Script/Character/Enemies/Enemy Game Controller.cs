using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGameController : MonoBehaviour
{
    public static EnemyGameController Instance{get; private set;}
    // Listas para diferentes tipos de enemigos y puntos
    public static List<Enemy> normalEnemies = new List<Enemy>();
    public static List<BossPoint> outAreaPoints = new List<BossPoint>();
    public static List<BossPoint> inAreaPoints = new List<BossPoint>();
    public static Boss boss;

    private void Awake()
    {
        // Encontrar todos los enemigos y puntos en la escena
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        BossPoint[] allPoints = FindObjectsOfType<BossPoint>();

        // Clasificar enemigos y puntos
        foreach (Enemy enemy in allEnemies)
        {
            if (enemy is Boss)
            {
                boss = (Boss)enemy; // Asignar al jefe
            }
            else if (!(enemy is BossPoint))
            {
                normalEnemies.Add(enemy); // Agregar enemigos normales
            }
        }

        foreach (BossPoint point in allPoints)
        {
            if (point.GetPointType() == BossPoint.Type.inArea)
            {
                inAreaPoints.Add(point); // Agregar puntos dentro del área
            }
            else if (point.GetPointType() == BossPoint.Type.outArea)
            {
                outAreaPoints.Add(point); // Agregar puntos fuera del área
            }
        }
    }

    public static int AreaPointsDestroyed()
    {
        return outAreaPoints.Count;
    }

    public static int EnemiesDefeated()
    {
        // Contar los enemigos que han sido eliminados
        int defeatedEnemies = 0;
        foreach (Enemy enemy in normalEnemies)
        {
            if (enemy == null || enemy.currentHealth <= 0)
            {
                defeatedEnemies++;
            }
        }

        // Verificar si se ha derrotado al 50% o más de los enemigos
        return defeatedEnemies;
    }
    public static void Refresh(){
        foreach(Enemy enemy in normalEnemies){
            enemy.Respawn();
        }
    }
}
