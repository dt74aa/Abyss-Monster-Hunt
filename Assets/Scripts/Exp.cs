using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Exp : MonoBehaviour
{
    public Image fillBarExp;
    public TextMeshProUGUI ExpText;
    public void UpdateExp(float currentExp, float maxExp)
    {
        fillBarExp.fillAmount = currentExp / maxExp;
        ExpText.text = currentExp.ToString() + " / " + maxExp.ToString();
    }
}
