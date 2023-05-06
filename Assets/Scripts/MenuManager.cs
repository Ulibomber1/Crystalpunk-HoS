using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject shop;

    [Header("Player")]
    public PlayerController playerController;

    private bool isPaused = false;

    public int doubleJumpPrice = 20;

    public delegate void OnExitSettingsHandler();
    public static event OnExitSettingsHandler SettingsExited;

    public bool IsPaused()
    {
        return isPaused;
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
        GameManager.Instance.SetGameState(GameState.PAUSED); // Yes this is right. ;P
        InGameSwitch("Pause");
        Time.timeScale = 0; // Make sure Input System Package setting "Update Mode" is set to "Dynamic Update", otherwise it will not work
    }

    public void Unpause()
    {
        if (!isPaused)
            return;
        isPaused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.SetGameStateByContext();
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

    public void OpenShop()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        InGameSwitch("Shop");
    }

    public void CloseShop()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        InGameSwitch("HUD");
    }

    public void PurchaseBoots()
    {
        //take money here
        int gears = playerController.GetGearTotal();
        Debug.Log(gears);
        if (gears >= doubleJumpPrice)
        {
            playerController.doubleJumpUnlocked = true;
            //take gears away
        }
        
    }

    private void InGameSwitch(string ui)
    {
        switch (ui)
        {
            case "HUD":
                HUD.SetActive(true);
                pauseMenu.SetActive(false);
                inGameSettings.SetActive(false);
                shop.SetActive(false);
                break;
            case "Pause":
                HUD.SetActive(false);
                pauseMenu.SetActive(true);
                inGameSettings.SetActive(false);
                shop.SetActive(false);
                break;
            case "Settings":
                HUD.SetActive(false);
                pauseMenu.SetActive(false);
                inGameSettings.SetActive(true);
                shop.SetActive(false);
                break;
            case "Shop":
                HUD.SetActive(false);
                pauseMenu.SetActive(false);
                inGameSettings.SetActive(false);
                shop.SetActive(true);
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
