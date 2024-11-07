using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionDK : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Enemy Detected Player");
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("Game Over");
    }
}
