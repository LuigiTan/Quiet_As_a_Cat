using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementDK : MonoBehaviour
{
    public GameObject home;
    public GameObject player;
    [SerializeField] float range;
    public float speed;
    private float distance;
    private float distancePlayer;
    Rigidbody2D rb;
    Vector3 localScale;
    float dirX;

    void Start()
    {
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, home.transform.position);
        distancePlayer = Vector2.Distance(player.transform.position, home.transform.position);
        Vector2 directionPlayer = player.transform.position - transform.position;
        directionPlayer.Normalize();


        if (distance < range && distancePlayer < range)
        {
            Debug.Log("Moving towards Player");
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("Moving to Home");
            transform.position = Vector2.MoveTowards(this.transform.position, home.transform.position, speed * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        CheckWhereToFace();
    }

    void CheckWhereToFace()
    {
        dirX = transform.position.x;

        if ((dirX - player.transform.position.x < 0 && distancePlayer < range) || (distancePlayer > range && dirX < home.transform.position.x))
        {
            localScale.x = 1;
        }
        else
        {
            localScale.x = -1;
        }
        transform.localScale = localScale;
    }
}
