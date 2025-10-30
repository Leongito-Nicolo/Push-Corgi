using UnityEngine;
using UnityEngine.UI;

public class SettingsUIManager : MonoBehaviour
{
    public Sprite newButtonImage;
    public Sprite oldButtonImage;
    public Image buttonImage;
    public bool isChanged = false;

    public void ChangeButtonImage()
    {
        buttonImage.sprite = isChanged ? oldButtonImage : newButtonImage;
        isChanged = !isChanged;
    }

    public void Music()
    {
        SoundManager.Instance.backgroundSource.mute = isChanged ? false : true;
    }

    public void Sound()
    {
        SoundManager.Instance.gameSource.mute = isChanged ? false : true;
    }

    public void Vibration()
    {

    }
}
