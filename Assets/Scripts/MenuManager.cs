using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    [Header("Start Menu")]
    public GameObject mainMenu;
    public GameObject fileSelectMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    public GameObject areYouSure;
    public GameObject fadeOut;

    [Header("In Game Menu")]
    public GameObject pauseMenu;
    public GameObject inGameSettings;
    public GameObject HUD;

    bool isPaused = false;

    public delegate void OnExitSettingsHandler();
    public static event OnExitSettingsHandler SettingsExited;

    void OnPause()
    {
        if (isPaused)
            Unpause();
        else
            Pause();
    }

    private void Start()
    {
        BackToMainMenu();
    }
    public void StartOfScene()
    {
        InGameSwitch("HUD");
        Time.timeScale = 1;
    }

    public void Pause()
    {
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.SetGameState(GameState.PAUSED); // I dont know if this is right?
        InGameSwitch("Pause");
        Time.timeScale = 0; //Timescale is a controvertial way to pause, needs more research
    }

    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1;
        //GameManager.Instance.SetGameState(Name of scene); 
        InGameSwitch("HUD");

    }

    public void OpenInGameSettings()
    {
        InGameSwitch("Settings");
    }

    public void BackOutInGameSettings()
    {
        SettingsExited?.Invoke();
        InGameSwitch("Pause");
    }

    private void InGameSwitch(string ui)
    {
        switch (ui)
        {
            case "HUD":
                HUD.SetActive(true);
                pauseMenu.SetActive(false);
                inGameSettings.SetActive(false);
                break;
            case "Pause":
                HUD.SetActive(false);
                pauseMenu.SetActive(true);
                inGameSettings.SetActive(false);
                break;
            case "Settings":
                HUD.SetActive(false);
                pauseMenu.SetActive(false);
                inGameSettings.SetActive(true);
                break;
            default:
                break;
        }
    }


    public void MoveToScene(string SceneName)
    {
        GameManager.Instance.ChangeScene(SceneName);
    }

    public void LoadGame()
    {
        Debug.Log("We don't have a save system to load from yet!");
    }

    public void NewGame(string SceneName)
    {
        Debug.Log("Starting new game!");
        StartCoroutine(Fade(SceneName));
        
    }

    private IEnumerator Fade(string SceneName)
    {
        fadeOut.SetActive(true);
        fadeOut.GetComponent<Animator>().Play("MenuFade");
        yield return new WaitForSeconds(1.0f);
        MoveToScene(SceneName);
    }

    public void OpenCredits()
    {
        MenuSwitch("Credits");
    }

    public void BackToMainMenu()
    {
        SettingsExited?.Invoke();
        MenuSwitch("Main Menu");
    }

    public void OpenSettings()
    {
        MenuSwitch("Settings");
    }
    public void OpenFileSelect()
    {
        MenuSwitch("File Select");
    }

    public void AreYouSure()//asks the player if they are sure about starting new game
    {
        MenuSwitch("Are You Sure");
    }

    private void MenuSwitch(string ui)
    {
        switch (ui)
        {
            case "Credits":
                creditsMenu.SetActive(true);
                mainMenu.SetActive(false);
                settingsMenu.SetActive(false);
                fileSelectMenu.SetActive(false);
                areYouSure.SetActive(false);
                break;
            case "Main Menu":
                creditsMenu.SetActive(false);
                mainMenu.SetActive(true);
                settingsMenu.SetActive(false);
                fileSelectMenu.SetActive(false);
                areYouSure.SetActive(false);
                break;
            case "Settings":
                creditsMenu.SetActive(false);
                mainMenu.SetActive(false);
                settingsMenu.SetActive(true);
                fileSelectMenu.SetActive(false);
                areYouSure.SetActive(false);
                break;
            case "File Select":
                creditsMenu.SetActive(false);
                mainMenu.SetActive(false);
                settingsMenu.SetActive(false);
                fileSelectMenu.SetActive(true);
                areYouSure.SetActive(false);
                break;
            case "Are You Sure":
                creditsMenu.SetActive(false);
                mainMenu.SetActive(false);
                settingsMenu.SetActive(false);
                fileSelectMenu.SetActive(false);
                areYouSure.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
