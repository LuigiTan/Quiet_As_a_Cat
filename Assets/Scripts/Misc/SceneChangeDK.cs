using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeDK : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Home(string home)
    {
        Time.timeScale = 1.0f;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = true;
        PauseMenuDK.instance.isPaused = false;
        PauseMenuDK.instance.pauseMenu.SetActive(false);
        SceneManager.LoadScene(home);
    }

    public void Play(string gameMain)
    {
        SceneManager.LoadScene(gameMain);
    }
}
