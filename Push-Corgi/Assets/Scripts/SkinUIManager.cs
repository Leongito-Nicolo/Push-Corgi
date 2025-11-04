using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinUIManager : MonoBehaviour
{
    private int currentSkin = 0;
    [SerializeField] private GameObject skinImage;
    [SerializeField] private List<Sprite> skins;
    [SerializeField] private TMP_Text skinCounter;

    void Awake()
    {
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
        skinImage.GetComponent<Image>().sprite = skins[currentSkin];
        skinCounter.text = $"{currentSkin + 1}/{skins.Count}";
        Debug.Log(currentSkin);
    }
}
