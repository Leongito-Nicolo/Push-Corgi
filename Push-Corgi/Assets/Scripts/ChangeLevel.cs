using UnityEngine;
using UnityEngine.UI;

public class ChangeLevel : MonoBehaviour
{
    public string levelName;
    private Button button;
    private GameManager _gameManager;

    public GameObject levelsPanel;

    //public GameObject nextLevelButton;
    //public GameObject previusLevelButton;

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
            //nextLevelButton.SetActive(true);
            //previusLevelButton.SetActive(true);
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

        
        if (LevelLoader.Instance == null)
        {
            Debug.LogError("Errore ChangeLevel: GameManager.LevelLoader è NULL. Assegnalo nell'Inspector!");
            return;
        }

        LevelLoader.Instance.LoadLevelByName(levelName);
    }

    public void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(OnLevelSelected);
        }
    }

}
