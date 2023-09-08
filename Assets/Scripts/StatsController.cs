using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    public static StatsController Instance { get; private set; }

    public int gameScore = 0;

    public int correctlyGuessedWords = 0;
    public int bestScore = 0;

    private const int SCORE_FOR_CORRECTLY_GUESSED_WORD = 10;

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

    public void AddScore()
    {
        gameScore += SCORE_FOR_CORRECTLY_GUESSED_WORD;
        bestScore += SCORE_FOR_CORRECTLY_GUESSED_WORD;
        MenuController.Instance.SetScoreText(gameScore);
        correctlyGuessedWords += 1;
    }
}
