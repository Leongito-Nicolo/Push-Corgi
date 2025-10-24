using UnityEngine;
using System.Collections.Generic;
using System;

public class LevelLoader : MonoBehaviour
{
    private LevelGlobalContainer _levelGlobalContainer;

    public int GlobalLine { get; private set; }
    public int GlobalCol { get; private set; }
    private LevelData _data;

    public LevelData[] AllLevels => _levelGlobalContainer?.Levels;

    public static LevelLoader Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }



    public void LoadAllLevel()
    {
        //TextAsset jsonFile = Resources.Load<TextAsset>("JSONChessboardLevel");
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
            // Trova le dimensioni globali e chiama la generazione
            int line = _levelGlobalContainer.line;
            int col = _levelGlobalContainer.col;

            // Avvia la generazione del livello
            LevelGenerator.Instance.LevelGenerate(selectedLevel, line, col);
        }
        else
        {
            Debug.LogError($"Livello '{name}' non trovato nei dati JSON.");
        }
    }

}
