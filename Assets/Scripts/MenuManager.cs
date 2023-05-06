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
    public GameObject doubleJumpSold;
    public GameObject doubleJumpButton;
    public GameObject insufficientFunds;

    [Header("Player")]
    public PlayerController playerController;

    private bool isPaused = false;
    private bool isShop = false;

    public int doubleJumpPrice = 20;
    public int healthPrice = 5;
    public int ammoPrice = 5;

    public delegate void OnExitSettingsHandler();
    public static event OnExitSettingsHandler SettingsExited;

    public bool IsPaused()
    {
        return isPaused;
    }

    public bool IsShop()
    {
        return isShop;
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Start")
        {
            BackToMainMenu();
        }
        else
        {
            StartOfScene();
        }
    }
    public void StartOfScene()
    {
        InGameSwitch("HUD");
        Time.timeScale = 1;
        if(playerController.doubleJumpUnlocked == true)
        {
            doubleJumpSold.SetActive(true);
            doubleJumpButton.SetActive(false);
        }
        else
        {
            doubleJumpSold.SetActive(false);
            doubleJumpButton.SetActive(true);
        }

    }

    public void Pause()
    {
        isPaused = true; isShop = false;
        Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.SetGameState(GameState.PAUSED); // Yes this is right. ;P
        InGameSwitch("Pause");
        Time.timeScale = 0; // Make sure Input System Package setting "Update Mode" is set to "Dynamic Update", otherwise it will not work
    }

    public void Unpause()
    {
        if (!isPaused)
            return;
        isPaused = false; isShop = false;
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
        GameManager.Instance.SetGameState(GameState.SHOP);
        isPaused = false; isShop = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        InGameSwitch("Shop");
    }

    public void CloseShop()
    {
        GameManager.Instance.SetGameStateByContext();
        isPaused = false; isShop = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        InGameSwitch("HUD");
    }

    public void PurchaseBoots()
    {
        if (playerController.GetGearTotal() >= doubleJumpPrice)
        {
            playerController.SubtractGears(doubleJumpPrice);
            playerController.doubleJumpUnlocked = true;
            doubleJumpSold.SetActive(true);
            doubleJumpButton.SetActive(false);
        }
        else
            StartCoroutine(NotEnoughMoney()); 
    }

    public void PurchaseHealth()
    {
        if (playerController.GetGearTotal() >= healthPrice)
        {
            playerController.SubtractGears(healthPrice);
            playerController.SetHealthFull();
        }
        else
            StartCoroutine(NotEnoughMoney());
    }

    public void PurchaseAmmo()
    {
        if (playerController.GetGearTotal() >= ammoPrice)
        {
            playerController.SubtractGears(ammoPrice);
            Debug.Log("Idk how to refill ammo");
        }
        else
            StartCoroutine(NotEnoughMoney());
    }

    public void PurchaseTheGame()
    {
        StartCoroutine(NotEnoughMoney());
    }

    private IEnumerator NotEnoughMoney()
    {
        insufficientFunds.SetActive(true);
        yield return new WaitForSecondsRealtime(1.5f);
        insufficientFunds.SetActive(false);
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
                HUD.SetActive(true); //stays active to show gear count
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
