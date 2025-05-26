using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Healthbar : MonoBehaviour
{
    public Slider hSlider;
    public TMP_Text health_txt;

    public int GetMaxHealth()
    {
        return (int)hSlider.maxValue;
    }

    public int GetHealth()
    {
        return (int)hSlider.value;
    }

    public void SetMaxHealth(int health)
    {
        hSlider.maxValue = health;
        hSlider.value = health;
        health_txt.text = health.ToString();
    }

    public void SetHealth(int health)
    {
        hSlider.value = health;
        health_txt.text = health.ToString();
    }
}
