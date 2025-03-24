using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu_UI : MonoBehaviour
{
    [SerializeField] private VolumeController_UI[] volumeController;

    private void Start()
    {
        for (int i = 0; i < volumeController.Length; i++)
        {
            volumeController[i].GetComponent<VolumeController_UI>().SetupVolume();
        }
    }

    public void SwitchMenuTo(GameObject UIMenu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        AudioManager.instance.PlaySFX(4);
        UIMenu.SetActive(true);
    }
    public void ResetGame()
    {
        for (int i = 2; i < SceneManager.sceneCountInBuildSettings + 5; i++)
        {
            PlayerPrefs.SetInt("Level" + i + "Unlocked", 0);
        }
        PlayerPrefs.SetInt("TotalFruitsCollected", 200);

        for (int i = 0; i < 4; i++)
        {
            PlayerPrefs.SetInt("SkinPurchased" + i, 0);
        }
        PlayerPrefs.SetInt("EquipedSkin", 0);
    }
}
