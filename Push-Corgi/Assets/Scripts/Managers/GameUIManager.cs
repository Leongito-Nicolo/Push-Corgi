using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance { get; private set; }
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private TMP_Text _movesText;
    [SerializeField] private TMP_Text _gameMovesText;

    public Button button;


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

    public void HasWon()
    {
        _winPanel.SetActive(true);
        _movesText.text = "Moves: " + GameManager.Instance.movesCounter;
    }

    public void UpdateMoves()
    {
        _gameMovesText.text = "Moves: " + GameManager.Instance.movesCounter;
    }

    public void Restart()
    {
        if (LevelLoader.Instance == null)
        {
            Debug.LogError("Restart fallito: LevelLoader.Instance è NULL.");
            return;
        }

        string levelToRestart = LevelLoader.Instance.CurrentLevelName;

        if (!string.IsNullOrEmpty(levelToRestart))
        {
            Debug.Log($"Riavvio del livello: {levelToRestart}");
            LevelLoader.Instance.LoadLevelByName(levelToRestart);
        }
        else
        {
            Debug.LogWarning("Nessun livello è stato caricato in precedenza. Impossibile riavviare.");
        }

    }
    
    void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(Restart);
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Undo()
    {
        GameManager.Instance.UndoMove();
    }


}
