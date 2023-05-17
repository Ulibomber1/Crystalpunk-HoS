using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject fadeOut;
    public GameObject restart;

    private string scenename;
    public void MoveToScene(string SceneName)
    {
        GameManager.Instance.ChangeScene(SceneName);
        GameManager.Instance.SetGameStateByContext();
        /*if (SceneName == "Hub World")
        {
            GameManager.Instance.SetGameState(GameState.HUB_WORLD);
        }
        else if (SceneName == "Start")
        {
            GameManager.Instance.SetGameState(GameState.MAIN_MENU);
        }*/
    }
    public void Restart()
    {
        scenename = GameManager.Instance.getPrevScene();
        Debug.Log("Restarting Current Level!");
        StartCoroutine(Fade(scenename));
    }
    public void MoveToMain()
    {
        scenename = "Start";
        Debug.Log("Back to Main Menu!");
        StartCoroutine(Fade(scenename));
    }
    private IEnumerator Fade(string SceneName)
    {
        fadeOut.SetActive(true);
        fadeOut.GetComponent<Animator>().Play("MenuFade");
        yield return new WaitForSeconds(1.0f);
        MoveToScene(SceneName);
    }
}