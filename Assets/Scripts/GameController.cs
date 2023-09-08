using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController Instance { get; private set; }

    public JackpotController jackpotController;
    public GuessMenuController guessMenuController;

    public Dictionary<int, int> stoppedSlotsCategories = new Dictionary<int, int>();
    public int letterIndex;
    public string letter;

    public RowItemCategory[] rowItemsCategories;

    private bool rotationDone;

    public int currentRound = 1;
    public int totalRounds = 3;

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

    public void OnNextRound()
    {
        currentRound += 1;

        if (currentRound > totalRounds)
        {
            MenuController.Instance.ShowGameEndPopup();
            return;
        }
        stoppedSlotsCategories.Clear();
        MenuController.Instance.spinBtn.interactable = true;
        MenuController.Instance.SetRoundText(currentRound);
    }

    //Button Listeners

    //Main Menu
    public void OnPlayBtnClicked()
    {
        stoppedSlotsCategories.Clear();
        currentRound = 1;
        MenuController.Instance.spinBtn.interactable = true;
        MenuController.Instance.SetRoundText(currentRound);
        MenuController.Instance.SetScoreText(0);
    }

    //Game Menu
    public void OnSpinBtnClicked()
    {
        rotationDone = false;
        jackpotController.StartSpinning();
    }
}
