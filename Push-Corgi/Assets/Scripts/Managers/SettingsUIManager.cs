using UnityEngine;
using UnityEngine.UI;

public class SettingsUIManager : MonoBehaviour
{
    public Sprite newButtonImage;
    public Sprite oldButtonImage;
    public Image buttonImage;
    public bool isChanged = false;
    private string settingKey;

    private void Start()
    {
        settingKey = gameObject.name;

        isChanged = PlayerPrefs.GetInt(settingKey, 0) == 1;

        buttonImage.sprite = isChanged ? newButtonImage : oldButtonImage;

        Music();
        Sound();
        Vibration();
    }

    public void ChangeButtonImage()
    {
        isChanged = !isChanged;

        buttonImage.sprite = isChanged ? newButtonImage : oldButtonImage;

        PlayerPrefs.SetInt(settingKey, isChanged ? 1 : 0);

        Music();
        Sound();
        Vibration();
    }

    public void Music()
    {
        if (settingKey == "Music")
            SoundManager.Instance.backgroundSource.mute = isChanged;
    }

    public void Sound()
    {
        if (settingKey == "Sound")
            SoundManager.Instance.gameSource.mute = isChanged;
    }

    public void Vibration()
    {
        if (settingKey == "Vibration")
        {

        }
    }
}
