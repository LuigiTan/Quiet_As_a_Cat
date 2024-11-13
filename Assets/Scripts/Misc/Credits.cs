using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;

    public void StartMenu()
    {
        creditsPanel.SetActive(false);
    }

    public void CreditsPanel()
    {
        creditsPanel.SetActive(true);
    }
}
