using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] ObjectPlacement placer;
    [SerializeField] Slider slider;
    [SerializeField] float maxMana = 5f;
    [SerializeField] float manaCd = 1f;

    private float baseManaCd;
    private int portalsInUse;
    private bool usingMana, manaRestore, manaEmpty, manaThreshold;

    // Start is called before the first frame update
    void Start()
    {
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

        if (manaEmpty)
            OutOfMana();

        if (slider.value > 0)
            manaEmpty = false;

        if (slider.value >= (0.2 * slider.maxValue))
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
            slider.value -= Time.deltaTime * portalsInUse;
        else if (slider.value <= 0)
        {
            slider.value = 0;
            usingMana = false;
            manaEmpty = true;
        }
    }

    private void OutOfMana()
    {
        placer.OutOfMana();
        manaEmpty = false;
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
            Debug.Log("Cooling Down mana");
        }
        else if (manaCd <= 0)
        {
            Debug.Log("Cooldown done");
            manaCd = baseManaCd;
            manaRestore = true;
        }
        else if (usingMana == true)
            manaCd = baseManaCd;
    }

    private void ManaRestore()
    {
        if (slider.value < slider.maxValue && manaRestore)
            slider.value += Time.deltaTime;
        else if (slider.value >= slider.maxValue)
        {
            slider.value = slider.maxValue;
            manaRestore = false;
        }
        else if (usingMana == true)
            manaRestore = false;
    }
}
