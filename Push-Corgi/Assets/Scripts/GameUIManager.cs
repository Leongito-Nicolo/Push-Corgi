using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance { get; private set; }
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private TMP_Text _movesText;
    [SerializeField] private TMP_Text _gameMovesText;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
