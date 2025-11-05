using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuUI : MonoBehaviour
{
    public GameObject levelBottonPrefab;

    public RectTransform currentParent;

    public GameObject levelsPanelToClose;
    
    public TMP_Text minMovesText;

    void Awake()
    {
        LevelLoader.OnLevelsReady += OnLevelsReadyToGenerate;
    }

    void Start()
    {
        LevelLoader.OnLevelDataLoaded += MinMoveOnDisplay;

        if (GameManager.Instance == null || LevelLoader.Instance == null)
        {
            Debug.LogError("GameManager o LevelLoader non inizializzati.");
            return;
        }
    }

    public void OnLevelsReadyToGenerate()
    {

        
        LevelLoader.OnLevelsReady -= OnLevelsReadyToGenerate;

        LevelData[] levels = LevelLoader.Instance.AllLevels;

        if (levels != null && levels.Length > 0)
        {
            GenerateLevelButtons(levels);
        }
        else
        {
            Debug.LogError("Nessun dato sui livelli disponibile per la generazione dei pulsanti, nonostante l'evento di caricamento.");
        }
        LevelLoader.OnLevelsReady -= OnLevelsReadyToGenerate;
    }

    void OnDestroy()
    {
        LevelLoader.OnLevelsReady -= OnLevelsReadyToGenerate;
    }

    public void MinMoveOnDisplay(LevelData data)
{
    // Questo metodo è chiamato automaticamente dall'evento OnLevelDataLoaded, 
    // e riceve l'oggetto LevelData come parametro 'data'.

    if (minMovesText != null)
    {
        // 1. Accede al campo MinMoveCounter (che è una stringa nel tuo JSON)
        minMovesText.text = data.MinMoveCounter;

        // Se volessi aggiungere un'etichetta:
        // minMovesText.text = $"Min: {data.MinMoveCounter}";

        Debug.Log($"[MainMenuUI] Mosse minime aggiornate a: {data.MinMoveCounter}");
    }
    else
    {
        Debug.LogWarning("minMovesText non è assegnato nell'Inspector di MainMenuUI. Impossibile visualizzare le mosse minime.");
    }
}


    private void GenerateLevelButtons(LevelData[] levels)
    {
        foreach (LevelData level in levels)
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
                button.onClick.AddListener(levelChanger.OnLevelSelected);
            }
        }

    }
}
