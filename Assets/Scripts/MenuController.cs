using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance { get; private set; }

    public GameObject jackpot;

    public Button playBtn;

    public Button spinBtn;
    public Button pauseBtn;

    public Button resumeBtn;
    public Button replayBtn;
    public Button quitBtn;

    public Button gameEndReplayBtn;

    public GameObject mainMenu;
    public GameObject gameMenu;
    public GameObject pauseMenu;
    public GameObject gameEndMenu;

    public TMP_Text roundText;
    public TMP_Text scoreText;

    public TMP_Text gameEnd_gameScoreText;
    public TMP_Text gameEnd_bestScoreText;
    public TMP_Text gameEnd_wordsGuessedText;

    public TMP_Text mainMenu_bestScoreText;
    public TMP_Text mainMenu_bestStatText;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playBtn.onClick.AddListener(OnPlayBtnClicked);

        spinBtn.interactable = false;
        spinBtn.onClick.AddListener(OnSpinBtnClicked);
        pauseBtn.onClick.AddListener(OnPauseBtnClicked);


        resumeBtn.onClick.AddListener(OnResumeBtnClicked);
        replayBtn.onClick.AddListener(OnReplayBtnClicked);
        quitBtn.onClick.AddListener(OnQuitBtnClicked);

        gameEndReplayBtn.onClick.AddListener(OnReplayBtnClicked);
    }

    public void SetRoundText(int _round)
    {
        roundText.text = $"{_round} of {GameController.Instance.totalRounds} ROUND";
    }

    public void SetScoreText(int _score)
    {
        scoreText.text = _score.ToString();
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        //mainMenu_bestScoreText.text = 
        mainMenu_bestStatText.text = $"{StatsController.Instance.correctlyGuessedWords} Correct";
    }

    public void ShowGameEndPopup()
    {
        gameEnd_gameScoreText.text = StatsController.Instance.gameScore.ToString();
        gameEnd_bestScoreText.text = StatsController.Instance.bestScore.ToString();
        gameEnd_wordsGuessedText.text = StatsController.Instance.correctlyGuessedWords.ToString();
        gameEndMenu.SetActive(true);
    }

    //Button Listeners

    //Main Menu
    public void OnPlayBtnClicked()
    {
        GameController.Instance.OnPlayBtnClicked();
        spinBtn.interactable = true;
        gameMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    //Game Menu
    public void OnSpinBtnClicked()
    {
        GameController.Instance.OnSpinBtnClicked();
        spinBtn.interactable = false;
    }

    public void OnPauseBtnClicked()
    {
        pauseMenu.SetActive(true);
        gameMenu.SetActive(false);
        jackpot.SetActive(false);
    }

    //Pause Menu
    public void OnResumeBtnClicked()
    {
        pauseMenu.SetActive(false);
        gameMenu.SetActive(true);
        jackpot.SetActive(true);
    }

    public void OnReplayBtnClicked()
    {
        pauseMenu.SetActive(false);
        gameMenu.SetActive(true);
        jackpot.SetActive(true);
        GameController.Instance.OnPlayBtnClicked();
        gameEndMenu.SetActive(false);
    }

    public void OnQuitBtnClicked()
    {
        ShowMainMenu();
        pauseMenu.SetActive(false);
        jackpot.SetActive(true);
    }
}
