using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    public Image fillBar;

    public void UpdateEnemyHealth(float currentHealth, float maxHealth)
    {
        fillBar.fillAmount = currentHealth / maxHealth;
    }
}
