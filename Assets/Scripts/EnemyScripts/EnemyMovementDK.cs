using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementDK : MonoBehaviour
{
    private float anchor1Distance;
    private float anchor2Distance;
    [SerializeField] float anchorRange;
    [SerializeField] Transform player;
    [SerializeField] Transform anchor1;
    [SerializeField] Transform anchor2;
    NavMeshAgent agent;

    [SerializeField] private float FOV;
    [SerializeField] private float viewDistance;
    [SerializeField] private Transform prefabFOV;
    private FieldOfView fov;

    Vector3 aimDirection;
    Vector3 localScale;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(anchor1.position, anchorRange);
        Gizmos.DrawWireSphere(anchor2.position, anchorRange);
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        fov = Instantiate(prefabFOV, null).GetComponent<FieldOfView>();
        fov.SetFOV(FOV);
        fov.SetViewDistance(viewDistance);
        fov.SetAimDirection(aimDirection);
    }

    void Update()
    {
        anchor1Distance = Vector2.Distance(transform.position, anchor1.position);
        anchor2Distance = Vector2.Distance(transform.position, anchor2.position);

        if (anchor2Distance <= anchorRange || anchor1Distance <= anchorRange)
            MoveToAnchor();

        aimDirection = agent.velocity.normalized;
        localScale = agent.velocity.normalized;
        fov.SetOrigin(transform.position);
        fov.SetAimDirection(aimDirection);
        FindPlayer();
    }

    void MoveToAnchor()
    {
        StartCoroutine(Waiting());
    }

    IEnumerator Waiting()
    {
        Debug.Log("Enemy is waiting");
        if (fov.CompareTag("Player"))
        {
            Debug.Log("Player Detected");
            agent.Move(Vector3.zero);
        }
        if (anchor2Distance <= anchorRange || transform.position == anchor2.position)
        {
            Debug.Log("Moving to anchor1");
            yield return new WaitForSeconds(2f);
            agent.SetDestination(anchor1.position);
        }
        if (anchor1Distance <= anchorRange || transform.position == anchor1.position)
        {
            Debug.Log("Moving to anchor2");
            yield return new WaitForSeconds(2f);
            agent.SetDestination(anchor2.position);
        }
    }

    private void FindPlayer()
    {
        if (Vector3.Distance(this.transform.position, player.position) < viewDistance)
        {
            Vector3 dirToPlayer = (player.position - this.transform.position).normalized;
            if (Vector3.Angle(aimDirection, dirToPlayer) < FOV / 2f)
            {
                Debug.Log("Player Detected");
            }
        }
    }
}