using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuDK : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused = false;
    public static PauseMenuDK instance;

    // Start is called before the first frame update
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
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Pause");
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true; Why tho
        isPaused = true;
        DistractionThrowGGC.instance.canThrow = false;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        DistractionThrowGGC.instance.canThrow = true;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        pauseMenu.SetActive(false);
    }
}
