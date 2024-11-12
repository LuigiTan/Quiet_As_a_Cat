using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionDK : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    public static PlayerDetectionDK instance;

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

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
