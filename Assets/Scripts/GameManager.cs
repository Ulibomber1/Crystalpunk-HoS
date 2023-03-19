using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.Collections;
public enum GameState { MAIN_MENU, 
                        HUB_WORLD, 
                        LEVEL_0, 
                        LEVEL_FACTORY,
                        LEVEL_LAVA, 
                        LEVEL_AIR, 
                        PAUSED, 
                        GAME_OVER}

public class GameManager : MonoBehaviour
{
    [SerializeField] private string hubWorldSceneName;
    [SerializeField] private string levelZeroSceneName;
    [SerializeField] private string mainMenuSceneName;

    public static int gears = 0;
    public static int ammo = 0;
    public static int health = 0;
    public static int lives = 0;
    public static bool doubleJump = false;

    // New Singleton Pattern
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }
    // End of new Singleton Pattern

    public GameState gameState { get; private set; }
    public void SetGameState(GameState state)
    {
        gameState = state;
    }

    public void OnApplicationQuit()
    {
        GameManager.Instance = null;
    }

    public void Start()
    {
        SetGameState(GameState.MAIN_MENU);
    }

    public void Update()
    {
        // Game Logic may occur here
    }

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
            if (SceneManager.GetActiveScene().name != mainMenuSceneName)
            {
                Debug.LogError("Cannot change based on a save file from anywhere except the main menu.");
                return;
            }
            // Need to load save, then intialize the scene using that save data
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            // Temporary:
            SetGameState(GameState.HUB_WORLD);
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
            // temporary:
            SetGameState(GameState.MAIN_MENU);
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
