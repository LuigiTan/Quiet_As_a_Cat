using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDetectionDK : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    public Animator animator;
    public GameObject player;
    public Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();;
        rb = GetComponent<Rigidbody2D>();
    }

    public void GameOver()
    {
        animator.SetBool("isWalkingUp", false);
        animator.SetBool("isWalkingDown", false);
        animator.SetBool("isWalkingToSide", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Dead", true);
        animator.SetBool("isThrowing", false);
        animator.SetBool("IsIncapacitating", false);
        
        PlayerControllerGGC.instance.rigidbody2D.velocity = Vector3.zero;
        player.GetComponent<PlayerControllerGGC>().enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    public void Dead()
    {
        Debug.Log("Player died");
        gameOverPanel.SetActive(true);
        gameObject.SetActive(false);  
    }
}
