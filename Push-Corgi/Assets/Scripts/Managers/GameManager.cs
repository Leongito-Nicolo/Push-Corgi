using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool hasWon = false;
    public int movesCounter = 0;

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
        if (LevelLoader.Instance == null)
        {
            Debug.LogError("il level loader non è assegnato correttamente nel GameManager!");
            return;
        }

        LevelLoader.Instance.LoadAllLevel();
        _totalLevel = LevelLoader.Instance.GetGlobalLevel();

        LoadLevel(_currentLevel);
    }

    void Update()
    {
        if (hasWon)
        {
            GameUIManager.Instance.HasWon();
        }

        GameUIManager.Instance.UpdateMoves();
    }

    public void LoadLevel(int levelNumber)
    {
        if (levelNumber < 1 || levelNumber > _totalLevel)
        {
            Debug.LogError($"livello {levelNumber} non trovato");
            return;
        }

        LevelData levelData = LevelLoader.Instance.GetPushLevelData(levelNumber);

        if (levelData != null && LevelGenerator.Instance != null)
        {
            Debug.Log($"Caricamento livello {levelNumber} : {levelData.levelName}");

            LevelGenerator.Instance.LevelGenerate(
                levelData,
                LevelLoader.Instance.GlobalLine,
                LevelLoader.Instance.GlobalCol
            );
        }

        _currentLevel = levelNumber;
    }

}
