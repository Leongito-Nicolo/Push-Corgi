using UnityEngine;
using UnityEngine.UI;

public class ChangeLevel : MonoBehaviour
{
    public string levelName;
    private Button button;
    private GameManager _gameManager;

    public GameObject levelsPanel;

    void Awake()
    {
        _gameManager = GameManager.Instance;
        button = GetComponent<Button>();
        button.onClick.AddListener(OnLevelSelected);
    }

    public void OnLevelSelected()
    {

        if (string.IsNullOrEmpty(levelName))
        {
            Debug.Log($"Il nome del livello non è stato impostato sul pulsante '{gameObject.name}'.");
            return;
        }
        
        if (levelsPanel != null)
        {
            levelsPanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Il pannello dei livelli non è stato assegnato, procedo solo con il caricamento.");
        }

        if (GameManager.Instance == null)
        {
            Debug.LogError("Errore ChangeLevel: GameManager.Instance è NULL. Controlla l'inizializzazione del Singleton.");
            return; 
        }
    
        // Verifica 2: LevelLoader (L'oggetto che è NULL alla riga 25)
        if (GameManager.Instance.levelLoader == null) 
        {
            Debug.LogError("Errore ChangeLevel: GameManager.LevelLoader è NULL. Assegnalo nell'Inspector!");
            return;
        }

        GameManager.Instance.levelLoader.LoadLevelByName(levelName);
    }
    
    public void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(OnLevelSelected);
        }
    }
    
}
