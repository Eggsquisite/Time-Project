using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    Color currentColor;
    private Transform bar;
    private SpriteRenderer barSprite;
    private bool timerActive = false;

    // Start is called before the first frame update
    void Start()
    {
        bar = transform.Find("Bar");
        barSprite = bar.transform.Find("BarSprite").GetComponent<SpriteRenderer>();
        currentColor = barSprite.color;
    }

    public void SetTime(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }

    public void SetColor(Color colorValue)
    {
        barSprite.color = colorValue;
    }

    public void ResetColor()
    {
        barSprite.color = currentColor;
    }

    public void SetTimerStatus(bool status)
    {
        timerActive = status;
    }

    public bool GetTimerStatus()
    {
        return timerActive;
    }
}
