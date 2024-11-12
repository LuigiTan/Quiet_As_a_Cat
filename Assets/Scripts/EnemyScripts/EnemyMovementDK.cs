using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementDK : MonoBehaviour
{
    public float anchor1Distance;
    private float anchor2Distance;
    [SerializeField] public float anchorRange;
    [SerializeField] public GameObject player;
    [SerializeField] Transform anchor1;
    [SerializeField] Transform anchor2;
    NavMeshAgent agent;

    [SerializeField] private float FOV;
    [SerializeField] private float viewDistance;
    [SerializeField] private Transform prefabFOV;
    private FieldOfView fov;

    Vector3 aimDirection;
    Vector3 localScale;
    private SpriteRenderer spriteRenderer;

    public static EnemyMovementDK instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(anchor1.position, anchorRange);
        Gizmos.DrawWireSphere(anchor2.position, anchorRange);
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        fov = Instantiate(prefabFOV, null).GetComponent<FieldOfView>();
        fov.SetFOV(FOV);
        fov.SetViewDistance(viewDistance);
        fov.SetAimDirection(aimDirection);
        //localScale = transform.localScale;
    }

    void Update()
    {
        anchor1Distance = Vector2.Distance(transform.position, anchor1.position);
        anchor2Distance = Vector2.Distance(transform.position, anchor2.position);

        if (anchor2Distance <= anchorRange || anchor1Distance <= anchorRange)
            MoveToAnchor();

        aimDirection = agent.velocity.normalized;
        fov.SetOrigin(transform.position);
        fov.SetAimDirection(aimDirection);
        //FindPlayer();
        //agent.speed = 0;
        UpdateFlip();
    }

    void UpdateFlip()
    {
        if (agent.velocity.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (agent.velocity.x > 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void MoveToAnchor()
    {
        StartCoroutine(Waiting());
    }

    IEnumerator Waiting()
    {
        Debug.Log("Enemy is waiting");
        if (anchor2Distance <= anchorRange || transform.position == anchor2.position)
        {
            Debug.Log("Moving to anchor1");
            yield return new WaitForSeconds(2f);
            agent.SetDestination(anchor1.position);
            //localScale.x = -1;
            //transform.localScale = localScale;
        }
        if (anchor1Distance <= anchorRange || transform.position == anchor1.position)
        {
            Debug.Log("Moving to anchor2");
            yield return new WaitForSeconds(2f);
            agent.SetDestination(anchor2.position);
            //localScale.x = 1;
            //transform.localScale = localScale;
        }
    }
}