using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Move
{
    public Draggable currentDraggable;
    public Vector3 oldPosition;

    public Move(Draggable currentDraggable, Vector3 oldPosition)
    {
        this.currentDraggable = currentDraggable;
        this.oldPosition = oldPosition;
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool hasWon = false;
    public bool isPaused = false;
    public int movesCounter = 0;

    private int _currentLevel = 1;
    private int _totalLevel;

    public Stack<Move> moves = new();



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

    public void UndoMove()
    {
        if (moves.Count <= 0) return;

        Move moveToRevert = moves.Peek();

        if (moveToRevert.currentDraggable.isSnapping) return;

        movesCounter--;
        SoundManager.Instance.PlayGameSound(SoundManager.Instance.rewindSound);
        StartCoroutine(moveToRevert.currentDraggable.SnapRoutine(moveToRevert.oldPosition));

        moves.Pop();

    }

}
