using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// Clase base que encapsula el comportamiento de patrullaje
public class PatrolBehavior : MonoBehaviour
{
    public Transform patrolArea;
    public float patrolAreaWidth = 10f;
    public float patrolAreaHeight = 10f;
    public float patrolWaitTime = 2f;

    private NavMeshAgent navMeshAgent;
    private bool isPatrolling = true;

    protected virtual void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Método para manejar la patrulla dentro de un área
    public void Patrol()
    {
        if (isPatrolling && !navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            StartCoroutine(WaitAndMoveToRandomPoint());
        }
    }

    // Corrutina para esperar un tiempo y luego moverse a un punto aleatorio dentro del área
    private IEnumerator WaitAndMoveToRandomPoint()
    {
        isPatrolling = false;
        yield return new WaitForSeconds(patrolWaitTime);

        Vector3 randomPosition = GetRandomPointWithinArea();
        navMeshAgent.SetDestination(randomPosition);
        isPatrolling = true;
    }

    // Método para obtener un punto aleatorio dentro del área de patrullaje
    private Vector3 GetRandomPointWithinArea()
    {
        float randomX = Random.Range(-patrolAreaWidth / 2f, patrolAreaWidth / 2f);
        float randomZ = Random.Range(-patrolAreaHeight / 2f, patrolAreaHeight / 2f);
        Vector3 randomPoint = patrolArea.position + new Vector3(randomX, 0.3f, randomZ);
        return randomPoint;
    }
}