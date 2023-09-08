using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class RowItem : MonoBehaviour
{
    public int index;
    public Image image;
    //public TMP_Text letter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetRowItem(int _index, Sprite _sprite)
    {
        index = _index;
        image.sprite = _sprite;
        image.SetNativeSize();
    }
}
