using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum GameState { MAIN_MENU, HUB_WORLD, LEVEL_0, LEVEL_FACTORY, LEVEL_LAVA, LEVEL_AIR, PAUSED, GAME_OVER}

public delegate void OnStateChangeHandler();

public class GameManager : MonoBehaviour
{
    // Singleton Pattern
    protected GameManager() { }
    private static GameManager instance = null;
    public event OnStateChangeHandler OnStateChange;
    public GameState gameState { get; private set; }

    public static GameManager Instance
    {
        get
        {
            if (GameManager.instance == null)
            {
                DontDestroyOnLoad(GameManager.instance);
                GameManager.instance = new GameManager { };
            }
            return GameManager.instance;
        }
    }

    public void SetGameState(GameState state)
    {
        this.gameState = state;
        OnStateChange();
    }

    public void OnApplicationQuit()
    {
        GameManager.instance = null;
    }
    // Singleton Pattern End

    
    public void ChangeScene(string sceneName)
    {
        switch (gameState)
        {
            case GameState.MAIN_MENU:
                // Change to a level based upon save file
                break;
            case GameState.LEVEL_0:
                // Change to Hub World, nothing else
                break;
            case GameState.HUB_WORLD:
                // Change to a level but cannot go to level 0
                break;
            case GameState.LEVEL_FACTORY:
                // Change to Hub World, nothing else
                break;
            case GameState.LEVEL_LAVA:
                // Change to Hub World, nothing else
                break;
            case GameState.LEVEL_AIR:
                // Change to Hub World, nothing else
                break;
            case GameState.PAUSED:
                // Change to either main menu or Hub World, nothing else
                break;
            case GameState.GAME_OVER:
                // Change to Hub World, restart scene, or go to Main Menu
                break;
        }

        void ChangeBasedOnSave()
        {

        }

        void ChangeSpecific(string sceneName)
        {

        }

        // only here so I can move it to other places in code
        SceneManager.LoadScene(sceneName);
    }
}
