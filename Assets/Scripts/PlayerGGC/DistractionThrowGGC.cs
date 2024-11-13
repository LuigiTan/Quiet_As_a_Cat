using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionThrowGGC : MonoBehaviour
{
   
    public GameObject distractorPrefab; // Prefab del objeto a lanzar
    public float throwForce = 10f; // Fuerza de lanzamiento
    public float spawnOffset = 1f; // Distancia desde el jugador para instanciar el distractor

    [Header("Cooldown")]
    public float throwCooldown = 5f; // Tiempo de espera entre lanzamientos
    public bool canThrow = true; // Controla si el jugador puede lanzar

    [Header("Distractor Limit")]
    public int maxDistractors = 5; // N�mero m�ximo de distractores que el jugador puede lanzar
    private int remainingDistractors; // N�mero de distractores restantes

    public static DistractionThrowGGC instance;

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

    void Start()
    {
        // Inicializa el contador de distractores restantes con el valor m�ximo
        remainingDistractors = maxDistractors;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canThrow) // Lanza con clic izquierdo
        {
            if (remainingDistractors > 0)
            {
                ThrowDistractor();
                StartCoroutine(ThrowCooldownCoroutine());
            }
            else
            {
                Debug.Log("Ya no quedan distracciones disponibles");
            }
        }
        else if (Input.GetMouseButtonDown(0) && !canThrow)
        {
            Debug.Log("Distraccion en Cooldown");
        }
    }

    void ThrowDistractor()
    {
        // Calcula la posici�n del cursor en el mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Asegura que el objeto est� en el plano 2D

        // Calcula la direcci�n del lanzamiento en 2D
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Ajusta la posici�n de inicio del distractor para que est� lejos del jugador
        Vector3 spawnPosition = transform.position + (Vector3)(direction * spawnOffset);

        // Instancia el objeto y aplica la fuerza en el plano 2D
        GameObject distractorInstance = Instantiate(distractorPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = distractorInstance.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * throwForce, ForceMode2D.Impulse);

        // Reduce el contador de distractores restantes y muestra el mensaje en consola
        remainingDistractors--;
        Debug.Log("Quedan " + remainingDistractors + " distracciones");
    }
    IEnumerator ThrowCooldownCoroutine()
    {
        // Activa el cooldown y espera el tiempo especificado
        canThrow = false;
        yield return new WaitForSeconds(throwCooldown);
        canThrow = true;
    }
}
