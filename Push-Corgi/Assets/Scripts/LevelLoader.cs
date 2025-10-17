using UnityEngine;
using System.Collections.Generic;
using System;

public class LevelLoader : MonoBehaviour
{
    private LevelGlobalContainer _levelGlobalContainer;

    public int GlobalLine { get; private set; }
    public int GlobalCol { get; private set; }

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

}
