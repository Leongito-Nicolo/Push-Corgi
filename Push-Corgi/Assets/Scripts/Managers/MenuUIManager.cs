using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameSetUpManager
{
    public static bool shouldOpenLevelPanelOnLoad = false;
}

public class MenuUIManager : MonoBehaviour
{
    public ChangeLevel levels;
    public LevelLoader levelLoader;

    public void StartGame()
    {
        GameSetUpManager.shouldOpenLevelPanelOnLoad = false;
        
        SceneManager.LoadScene("test 1");
    }

    public void LevelSelection()
    {
        GameSetUpManager.shouldOpenLevelPanelOnLoad = true;
        SceneManager.LoadScene("test 1");

    }
}
