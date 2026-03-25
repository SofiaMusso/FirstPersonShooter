using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform player;

    [HeaderAttribute("movement")]
    public float speed;
    public float maxDistance;

    public float fleeDinstance;
    public float startFleeingDinstance;

    public float followDinstance;
    public float startFollowingDinstance;

    public NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        float distancePlayer = Vector3.Distance(transform.position, player.position);

        if(distancePlayer < startFleeingDinstance)
        {
            Flee();
        }

        if (distancePlayer > startFollowingDinstance)
        {
            Follow();
        }
    }

    private void Flee()
    {
        Vector3 dirToPlayer = transform.position - player.position;

        Vector3 newPos = transform.position + dirToPlayer.normalized * fleeDinstance;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(newPos, out hit, maxDistance, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    private void Follow()
    {
        Vector3 dirToPlayer = player.position - transform.position;

        Vector3 newPos = transform.position + dirToPlayer.normalized * followDinstance;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(newPos, out hit, maxDistance, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}
