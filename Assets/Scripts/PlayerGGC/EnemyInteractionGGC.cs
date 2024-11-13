using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyInteractionGGC : MonoBehaviour
{
    public int maxIncapacitateUses = 3; // Número máximo de veces que se puede incapacitar a enemigos
    public Animator animator;
    private int remainingUses; // Contador de usos restantes

    private Collider2D enemyColliderInRange; // Referencia al collider del enemigo con el que se puede interactuar

    void Start()
    {
        // Inicializa los usos restantes al valor máximo
        remainingUses = maxIncapacitateUses;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Verifica si se presiona la tecla "E"
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (enemyColliderInRange != null)
            {
                if (remainingUses > 0)
                {
                    IncapacitateEnemy();
                    remainingUses--;
                    Debug.Log("Quedan " + remainingUses + " ataques");
                }
                else
                {
                    Debug.Log("No quedan ataques");
                }
            }
            else
            {
                Debug.Log("Fuera de rango para incapacitar");
            }
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Incapacitate") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            animator.SetBool("isWalkingUp", false);
            animator.SetBool("isWalkingDown", false);
            animator.SetBool("isWalkingToSide", false);
            animator.SetBool("Idle", true);
            animator.SetBool("Dead", false);
            animator.SetBool("isThrowing", false);
            animator.SetBool("IsIncapacitating", false);
        }

    }

    void IncapacitateEnemy()
    {
        // Desactiva el objeto del enemigo
        animator.SetBool("isWalkingUp", false);
        animator.SetBool("isWalkingDown", false);
        animator.SetBool("isWalkingToSide", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Dead", false);
        animator.SetBool("isThrowing", false);
        animator.SetBool("IsIncapacitating", true);

        EnemyMovementDK enemyMovement = enemyColliderInRange.GetComponentInParent<EnemyMovementDK>();
        if (enemyMovement != null)
        {
            enemyMovement.fieldOfView.transform.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("EnemyMovementDK component not found on " + enemyColliderInRange.name);
        }
        enemyColliderInRange.transform.parent.gameObject.SetActive(false);


        // Aquí irían las instrucciones futuras para animar y desactivar componentes del enemigo
        // Ejemplo:
        // enemyColliderInRange.transform.parent.GetComponent<EnemyAI>().enabled = false;
        // Reproducir una animación antes de desactivar los componentes del enemigo
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Comprueba si el jugador ha entrado en el collider de un enemigo
        if (other.CompareTag("Enemy"))
        {
            enemyColliderInRange = other;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Comprueba si el jugador ha salido del collider del enemigo
        if (other == enemyColliderInRange)
        {
            enemyColliderInRange = null;
        }
    }
}
