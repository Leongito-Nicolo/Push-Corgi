using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinUIManager : MonoBehaviour
{
    private int currentSkin = 0;
    [SerializeField] private GameObject skinPanel;
    [SerializeField] private List<Sprite> skins;
    [SerializeField] private TMP_Text skinCounter;

    private Image skinImage;

    void Awake()
    {
        skinImage = skinPanel.GetComponent<Image>();
        skinCounter.text = $"{currentSkin + 1}/{skins.Count}";
    }

    public void SelectSkin()
    {
        PlayerPrefs.SetInt("skinSelected", currentSkin);
    }

    public void PreviousOrNextSkin(bool isNext = true)
    {
        if (isNext)
        {
            currentSkin++;
        }
        else
        {
            currentSkin--;
        }

        currentSkin = (skins.Count + currentSkin) % skins.Count;
        skinImage.sprite = skins[currentSkin];
        skinCounter.text = $"{currentSkin + 1}/{skins.Count}";
        Debug.Log(currentSkin);
    }
}
