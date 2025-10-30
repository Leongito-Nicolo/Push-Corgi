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
        if (LevelLoader.Instance != null)
        {
            // Imposta il nome del livello iniziale. 
            // Assicurati che il nome "Level1" corrisponda esattamente al levelName nel tuo JSON.
            LevelLoader.Instance.CurrentLevelName = "Level1"; 
        }
        else
        {
            Debug.LogError("LevelLoader.Instance non è disponibile. Controlla l'ordine di esecuzione o la presenza del GameObject.");
        }
        
        // Assumiamo che GameSetUpManager.shouldOpenLevelPanelOnLoad esista in un'altra classe statica/manager
        GameSetUpManager.shouldOpenLevelPanelOnLoad = false;
        
        SceneManager.LoadScene("test 1");
    }

    public void LevelSelection()
    {
        GameSetUpManager.shouldOpenLevelPanelOnLoad = true;
        SceneManager.LoadScene("test 1");

    }
}
