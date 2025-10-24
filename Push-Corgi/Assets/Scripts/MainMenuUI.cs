using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuUI : MonoBehaviour
{
    public GameObject levelBottonPrefab;

    public RectTransform currentParent;

    public GameObject levelsPanelToClose;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.Instance == null || GameManager.Instance.levelLoader == null)
        {
            Debug.LogError("GameManager o LevelLoader non inizializzati. Controlla l'ordine di esecuzione.");
            return;
        }
        LevelData[] levels = GameManager.Instance.levelLoader.AllLevels;

        if (levels != null && levels.Length > 0)
        {
            GenerateLevelButtons(levels);
        }

        else
        {
            Debug.LogError("Nessun dato sui livelli disponibile per la generazione dei pulsanti.");
        }
    }

    // Update is called once per frame
    private void GenerateLevelButtons(LevelData[] levels)
    {
        foreach(LevelData level in levels)
        {
            GameObject buttonGO = Instantiate(levelBottonPrefab, currentParent);

            TextMeshProUGUI buttonText = buttonGO.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = level.levelName;
            }
            else
            {
                Debug.LogWarning($"Componente testo non trovato sul prefab del pulsante per il livello: {level.levelName}");
            }

            ChangeLevel levelChanger = buttonGO.GetComponent<ChangeLevel>();

            if (levelChanger == null)
            {
                levelChanger = buttonGO.AddComponent<ChangeLevel>();
            }

            levelChanger.levelName = level.levelName;
            levelChanger.levelsPanel = levelsPanelToClose;
            
            Button button = buttonGO.GetComponent<Button>();
            if (button != null)
            {
                // Collega la funzione OnLevelSelected con il listener standard
                button.onClick.AddListener(levelChanger.OnLevelSelected); 
            }
        }
        
    }
}
