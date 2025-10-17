using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public LevelLoader levelLoader;
    public LevelGenerator levelGenerator;

    private int _currentLevel = 1;
    private int _totalLevel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if (levelLoader == null)
        {
            Debug.LogError("il level loader non è assegnato correttamente nel GameManager!");
            return;
        }

        levelLoader.LoadAllLevel();
        _totalLevel = levelLoader.GetGlobalLevel();

        LoadLevel(_currentLevel);
    }

    public void LoadLevel(int levelNumber)
    {
        if (levelNumber < 1 || levelNumber > _totalLevel)
        {
            Debug.LogError($"livello {levelNumber} non trovato");
            return;
        }

        LevelData levelData = levelLoader.GetPushLevelData(levelNumber);

        if (levelData != null && levelGenerator != null)
        {
            Debug.Log($"Caricamento livello {levelNumber} : {levelData.levelName}");

            levelGenerator.LevelGenerate(
                levelData,
                levelLoader.GlobalLine,
                levelLoader.GlobalCol
            );
        }

        _currentLevel = levelNumber;
    }
    
}
