using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    public Animator animator;
    public Transform player;

    [Header("Settings")]
    public float chaseRange = 15f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;

    private float lastAttackTime = 0f;

    void Start()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        if (animator == null) animator = GetComponent<Animator>();

        // Find player by tag if not assigned
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // If in chase range -> chase player
        if (distance <= chaseRange && distance > attackRange)
        {
            ChasePlayer();
        }
        // If in attack range -> stop & attack
        else if (distance <= attackRange)
        {
            AttackPlayer();
        }
        else
        {
            Idle();
        }
    }

    void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);

        if (animator != null)
            animator.SetBool("IsMoving", true);
    }

    void AttackPlayer()
    {
        agent.isStopped = true;

        // Face the player
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(direction),
            Time.deltaTime * 7f
        );

        if (animator != null)
            animator.SetBool("IsMoving", false);

        // Attack cooldown logic
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            if (animator != null)
                animator.SetTrigger("Attack");

            // TODO: Apply damage to player here
            player.GetComponent<PlayerHealth>().TakeDamage(20);

            lastAttackTime = Time.time;
        }
    }

    void Idle()
    {
        agent.isStopped = true;
        if (animator != null)
            animator.SetBool("IsMoving", false);
    }
}
