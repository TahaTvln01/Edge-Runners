using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGame_UI : MonoBehaviour
{
    private bool isGamePaused;
    private bool isLevelFinished;
    private int totalLevels;

    [Header("In Game Info")]
    [SerializeField] private TextMeshProUGUI currentFruitAmount;

    [Header("End Level Info")]
    [SerializeField] private TextMeshProUGUI totalFruitAmount;

    [Header("Game Objects")]
    [SerializeField] private GameObject inGame_UI;
    [SerializeField] private GameObject pause_UI;
    [SerializeField] private GameObject endLvl_UI;

    [Header("Health Info")]
    [SerializeField] private Animator Heart1;
    [SerializeField] private Animator Heart2;

    private void Awake()
    {
        PlayerManager.instance.inGame_UI = this;   
    }
    private void Start()
    {
        GameManager.instance.levelNumber = SceneManager.GetActiveScene().buildIndex;
        AudioManager.instance.PlayBGM(AudioManager.instance.scene_bgm[GameManager.instance.levelNumber]);
        Time.timeScale = 1;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        inGame_UI.SetActive(true);
        isLevelFinished = false;
        totalLevels = SceneManager.sceneCountInBuildSettings;
    }
    public void SetupHp_UI()
    {
        if (PlayerManager.instance.currentPlayer != null)
        {
            if (PlayerManager.instance.currentPlayer.GetComponent<Player>().Hp == 2)
            {
                Heart1.SetBool("Active", true);
                Heart2.SetBool("Active", true); 
                return;
            }

            if (PlayerManager.instance.currentPlayer.GetComponent<Player>().Hp == 1)
            {
                Heart1.SetBool("Active", true);
                Heart2.SetBool("Active", false);
                return;
            }

            if (PlayerManager.instance.currentPlayer.GetComponent<Player>().Hp <= 0)
            {
                Heart1.SetBool("Active", false);
                Heart2.SetBool("Active", false);
                return;
            }
        }
        else
        {
            Heart1.SetBool("Active", false);
            Heart2.SetBool("Active", false);
            return;
        }
    }

    private void Update()
    {
        SetupHp_UI();
        currentFruitAmount.text = PlayerManager.instance.fruits.ToString();

        if (Input.GetKeyDown(KeyCode.Escape) && !isLevelFinished)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (!isGamePaused)
        {
            PlayerManager.instance.isGamePaused = true;
            isGamePaused = true;
            Time.timeScale = 0;
            SwitchMenuTo(pause_UI);
        }
        else
        {
            PlayerManager.instance.isGamePaused = false;
            if(PlayerManager.instance.currentPlayer != null)
            {
                PlayerManager.instance.currentPlayer.GetComponent<Player>().ButtonXInput = 0;
                PlayerManager.instance.currentPlayer.GetComponent<Player>().ButtonYInput = 0;
            }
            isGamePaused = false;
            Time.timeScale = 1f;
            SwitchMenuTo(inGame_UI);
        }
    }
    public void SwitchMenuTo(GameObject UIMenu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        UIMenu.SetActive(true);
        AudioManager.instance.PlaySFX(4);
    }
    public void OnLevelFinished()
    {
        isLevelFinished = true;
        totalFruitAmount.text = PlayerManager.instance.fruits.ToString() + "/" + GameManager.instance.fruitAmount;
        SwitchMenuTo(endLvl_UI);
    }
    public void LoadMainMenu()
    {
        AudioManager.instance.PlaySFX(4);
        Time.timeScale = 1;
        ResetPlayerStats();
        SceneManager.LoadScene("MainMenu");
        AudioManager.instance.PlayBGM(5);
    }
    public void LoadNextLevel()
    {
        AudioManager.instance.PlaySFX(4);
        if (totalLevels > SceneManager.GetActiveScene().buildIndex + 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            LoadMainMenu();
    }
    public void ReloadCurrentLevel()
    {
        AudioManager.instance.PlaySFX(4);
        ResetPlayerStats();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private static void ResetPlayerStats()
    {
        PlayerManager.instance.fruits = 0;
    }
}
