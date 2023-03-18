using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum GameState { MAIN_MENU, HUB_WORLD, LEVEL_0, LEVEL_FACTORY, LEVEL_LAVA, LEVEL_AIR, PAUSED, GAME_OVER}

public delegate void OnStateChangeHandler();

public class GameManager : MonoBehaviour
{
    [SerializeField] private string hubWorldSceneName;
    [SerializeField] private string levelZeroSceneName;
    [SerializeField] private string mainMenuSceneName;

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
                ChangeBasedOnSave();
                break;
            case GameState.LEVEL_0:
                ChangeFromLevel();
                break;
            case GameState.HUB_WORLD:
                ChangeFromHub();
                break;
            case GameState.LEVEL_FACTORY:
                ChangeFromLevel();
                break;
            case GameState.LEVEL_LAVA:
                ChangeFromLevel();
                break;
            case GameState.LEVEL_AIR:
                ChangeFromLevel();
                break;
            case GameState.PAUSED:
                ChangeFromPause();
                break;
            case GameState.GAME_OVER:
                ChangeFromGameOver();
                break;
        }

        void ChangeBasedOnSave()
        {
            if (sceneName != mainMenuSceneName)
            {
                Debug.LogError("Cannot change based on a save file from anywhere except the main menu.");
                return;
            }
            // Need to load save, then intialize the scene using that save data
        }

        void ChangeFromGameOver()
        {
            if (sceneName != hubWorldSceneName || sceneName != mainMenuSceneName || sceneName != SceneManager.GetActiveScene().name)
            {
                Debug.LogError("Cannot change from " + SceneManager.GetActiveScene().name + " to " + sceneName + ".");
                return;
            }
            else if (sceneName == SceneManager.GetActiveScene().name)
            {
                RestartCurrent();
                return;
            }
            SceneManager.LoadScene(sceneName);
        }

        void ChangeFromHub()
        {
            if (SceneManager.GetActiveScene().name != hubWorldSceneName)
            {
                Debug.LogError("Cannot change to " + sceneName + " from a non-hub scene.\n" +
                    "Make sure the hubWorldSceneName is intitialized properly.");
                return;
            }
            else if (sceneName == levelZeroSceneName)
            {
                Debug.LogError("Cannot change from hub world to Level 0. " +
                    "Also make sure Level Zero Scene Name is set properly.");
                return;
            }
            // save player state first, then load the scene
            SceneManager.LoadScene(sceneName);
        }

        void ChangeFromLevel()
        {
            if (sceneName != hubWorldSceneName)
            {
                Debug.LogError("Cannot change to " + sceneName +
                               " from a from a level.\n" +
                               "Make sure the call to ChangeFromLevel() is" +
                               "getting the Hub World's scene name as its parameter.");
                return;
            }
            // Before scene change, save player stats/equipment except for HP
            SceneManager.LoadScene(sceneName);
        }

        void RestartCurrent()
        {
            // initialize the player to last saved state first
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        void ChangeFromPause() 
        {
            if (sceneName != hubWorldSceneName || sceneName != mainMenuSceneName)
            {
                Debug.LogError("Cannot change from " + SceneManager.GetActiveScene().name + " to " + sceneName + ".");
                return;
            }
            SceneManager.LoadScene(sceneName);
        }
    }
}
