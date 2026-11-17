using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// Clase específica del enemigo Sporer
public class Sporer : PatrollerEnemy
{
    [Header("Spore Attack Settings")]
    public GameObject sporeObject;
    public float sporeLifetime = 60f;
    private bool firstAttack = true;

    [Header("Escape Settings")]
    public float escapeDistance = 15f;
    private bool isEscaping = false;

    protected override void Awake()
    {
        base.Awake();
        if (sporeObject != null)
        {
            sporeObject.SetActive(false);
        }
    }

    protected override void Update()
    {
        base.Update();

        if (!isEscaping && player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= attackRange && (firstAttack || Time.time >= lastAttackTime + attackCooldown))
            {
                Attack();
                lastAttackTime = Time.time;
                firstAttack = false;
            }
        }
    }

    protected override void Attack()
    {
        base.Attack();

        if (sporeObject != null && !sporeObject.activeInHierarchy)
        {
            sporeObject.transform.position = new Vector3(transform.position.x, sporeObject.transform.position.y, transform.position.z);
            sporeObject.SetActive(true);

            StartCoroutine(DeactivateSporeAfterTime());
            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }
            StartCoroutine(EscapeAfterAttack());
        }
    }

    private IEnumerator DeactivateSporeAfterTime()
    {
        yield return new WaitForSeconds(sporeLifetime);
        if (sporeObject != null)
        {
            sporeObject.SetActive(false);
        }
    }

    private IEnumerator EscapeAfterAttack()
    {
        yield return new WaitForSeconds(1f);
        isEscaping = true;

        Vector3 directionAwayFromPlayer = (transform.position - player.transform.position).normalized;
        Vector3 escapePosition = transform.position + directionAwayFromPlayer * escapeDistance;
        patrolBehavior.GetComponent<NavMeshAgent>().SetDestination(escapePosition);

        yield return new WaitUntil(() => patrolBehavior.GetComponent<NavMeshAgent>().remainingDistance < 0.5f);
        isEscaping = false;
    }

    public override void Die()
    {
        base.Die();
    }
}
