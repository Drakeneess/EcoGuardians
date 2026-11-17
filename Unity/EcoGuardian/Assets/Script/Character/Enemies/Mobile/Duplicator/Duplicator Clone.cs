using System.Collections;
using UnityEngine;

public class DuplicatorClone : Enemy
{
    [Header("Clone Settings")]
    public float selfDestructDelay = 3f; // Tiempo antes de destruirse tras el ataque
    public float attackDamage = 5f;
    public float movementSpeed = 3f;
    public float lifetime = 10f; // Tiempo de vida del clon

    private bool hasAttacked = false; // Indica si ya ha atacado
    private Duplicator creator; // Referencia al duplicador que lo creó

    public void Initialize(Duplicator duplicator)
    {
        creator = duplicator;

        // Iniciar el temporizador de vida del clon
        StartCoroutine(LifetimeCountdown());
    }

    protected override void Awake()
    {
        base.Awake();
        // Podrías agregar una animación o sonido de aparición aquí si es necesario
    }

    protected override void Update()
    {
        base.Update();

        if (player == null || hasAttacked) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= attackRange)
        {
            Attack();
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    protected override void Attack()
    {
        base.Attack();

        hasAttacked = true;

        // Activar animación de ataque si está definida
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // Iniciar el autodestrucción tras el ataque
        StartCoroutine(SelfDestruct());
    }

    private void MoveTowardsPlayer()
    {
        // Moverse hacia el jugador restringiendo el eje Y
        Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        Vector3 direction = (targetPosition - transform.position).normalized;

        transform.position += direction * movementSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Shield"))
        {
            Die(); // Destruir el clon al chocar con un escudo
            return;
        }

        if (other.transform.CompareTag("Player") && other.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage(attackDamage); // Infligir daño al jugador
            Die(); // Destruir el clon tras impactar
        }
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(selfDestructDelay);
        Die();
    }

    private IEnumerator LifetimeCountdown()
    {
        yield return new WaitForSeconds(lifetime);
        Die();
    }

    public override void Die()
    {
        // Notificar al duplicador antes de destruirse
        creator?.RemoveClone(this);

        // Aquí puedes agregar lógica de destrucción adicional (ejemplo: efectos visuales)
        Destroy(gameObject);
    }
    public override void TakeDamage(float damage)
    {
    }
}
