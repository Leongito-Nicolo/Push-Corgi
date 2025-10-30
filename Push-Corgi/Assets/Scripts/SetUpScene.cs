using UnityEngine;

public class SetUpScene : MonoBehaviour
{
    public GameObject levelPannel;
    
    void Start()
    {
        if (GameSetUpManager.shouldOpenLevelPanelOnLoad)
        {
            levelPannel.SetActive(true);
        }
        else
        {
            levelPannel.SetActive(false);
        }
        
    }
}
