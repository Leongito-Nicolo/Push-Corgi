using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuUI : MonoBehaviour
{
    public GameObject levelBottonPrefab;

    public RectTransform currentParent;

    public GameObject levelsPanelToClose;

    void Awake()
    {
        LevelLoader.OnLevelsReady += OnLevelsReadyToGenerate;
    }

    public void OnLevelsReadyToGenerate()
    {
        if (GameManager.Instance == null || LevelLoader.Instance == null)
        {
            Debug.LogError("GameManager o LevelLoader non inizializzati.");
            return;
        }

        LevelData[] levels = LevelLoader.Instance.AllLevels;

        if (levels != null && levels.Length > 0)
        {
            GenerateLevelButtons(levels);
        }
        else
        {
            Debug.LogError("Nessun dato sui livelli disponibile per la generazione dei pulsanti, nonostante l'evento di caricamento.");
        }
    }

    void OnDestroy()
    {
        LevelLoader.OnLevelsReady -= OnLevelsReadyToGenerate;
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
