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
    [SerializeField] private TMP_Text _levelName;

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
        _movesText.text = $"{GameManager.Instance.movesCounter}";
    }

    public void UpdateMoves()
    {
        _gameMovesText.text = $"{GameManager.Instance.movesCounter}";
    }

    public void OnNextLevelButtonClicked()
    {
        GameManager.Instance.hasWon = false;
        _winPanel.SetActive(false);
        if (LevelLoader.Instance == null)
        {
            Debug.LogError("Il LevelLoader non è attivo. Impossibile passare al livello successivo.");
            return;
        }
        LevelLoader.Instance.LoadNextLevel();
        GameManager.Instance.movesCounter = 0;
        LevelLoader.Instance.threeStarsImage.SetActive(false);
        LevelLoader.Instance.twoStarsImage.SetActive(false);
        LevelLoader.Instance.uneStarsImage.SetActive(false);
    }

    public void OnPreviousLevelButtonClicked()
    {
        if (LevelLoader.Instance == null)
        {
            Debug.LogError("Il LevelLoader non è attivo. Impossibile passare al livello precedente.");
            return;
        }
        LevelLoader.Instance.LoadPreviousLevel();
        GameManager.Instance.movesCounter = 0;
        LevelLoader.Instance.threeStarsImage.SetActive(false);
        LevelLoader.Instance.twoStarsImage.SetActive(false);
        LevelLoader.Instance.uneStarsImage.SetActive(false);
    }

    public void CloseWinPannel()
    {
        _winPanel.SetActive(false);
    }

    public void Restart()
    {
        GameManager.Instance.hasWon = false;
        //_winPanel.SetActive(false);
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
        GameManager.Instance.movesCounter = 0;
        LevelLoader.Instance.threeStarsImage.SetActive(false);
        LevelLoader.Instance.twoStarsImage.SetActive(false);
        LevelLoader.Instance.uneStarsImage.SetActive(false);
        //_winPanel.SetActive(false);
    }

    public void LoadLevelByLoadedName()
    {
        _levelName.text = $"{LevelLoader.Instance.CurrentLevelName}";
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

    public void PauseUnpause()
    {
        GameManager.Instance.isPaused = !GameManager.Instance.isPaused;
    }

}
