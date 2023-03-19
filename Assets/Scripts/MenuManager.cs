using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    public void MoveToScene(string SceneName)
    {
        GameManager.Instance.ChangeScene(SceneName);
    }

    public void OpenCredits()
    {
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void BackToMainMenu()
    {
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void OpenSettings()
    {
        creditsMenu.SetActive(false);
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void OpenSave()
    {

    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
