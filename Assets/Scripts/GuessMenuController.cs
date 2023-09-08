using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.Linq;

public class GuessMenuController : MonoBehaviour
{
    public UnityEngine.UI.Image[] resultedCatergories;
    public TMP_Text[] text;
    public UnityEngine.UI.Image[] tickIcon;
    public UnityEngine.UI.Image[] wrongIcon;

    public UnityEngine.UI.Image currentCategory;
    public TMP_InputField input;
    public TMP_Text input_text;

    //public Category[] resultedCatergoriesName;

    private int guessCount;

    private string[] destinations = { "atlanta", "australia", "denmark", "dubai", "france", "finland"  };
    private string[] objects = { "apple", "airplane", "doll", "door", "fan", "fork" };
    private string[] animals = { "ape", "alligator", "dog", "duck",  "frog", "ferret" };

    // Start is called before the first frame update
    void Start()
    {
        input.onSubmit.AddListener(OnSubmit);
    }

    public void SetItems()
    {
        int index;
        for (int i = 0, k = 1; i < resultedCatergories.Length; i++, k++)
        {
            index = GameController.Instance.stoppedSlotsCategories[k];
            resultedCatergories[i].sprite = GameController.Instance.rowItemsCategories[index].sprite;
            resultedCatergories[i].SetNativeSize();
            text[i].text = GameController.Instance.rowItemsCategories[index].category.ToString();
            //resultedCatergoriesName[i] = 
            tickIcon[i].enabled = false;
            wrongIcon[i].enabled = false;

        }
        guessCount = 0;
        currentCategory.sprite = resultedCatergories[guessCount].sprite;
        currentCategory.SetNativeSize();
    }

    private void GuessValidator(string guess)
    {
        bool isValid = false;
        if (GameController.Instance.letter[0] != guess[0])
        {
            isValid = false;
        }
        else
        {
            guess = guess.ToLower();

            if (text[guessCount].text == "Destination")
                isValid = destinations.Contains(guess);

            else if (text[guessCount].text == "Animal")
                isValid = animals.Contains(guess);

            else if (text[guessCount].text == "Object")
                isValid = objects.Contains(guess);
        }


        if (isValid)
        {
            tickIcon[guessCount].enabled = true;
            wrongIcon[guessCount].enabled = false;
            StatsController.Instance.AddScore();
        }
        else
        {
            tickIcon[guessCount].enabled = false;
            wrongIcon[guessCount].enabled = true;
        }
    }

    private void ShowNextItem()
    {
        guessCount += 1;
        if (guessCount == resultedCatergories.Length)
        {
            GameController.Instance.OnNextRound();
            return;
        }
        currentCategory.sprite = resultedCatergories[guessCount].sprite;
        currentCategory.SetNativeSize();
    }

    private void OnSubmit(string text)
    {
        UnityEngine.Debug.Log(text);
        GuessValidator(text);
        ShowNextItem();
    }
}
