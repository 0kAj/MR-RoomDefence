using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Animations;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider hSlider;
    [SerializeField] private TMP_Text healthText;

    public float rotationSpeed = 5f;

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

    public void Update()
    {
        //  var player = GameObject.FindGameObjectsWithTag("Player")[0];

        // Vector3 direction = player.transform.position - transform.position;


        Vector3 direction = Camera.main.transform.position - transform.position;
        direction.y = 0; // Keep only the horizontal direction

        if (direction.sqrMagnitude > 0.001f)
        {
            float targetYAngle = (Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + 180) % 360;
            float currentYAngle = transform.eulerAngles.y;

            // Smoothly interpolate the y rotation towards targetYAngle
            float newYAngle = Mathf.LerpAngle(currentYAngle, targetYAngle, Time.deltaTime * rotationSpeed);
            Debug.Log("New Angle Y: " + newYAngle);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, newYAngle, transform.eulerAngles.z);
        }
    }
}
