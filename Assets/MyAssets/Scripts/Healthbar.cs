using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider hSlider;
    [SerializeField] private TMP_Text healthText;

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
        healthText.text = health.ToString();
    }

    public void SetHealth(int health)
    {
        hSlider.value = health;
        healthText.text = health.ToString();
    }
}
