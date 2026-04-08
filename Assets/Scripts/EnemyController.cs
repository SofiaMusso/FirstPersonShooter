using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform player;

    [HeaderAttribute("movement")]
    public float speed;
    public float maxDistance;

    public float fleeDistance;
    public float followDistance;

    public NavMeshAgent agent;

    [HeaderAttribute("health")]
    public int maxHealth = 100;
    int currentHealth;

    public EnemyUIManager healthBar;
    public UIManager ui;

    [HeaderAttribute("attack")]
    public float attackDistance = 5f;
    public int damage = 10;
    public float attackRate = 1f;
    private float nextAttackTime;

    private State currentState;

    [HeaderAttribute("shot")]
    public LineRenderer lineRenderer;
    public Transform shootPoint; // il punto da cui parte il laser (es. testa o canna)
    public float shootDuration = 0.1f; // quanto tempo resta visibile il laser

    public AudioSource shootSound;

    enum State
    {
        Idle,
        Follow,
        Flee
    }

    void Start()
    {
        lineRenderer.enabled = false;

        healthBar = GetComponentInChildren<EnemyUIManager>();
        ui = FindAnyObjectByType<UIManager>();

        currentHealth = maxHealth;
        healthBar.Setup(maxHealth);

        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);


        if (distance < fleeDistance)
        {
            currentState = State.Flee;
        }
        else if(currentHealth <= 40)
        {
            currentState = State.Flee;
        }
        else if (distance < followDistance)
        {
            currentState = State.Follow;
        }
        else
        {
            currentState = State.Idle;
        }

        switch (currentState)
        {
            case State.Follow:
                Follow();
                break;

            case State.Flee:
                Flee();
                break;

            case State.Idle:
                agent.SetDestination(transform.position);
                break;
        }

        if (distance < attackDistance && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    private void Flee()
    {
        Vector3 dirToPlayer = transform.position - player.position;

        Vector3 newPos = transform.position + dirToPlayer.normalized * fleeDistance;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(newPos, out hit, maxDistance, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    private void Follow()
    {
        agent.SetDestination(player.position);
    }

    void Attack()
    {
        // Attiva Line Renderer per il laser
        if (lineRenderer != null && shootPoint != null)
        {
            lineRenderer.SetPosition(0, shootPoint.position); // inizio del laser
            lineRenderer.SetPosition(1, player.position);     // fine del laser
            lineRenderer.enabled = true;

            // Disattiva dopo pochi frame
            StartCoroutine(DisableLaser());
        }

        if (shootSound != null)
        {
            shootSound.Play();
        }

        PlayerHealth ph = player.GetComponent<PlayerHealth>();

        if (ph != null)
        {
            ph.TakeDamage(damage);
        }

    }


    public void EnemyTakeDamage(int dmg)
    {
        currentHealth -= dmg;

        healthBar.UpdateHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
            ui.enemyCurrentNum --;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator DisableLaser()
    {
        yield return new WaitForSeconds(shootDuration);
        lineRenderer.enabled = false;
    }
}
