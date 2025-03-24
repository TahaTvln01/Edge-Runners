using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject levelButton;
    [SerializeField] private Transform levelButtonParent;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private bool[] levelOpen;

    public void SetupLevels()
    {
        for (int i = levelButtonParent.childCount - 1; i >= 0; i--)
            Destroy(levelButtonParent.GetChild(i).gameObject);
        for (int i = 0; i < levelOpen.Length; i++)
            levelOpen[i] = false;
        PlayerPrefs.SetInt("Level" + 1 + "Unlocked", 1);
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;
            if (unlocked)
            {
                levelOpen[i] = true;
            }
        }
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string sceneName = "Level " + i;

            GameObject newButton = Instantiate(levelButton, levelButtonParent);
            if (levelOpen[i])
            {
                newButton.GetComponentInChildren<TextMeshProUGUI>().text = sceneName;
                newButton.GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneName));
            }
            else
            {
                newButton.GetComponentInChildren<TextMeshProUGUI>().text = "Locked!";
            }
        }
        continueButton.SetActive(true);
        if (PlayerPrefs.GetInt("Level" + SceneManager.sceneCountInBuildSettings + "Unlocked") == 1)
        {
            continueButton.SetActive(false);
        }
    }

    public void LoadLevel(string sceneName) 
    {
        AudioManager.instance.PlaySFX(4);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadLastLevel()
    {
        AudioManager.instance.PlaySFX(4);
        for (int i = 1; i <= SceneManager.sceneCountInBuildSettings; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;

            if (!unlocked)
            {
                SceneManager.LoadScene("Level " + (i - 1));
                return;
            }
        }
    }
}
