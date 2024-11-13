using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerGGC : MonoBehaviour
{
    [SerializeField] float PlayerSpeed = 60f;
    [SerializeField] public Rigidbody2D rigidbody2D;
    private Vector3 moveDir;
    public Animator animator;
    public bool isMoving = false;
    SpriteRenderer spriteRenderer;

    public static PlayerControllerGGC instance;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            isMoving = true;
            if (Input.GetKey(KeyCode.W))
            {
                moveY = +1f;
                animator.SetBool("isWalkingUp", true);
                animator.SetBool("isWalkingDown", false);
                animator.SetBool("isWalkingToSide", false);
                animator.SetBool("Idle", false);
                animator.SetBool("Dead", false);
                animator.SetBool("isThrowing", false);
                animator.SetBool("IsIncapacitating", false);
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveY = -1f;
                animator.SetBool("isWalkingUp", false);
                animator.SetBool("isWalkingDown", true);
                animator.SetBool("isWalkingToSide", false);
                animator.SetBool("Idle", false);
                animator.SetBool("Dead", false);
                animator.SetBool("isThrowing", false);
                animator.SetBool("IsIncapacitating", false);
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveX = -1f;
                animator.SetBool("isWalkingUp", false);
                animator.SetBool("isWalkingDown", false);
                animator.SetBool("isWalkingToSide", true);
                animator.SetBool("Idle", false);
                animator.SetBool("Dead", false);
                animator.SetBool("isThrowing", false);
                animator.SetBool("IsIncapacitating", false);
                spriteRenderer.flipX = false;
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveX = +1f;
                animator.SetBool("isWalkingUp", false);
                animator.SetBool("isWalkingDown", false);
                animator.SetBool("isWalkingToSide", true);
                animator.SetBool("Idle", false);
                animator.SetBool("Dead", false);
                animator.SetBool("isThrowing", false);
                animator.SetBool("IsIncapacitating", false);
                spriteRenderer.flipX = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("isWalkingUp", false);
            animator.SetBool("isWalkingDown", false);
            animator.SetBool("isWalkingToSide", false);
            animator.SetBool("Idle", true);
            animator.SetBool("Dead", false);
            animator.SetBool("isThrowing", false);
            animator.SetBool("IsIncapacitating", false);
            isMoving = false;
        }
        moveDir = new Vector3 (moveX, moveY).normalized;
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = moveDir * PlayerSpeed;               
    }

}
