using UnityEngine;
using System;

public class LevelLoader : MonoBehaviour
{
    private LevelGlobalContainer _levelGlobalContainer;

    public int GlobalLine { get; private set; }
    public int GlobalCol { get; private set; }
    private LevelData _data;

    public LevelData[] AllLevels => _levelGlobalContainer?.Levels;

    public static LevelLoader Instance { get; private set; }

    public string CurrentLevelName;

    public static event Action OnLevelsReady;

    public GameObject threeStarsImage;

    public GameObject twoStarsImage;

    public GameObject uneStarsImage;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadAllLevel();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void LoadAllLevel()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("JSONpROVA");

        if (jsonFile == null)
        {
            Debug.LogError("File JSON non trovato");
            return;
        }

        try
        {
            _levelGlobalContainer = JsonUtility.FromJson<LevelGlobalContainer>(jsonFile.text);

            if (_levelGlobalContainer != null)
            {
                OnLevelsReady?.Invoke();
                Debug.Log($"Dati caricati {_levelGlobalContainer.Levels.Length}");
                GlobalLine = _levelGlobalContainer.line;
                GlobalCol = _levelGlobalContainer.col;
                Debug.Log($"Dati caricati {_levelGlobalContainer.Levels.Length}");
            }

            else
            {
                Debug.LogError("Errore deserializazione file JSON");
            }
        }

        catch (Exception e)
        {
            Debug.LogError($"ERRORE durante la lettura del file JSON: {e.Message}");
        }

    }

    public int GetGlobalLevel()
    {
        return (_levelGlobalContainer != null && _levelGlobalContainer.Levels != null) ? _levelGlobalContainer.Levels.Length : 0;
    }

    public LevelData GetPushLevelData(int levelNumber)
    {
        int index = levelNumber - 1;

        if (_levelGlobalContainer != null && _levelGlobalContainer.Levels != null && index >= 0 && index < _levelGlobalContainer.Levels.Length)
        {
            return _levelGlobalContainer.Levels[index];
        }

        return null;
    }

    public void LoadLevelByName(string name)
    {
        LevelData selectedLevel = Array.Find(
            _levelGlobalContainer.Levels,
            level => level.levelName.Equals(name, StringComparison.OrdinalIgnoreCase)
        );

        if (selectedLevel != null)
        {
            CurrentLevelName = name;

            int line = _levelGlobalContainer.line;
            int col = _levelGlobalContainer.col;

            LevelGenerator.Instance.LevelGenerate(selectedLevel, line, col);
        }
        else
        {
            Debug.LogError($"Livello '{name}' non trovato nei dati JSON.");
        }
    }

    public void LoadNextLevel()
    {
        if (_levelGlobalContainer == null || _levelGlobalContainer.Levels == null || string.IsNullOrEmpty(CurrentLevelName))
        {
            Debug.LogError("Impossibile caricare il livello successivo: I dati non sono stati caricati o il livello corrente non è definito.");
            return;
        }

        LevelData[] allLevels = _levelGlobalContainer.Levels;

        int currentIndex = Array.FindIndex(
            allLevels,
            level => level.levelName.Equals(CurrentLevelName, StringComparison.OrdinalIgnoreCase)
        );

        if (currentIndex == -1)
        {
            Debug.LogError($"Livello corrente '{CurrentLevelName}' non trovato nella lista dei livelli.");
            return;
        }

        int nextIndex = currentIndex + 1;

        if (nextIndex < allLevels.Length)
        {
            string nextLevelName = allLevels[nextIndex].levelName;

            Debug.Log($"Caricamento del livello successivo: {nextLevelName}");
            LoadLevelByName(nextLevelName);
        }
        else
        {
            Debug.Log("COMPLETATO: Sei all'ultimo livello. Non ci sono altri livelli da caricare.");
        }
    }

    public void LoadPreviousLevel()
    {
        if (_levelGlobalContainer == null || _levelGlobalContainer.Levels == null || string.IsNullOrEmpty(CurrentLevelName))
        {
            Debug.LogError("Impossibile caricare il livello precedente: I dati non sono stati caricati o il livello corrente non è definito.");
            return;
        }

        LevelData[] allLevels = _levelGlobalContainer.Levels;

        int currentIndex = Array.FindIndex(
            allLevels,
            level => level.levelName.Equals(CurrentLevelName, StringComparison.OrdinalIgnoreCase)
        );

        if (currentIndex == -1)
        {
            Debug.LogError($"Livello corrente '{CurrentLevelName}' non trovato nella lista dei livelli.");
            return;
        }

        int previousIndex = currentIndex - 1;

        if (previousIndex >= 0)
        {
            string previousLevelName = allLevels[previousIndex].levelName;

            Debug.Log($"Caricamento del livello precedente: {previousLevelName}");
            LoadLevelByName(previousLevelName);
        }
        else
        {
            Debug.Log("Sei già al primo livello. Impossibile tornare indietro.");
        }
    }

    public void CalculateStars(int oneStarLimit, int twoStarsLimit, int threeStarsLimit)
    {

        int playerMoves = GameManager.Instance.movesCounter;
        Debug.Log($"Mosse del giocatore: {playerMoves}");
        Debug.Log($"Calcolo Stelle: Mosse Giocatore={playerMoves} | Limiti (3/2/1) = {threeStarsLimit}/{twoStarsLimit}/{oneStarLimit}");

        if (playerMoves <= threeStarsLimit)
        {
            threeStarsImage.SetActive(true);
            Debug.Log("hai ottenuto 3 stelle");
        }

        else if (playerMoves <= twoStarsLimit)
        {
            twoStarsImage.SetActive(true);
            Debug.Log("hai ottenuto 2 stelle");
        }

        else if (playerMoves >= oneStarLimit)
        {
            uneStarsImage.SetActive(true);
            Debug.Log("hai ottenuto 1 stella");
        }
    }

    public LevelData GetCurrentLevelData()
    {
        if (_levelGlobalContainer == null || _levelGlobalContainer.Levels == null || string.IsNullOrEmpty(CurrentLevelName))
        {
            Debug.LogError("Impossibile trovare i dati del livello corrente.");
            return null;
        }

        return Array.Find(
            _levelGlobalContainer.Levels,
            level => level.levelName.Equals(CurrentLevelName, StringComparison.OrdinalIgnoreCase)
        );
    }
    public void StartFirstLevel()
    {
        if (_levelGlobalContainer != null && _levelGlobalContainer.Levels.Length > 0)
        {
            string firstLevelName = _levelGlobalContainer.Levels[0].levelName;


            LoadLevelByName(firstLevelName);

            Debug.Log($"Inizio del gioco: caricato il primo livello ({firstLevelName})");
        }
        else
        {
            Debug.LogError("Impossibile avviare il primo livello: nessun dato disponibile.");
        }
    }
}




