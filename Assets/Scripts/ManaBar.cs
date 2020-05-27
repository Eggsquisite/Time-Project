using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] float portalMultiplier = 0.5f;
    [SerializeField] float maxMana = 5f;
    [SerializeField] [Range(0,1)] float thresholdMultiplier = 1f;
    [SerializeField] float manaRegen = 2f;
    [SerializeField] float manaCd = 1f;

    private ObjectPlacement placer;
    private float baseManaCd;
    private int portalsInUse;
    private bool usingMana, manaRestore, manaThreshold;

    // Start is called before the first frame update
    void Start()
    {
        placer = Camera.main.GetComponent<ObjectPlacement>();
        baseManaCd = manaCd;
        SetMaxMana(maxMana);
    }

    private void SetMaxMana(float mana)
    {
        slider.maxValue = mana;
        slider.value = mana;
    }

    // Update is called once per frame
    void Update()
    {
        if (usingMana)
            UseMana();

        if (slider.value < slider.maxValue && !usingMana)
            ManaCooldown();

        if (!usingMana)
            ManaRestore();

        //if (manaEmpty)
            //OutOfMana();

        //if (slider.value > 0)
            //manaEmpty = false;

        if (slider.value >= (thresholdMultiplier * slider.maxValue))
            manaThreshold = true;

        if (manaThreshold)
            ManaThreshold();
    }

    public void UseMana(bool status)
    {
        if (slider.value > 0)
        {
            usingMana = status;
            portalsInUse++;
            Debug.Log("Portals: " + portalsInUse);
        }
    }
    private void UseMana()
    {
        manaRestore = false;
        if (slider.value > 0)
            slider.value -= Time.deltaTime * (portalsInUse * (0.5f + portalMultiplier * portalsInUse));
        else if (slider.value <= 0)
        {
            slider.value = 0;
            usingMana = false;
            //manaEmpty = true;
        }
    }

    private void OutOfMana()
    {
        placer.OutOfMana();
        portalsInUse = 0;
    }

    private void ManaThreshold()
    {
        placer.SetManaReady(true);
        manaThreshold = false;
    }


    private void ManaCooldown()
    {
        if (manaCd > 0 && !manaRestore)
        {
            manaCd -= Time.deltaTime;
            OutOfMana();
        }
        else if (manaCd <= 0)
        {
            manaCd = baseManaCd;
            manaRestore = true;
        }
        else if (usingMana == true)
            manaCd = baseManaCd;
    }

    private void ManaRestore()
    {
        if (slider.value < slider.maxValue && manaRestore)
            slider.value += Time.deltaTime * manaRegen;
        else if (slider.value >= slider.maxValue)
        {
            slider.value = slider.maxValue;
            manaRestore = false;
        }
        else if (usingMana == true)
            manaRestore = false;
    }
}
