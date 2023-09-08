using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Diagnostics;
using System.Security.Permissions;
using System.Linq;

public enum Category { Default, Object, Animal, Destination}

[Serializable]
public class RowItemValue
{
    public int index;
}

[Serializable]
public class RowItemCategory : RowItemValue
{
    public Sprite sprite;
    public Category category;
    public string letter;
}

[Serializable]
public class RowItemLetter : RowItemValue
{
    public TMP_Text letter;
}

public class Row : MonoBehaviour
{
    public RowItemCategory[] rowItemsCategories;
    public RowItem[] rowItems;

    public float[] yPos;

    public bool isLettered;
    public int slotIndex;

    //public Image[] rowItems;

    private int randomValue;
    private float timeInterval;

    public bool rowStopped;
    public int stoppedSlot;

    // Start is called before the first frame update
    void Start()
    {
        rowStopped = false;
        SetSlotItem();
    }

    private void SetSlotItem()
    {
        int Rand;
        List<int> list = new List<int>();
        list = new List<int>(new int[rowItemsCategories.Length]);

        for (int j = 0; j < rowItems.Length; j++)
        {
            Rand = UnityEngine.Random.Range(1, rowItemsCategories.Length);

            while (list.Contains(Rand))
            {
                Rand = UnityEngine.Random.Range(1, rowItemsCategories.Length);
            }

            list[j] = Rand;

            rowItems[j].SetRowItem(Rand, rowItemsCategories[Rand].sprite);
        }
    }

    public void StartRotating()
    {
        stoppedSlot = 0;
        StartCoroutine("Rotate");
    }

    private IEnumerator Rotate()
    {
        rowStopped = false;
        timeInterval = 0.02f;

        for (int i = 0; i < 53; i++)
        {
            for (int j = 0; j < rowItems.Length; j++)
            {
                if (rowItems[j].transform.localPosition.y <= yPos[yPos.Length - 1] - 75)
                    rowItems[j].transform.localPosition = new Vector2(rowItems[j].transform.localPosition.x, yPos[0]);
            }

            for (int j = 0; j < rowItems.Length; j++)
            {
                rowItems[j].gameObject.transform.position = new Vector2(rowItems[j].gameObject.transform.position.x,
                    rowItems[j].gameObject.transform.position.y - 25f);
            }


            yield return new WaitForSeconds(timeInterval);
        }


        yield return new WaitForSeconds(timeInterval);

        for (int i = 0; i < 5; i++) 
        { 
            for (int j = 0; j < rowItems.Length; j++)
            {
                rowItems[j].gameObject.transform.position = new Vector2(rowItems[j].gameObject.transform.position.x,
                    rowItems[j].gameObject.transform.position.y + 5f);
            }

            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < rowItems.Length; i++)
        {
            var list = yPos.ToList();
            float closest = list.OrderBy(item => Math.Abs(rowItems[i].gameObject.transform.localPosition.y - item)).First();
            rowItems[i].transform.localPosition = new Vector2(rowItems[i].transform.localPosition.x, closest);

            yield return new WaitForSeconds(0.1f);
        }

        for (int j = 0; j < rowItems.Length; j++)
        {
            if (rowItems[j].gameObject.transform.localPosition.y == 0f)
                stoppedSlot = rowItems[j].index;
        }


        /*if (isLettered)
            UnityEngine.Debug.Log("Letter"+stoppedSlot);
        else
            UnityEngine.Debug.Log("Category"+stoppedSlot);

        if (isLettered)
            GameController.Instance.stoppedSlotsCategories.Add(,stoppedSlot);
        else
            GameController.Instance.letterIndex = stoppedSlot;*/
        rowStopped = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
