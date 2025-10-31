using UnityEngine;

public class SetUpScene : MonoBehaviour
{
    public GameObject levelPannel;
    
    void Start()
    {
        if (GameSetUpManager.shouldOpenLevelPanelOnLoad)
        {
            levelPannel.SetActive(true);
        }
        else
        {
            // ⭐ AVVIA IL GIOCO SUBITO (Se StartGame è stato premuto)
            levelPannel.SetActive(false);
            if (LevelLoader.Instance != null)
            {
                LevelLoader.Instance.StartFirstLevel();
            }
        }
        
    }
}
