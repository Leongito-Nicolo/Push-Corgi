using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<GameObject> skins;
    private float velocity = 9f;


    void Awake()
    {
        int skinIndex = PlayerPrefs.GetInt("skinSelected");
        skins[skinIndex].gameObject.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Win"))
        {
            Animator doorAnimator = other.GetComponent<Animator>();
            doorAnimator.SetTrigger("OPEN");
            Draggable drag = transform.GetComponent<Draggable>();
            StartCoroutine(drag.SnapRoutine(Vector3.right * velocity));
            GameManager.Instance.hasWon = true;
            SoundManager.Instance.PlayGameSound(SoundManager.Instance.winSound);

            Debug.Log("🎉 Sto per calcolare le stelle!");
            //logica del calcolo dello score richiamata alla vittoria
            LevelData currentData = LevelLoader.Instance.GetCurrentLevelData();

            Debug.Log($"Dati Caricati: 3 Stelle={currentData.threeStar} | 2 Stelle={currentData.twoStar} | 1 Stella={currentData.uneStar}");

            if (currentData != null)
            {
                LevelLoader.Instance.CalculateStars(
                    currentData.uneStar,
                    currentData.twoStar,
                    currentData.threeStar
                );
            }
        }
    }
}
