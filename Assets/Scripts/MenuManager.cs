using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject fileSelectMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    public GameObject areYouSure;
    public GameObject fadeOut;

    private void Start()
    {
        BackToMainMenu();
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

    public void AreYouSure()//asks the player if they are sure about starting new game
    {
        creditsMenu.SetActive(false);
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        fileSelectMenu.SetActive(false);
        areYouSure.SetActive(true);
    }

    public void OpenCredits()
    {
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        fileSelectMenu.SetActive(false);
        areYouSure.SetActive(false);
    }

    public void BackToMainMenu()
    {
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        fileSelectMenu.SetActive(false);
        areYouSure.SetActive(false);
    }

    public void OpenSettings()
    {
        creditsMenu.SetActive(false);
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        fileSelectMenu.SetActive(false);
        areYouSure.SetActive(false);
    }

    public void OpenFileSelect()
    {
        fileSelectMenu.SetActive(true);
        creditsMenu.SetActive(false);
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        areYouSure.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
