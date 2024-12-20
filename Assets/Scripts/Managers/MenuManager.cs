using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Start Menu")]
    public GameObject mainMenu;
    public GameObject fileSelectMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    public GameObject areYouSure;
    public GameObject fadeOut;
    public GameObject noSaveData;

    [Header("In Game Menu")]
    public GameObject pauseMenu;
    public GameObject inGameSettings;
    public GameObject HUD;
    public GameObject shop;
    public GameObject dialogueBox;
    public GameObject doubleJumpSold;
    public GameObject doubleJumpButton;
    public GameObject insufficientFunds;
    public TextMeshProUGUI dialogueName;
    public Dialogue dialogue;
    public OpenDialogue openDialogue;
    public GameObject thanks;

    [Header("Loading Screen")]
    public GameObject LoadingScreen;
    public Image LoadingBarFill;

    [Header("Player")]
    public PlayerController playerController;

    private bool isPaused = false;
    private bool isShop = false;
    private bool isDialogue = false;
    public bool shopUnlocked = false;

    public int doubleJumpPrice = 20;
    public int healthPrice = 5;
    public int ammoPrice = 5;

    public delegate void OnExitSettingsHandler();
    public static event OnExitSettingsHandler SettingsExited;

    public void MenuNext()
    {
        SoundManager.Instance.PlaySound("menuNext", 1.0f);
    }

    public void MenuBack()
    {
        SoundManager.Instance.PlaySound("menuBack", 1.0f);
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public bool IsShop()
    {
        return isShop;
    }

    public bool IsDialogue()
    {
        return isDialogue;
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
        shopUnlocked = SaveData.shopUnlocked;
    }

    public void Pause()
    {
        SoundManager.Instance.PlaySound("menuPause", 1.0f);
        isPaused = true; isShop = false;
        Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.SetGameState(GameState.PAUSED); // Yes this is right. ;P
        InGameSwitch("Pause");
        Time.timeScale = 0; // Make sure Input System Package setting "Update Mode" is set to "Dynamic Update", otherwise it will not work
    }

    public void Unpause()
    {
        SoundManager.Instance.PlaySound("menuPause", 1.0f);
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

    public void OpenShop(string[] text)
    {
        if (shopUnlocked)
        {
            GameManager.Instance.SetGameState(GameState.SHOP);
            isPaused = false; isShop = true; isDialogue = false;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            InGameSwitch("Shop");
        }
        else
        { 
            OpenDialogue("Shop Keeper", text);
        }
    }

    public void CloseShop()
    {
        GameManager.Instance.SetGameStateByContext();
        isPaused = false; isShop = false; isDialogue = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        InGameSwitch("HUD");
    }

    public void OpenDialogue(string name, string[] text)
    {
        dialogueBox.GetComponent<Animator>().Play("OpenDialogue");
        GameManager.Instance.SetGameState(GameState.DIALOGUE);
        isPaused = false; isShop = false; isDialogue = true;
        playerController.SetSpeedZero();
        //Time.timeScale = 0;
        InGameSwitch("Dialogue");
        dialogueName.SetText(name);
        dialogue.SetText(text);
        dialogue.StartDialogue();
    }

    public void CloseDialogue()
    {
        dialogueBox.GetComponent<Animator>().Play("CloseDialogue");
        GameManager.Instance.SetGameStateByContext();
        isPaused = false; isShop = false; isDialogue = false;
        //Time.timeScale = 1;
        Invoke("CloseDialogueDelay", 0.3f);
    }

    private void CloseDialogueDelay()
    {
        InGameSwitch("HUD");
        openDialogue.DoneTalking();
    }

    public void NextDialogue()
    {
        dialogue.Click();
    }

    public void PurchaseBoots()
    {
        if (playerController.GetGearTotal() >= doubleJumpPrice)
        {
            SoundManager.Instance.PlaySound("purchase", 1.0f);
            playerController.SubtractGears(doubleJumpPrice);
            playerController.doubleJumpUnlocked = true;
            playerController.ActivateBoots();
            playerController.ActivateBoots();
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
            SoundManager.Instance.PlaySound("purchase", 1.0f);
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
            SoundManager.Instance.PlaySound("purchase", 1.0f);
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

    public void InGameSwitch(string ui)
    {
        switch (ui)
        {
            case "HUD":
                HUD.SetActive(true);
                pauseMenu.SetActive(false);
                inGameSettings.SetActive(false);
                shop.SetActive(false);
                dialogueBox.SetActive(false);
                thanks.SetActive(false);
                LoadingScreen.SetActive(false);
                break;
            case "Pause":
                HUD.SetActive(false);
                pauseMenu.SetActive(true);
                inGameSettings.SetActive(false);
                shop.SetActive(false);
                thanks.SetActive(false);
                break;
            case "Settings":
                HUD.SetActive(false);
                pauseMenu.SetActive(false);
                inGameSettings.SetActive(true);
                shop.SetActive(false);
                thanks.SetActive(false);
                break;
            case "Shop":
                HUD.SetActive(true); //stays active to show gear count
                pauseMenu.SetActive(false);
                inGameSettings.SetActive(false);
                shop.SetActive(true);
                dialogueBox.SetActive(false);
                thanks.SetActive(false);
                break;
            case "Dialogue":
                HUD.SetActive(true);
                pauseMenu.SetActive(false);
                inGameSettings.SetActive(false);
                shop.SetActive(false);
                dialogueBox.SetActive(true);
                thanks.SetActive(false);
                break;
            case "Thanks":
                HUD.SetActive(false);
                pauseMenu.SetActive(false);
                inGameSettings.SetActive(false);
                shop.SetActive(false);
                dialogueBox.SetActive(false);
                thanks.SetActive(true);
                break;
            default:
                break;
        }
    }


    public void MoveToScene(string SceneName)
    {
        //GameManager.Instance.ChangeScene(SceneName);
        StartCoroutine(LoadNextScene(SceneName));
    }

    private IEnumerator LoadNextScene(string SceneName)
    {
        LoadingScreen.SetActive(true);
        LoadingBarFill.fillAmount = 0;
        //yield return new WaitForSeconds(1.0f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneName); //does not change the game state, need to fix
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingBarFill.fillAmount = progressValue;

            yield return null;
        }
        //MoveToScene(SceneName); //might conflict with loading bar
    }

    public void SendToMainMenu(string SceneName)
    {
        SoundManager.Instance.PlayMusic("menuMusic");
        GameManager.Instance.SetGameState(GameState.MAIN_MENU);
        Time.timeScale = 1;
        GameManager.Instance.ChangeScene(SceneName);
    }

    public void LoadGame()
    {
        StartCoroutine(NoSaveData());
        Debug.Log("We don't have a save system to load from yet!");
    }

    private IEnumerator NoSaveData()
    {
        noSaveData.SetActive(true);
        yield return new WaitForSecondsRealtime(1.5f);
        noSaveData.SetActive(false);
    }

    public void NewGame(string SceneName)
    {
        Debug.Log("Starting new game!");
        StartCoroutine(NewGameFade(SceneName));
        
    }

    private IEnumerator NewGameFade(string SceneName)
    {
        
        fadeOut.SetActive(true);
        LoadingScreen.SetActive(true);
        fadeOut.GetComponent<Animator>().Play("MenuFade");
        LoadingBarFill.fillAmount = 0;
        yield return new WaitForSeconds(0.5f);
        SoundManager.Instance.PlayMusic("hubMusic");
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneName); //does not change the game state, need to fix
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingBarFill.fillAmount = progressValue;

            yield return null;
        }
        //MoveToScene(SceneName); //might conflict with loading bar
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
                LoadingScreen.SetActive(false);
                break;
            case "Main Menu":
                creditsMenu.SetActive(false);
                mainMenu.SetActive(true);
                settingsMenu.SetActive(false);
                fileSelectMenu.SetActive(false);
                areYouSure.SetActive(false);
                LoadingScreen.SetActive(false);
                break;
            case "Settings":
                creditsMenu.SetActive(false);
                mainMenu.SetActive(false);
                settingsMenu.SetActive(true);
                fileSelectMenu.SetActive(false);
                areYouSure.SetActive(false);
                LoadingScreen.SetActive(false);
                break;
            case "File Select":
                creditsMenu.SetActive(false);
                mainMenu.SetActive(false);
                settingsMenu.SetActive(false);
                fileSelectMenu.SetActive(true);
                areYouSure.SetActive(false);
                LoadingScreen.SetActive(false);
                break;
            case "Are You Sure":
                creditsMenu.SetActive(false);
                mainMenu.SetActive(false);
                settingsMenu.SetActive(false);
                fileSelectMenu.SetActive(false);
                areYouSure.SetActive(true);
                LoadingScreen.SetActive(false);
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
