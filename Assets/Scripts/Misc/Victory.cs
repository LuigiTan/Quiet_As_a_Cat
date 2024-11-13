using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        victoryPanel.SetActive(true);
        collision.gameObject.SetActive(false);
    }
}
