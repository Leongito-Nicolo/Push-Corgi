using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("test");
    }

    public void LevelSelection()
    {
        SceneManager.LoadScene("LevelSelection");
    }
}
