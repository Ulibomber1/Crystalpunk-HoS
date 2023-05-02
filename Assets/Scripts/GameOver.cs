using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject fadeOut;
    public GameObject restart;

    public void MoveToScene(string SceneName)
    {
        GameManager.Instance.ChangeScene(SceneName);
        
        if (SceneName == "Hub World")
        {
            GameManager.Instance.SetGameState(GameState.HUB_WORLD);
        }
        else if (SceneName == "Start")
        {
            GameManager.Instance.SetGameState(GameState.MAIN_MENU);
        }
    }

    public void Restart(string SceneName)
    {
        Debug.Log("Restarting to HUB!");
        StartCoroutine(Fade(SceneName));

    }

    public void MoveToMain(string SceneName)
    {
        Debug.Log("Back to Main Menu!");
        StartCoroutine(Fade(SceneName));
    }


    private IEnumerator Fade(string SceneName)
    {
        fadeOut.SetActive(true);
        fadeOut.GetComponent<Animator>().Play("MenuFade");
        yield return new WaitForSeconds(1.0f);
        MoveToScene(SceneName);
    }

}
