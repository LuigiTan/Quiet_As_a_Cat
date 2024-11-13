using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractorGGC : MonoBehaviour
{
    public float stopThreshold = 0.1f; // Velocidad m�nima para considerar que el objeto est� detenido
    public float detectionRadius = 5f; // Radio para detectar enemigos cercanos
    public float delayBeforeDestruction = 2f; // Tiempo en segundos antes de destruir el objeto despu�s de detenerse

    private Rigidbody2D rb;
    public bool hasActivated = false;
    public Vector3 distractorPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Detecta cuando el objeto est� en reposo y a�n no ha activado a los enemigos
        if (rb.velocity.magnitude < stopThreshold && !hasActivated)
        {
            hasActivated = true; // Marca que ya se activ� para evitar m�ltiples activaciones
            ActivateNearbyEnemies();
            StartCoroutine(DestroyAfterDelay()); // Inicia la corrutina para destruir despu�s de unos segundos
            distractorPosition = GetComponent<DistractorGGC>().transform.position;
            

        }
    }

    public void ActivateNearbyEnemies()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        foreach (Collider2D col in colliders)
        {
            // Aqu� ir�a la llamada a cada enemigo encontrado
            if (col.CompareTag("Enemy"))
            {
                // Ejemplo de llamada al script del enemigo
                // col.GetComponent<Enemy>().MoveTowards(transform.position);
                Debug.Log("Enemigo detectado y activado"); // Confirmaci�n de activaci�n en el log
                col.gameObject.SetActive(false);
                EnemyMovementDK enemyMovement = col.GetComponent<EnemyMovementDK>();
                if (enemyMovement != null)
                {
                    enemyMovement.fieldOfView.transform.gameObject.SetActive(false);
                }
                else
                {
                    Debug.LogError("EnemyMovementDK component not found on " + col.name);
                }
            }
        }
    }

    IEnumerator DestroyAfterDelay()
    {
        // Espera el tiempo especificado antes de destruir el objeto
        yield return new WaitForSeconds(delayBeforeDestruction);
        Destroy(gameObject); // Destruye el objeto
    }

    void OnDrawGizmosSelected()
    {
        // Visualiza el �rea de detecci�n en la vista de escena
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
