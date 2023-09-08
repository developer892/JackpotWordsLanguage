using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public JackpotController jackpotController;
    public GuessMenuController guessMenuController;
    public static GameController Instance { get; private set; }


    public Dictionary<int, int> stoppedSlotsCategories = new Dictionary<int, int>();
    public int letterIndex;
    public string letter;

    public RowItemCategory[] rowItemsCategories;

    public Button playBtn;
    public Button spinBtn;

    public GameObject mainMenu;
    public GameObject gameMenu;

    private bool rotationDone;

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
        spinBtn.interactable = false;
        spinBtn.onClick.AddListener(OnSpinBtnClicked);
        playBtn.onClick.AddListener(OnPlayBtnClicked);
    }

    // Update is called once per frame
    void Update()
    {
        if (jackpotController.rows[0].rowStopped && jackpotController.rows[1].rowStopped && jackpotController.rows[2].rowStopped
            && jackpotController.rows[3].rowStopped && !rotationDone)
        {
            rotationDone = true;
            letterIndex = jackpotController.rows[0].stoppedSlot;
            letter = jackpotController.rows[0].rowItemsCategories[letterIndex].letter;
            stoppedSlotsCategories.Clear();
            for (int i = 1; i < jackpotController.rows.Length; i++)
            {
                stoppedSlotsCategories.Add(i, jackpotController.rows[i].stoppedSlot);
            }
            guessMenuController.SetItems();
        } 
    }

    public void OnSpinBtnClicked()
    {
        rotationDone = false;
        jackpotController.StartSpinning();
    }

    public void OnPlayBtnClicked()
    {
        stoppedSlotsCategories.Clear();
        spinBtn.interactable = true;
        gameMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
}
