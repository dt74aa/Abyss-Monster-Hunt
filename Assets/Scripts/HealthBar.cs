using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HealthBar : MonoBehaviour
{
    public Image fillBar;
    public TextMeshProUGUI heathText;

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        fillBar.fillAmount = currentHealth/maxHealth;
        heathText.text = currentHealth.ToString()+ " / " + maxHealth.ToString();
    }
}
