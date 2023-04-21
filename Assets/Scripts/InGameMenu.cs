using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class InGameMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject HUD;

    void OnPause()
    {
        Pause();
    }

    private void Start()
    {
        StartOfScene();
    }

    public void MoveToScene(string SceneName)
    {
        GameManager.Instance.ChangeScene(SceneName);
    }

    public void StartOfScene()
    {
        HUD.SetActive(true);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.SetGameState(GameState.PAUSED); // I dont know if this is right?
        HUD.SetActive(false);
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
        Time.timeScale = 0; //Timescale is a controvertial way to pause, needs more research
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        //GameManager.Instance.SetGameState(Name of scene); 
        HUD.SetActive(true);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        
    }

    public void OpenSettings()
    {
        HUD.SetActive(false);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
