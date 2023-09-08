using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackpotController : MonoBehaviour
{
    public Row[] rows;
    public UISpriteAnimation handle;

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.rowItemsCategories = rows[1].rowItemsCategories;
    }

    public void StartSpinning()
    {
        handle.Func_PlayUIAnim();

        for (int i = 0; i < rows.Length; i++)
        {
            rows[i].rowStopped = false;
        }

        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < rows.Length; i++)
        {
            rows[i].StartRotating();
            yield return new WaitForSeconds(1f);
        }
    }
}
