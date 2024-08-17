using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpWeapon : MonoBehaviour
{
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject Weapon;
    public void UpLevelWeaPon1()
    {
        weapon1.SetActive(true);
        Time.timeScale = 1.0f;
        Weapon.SetActive(false);
    }
    public void UpLevelWeaPon2()
    {
        weapon2.SetActive(true);
        Time.timeScale = 1.0f;
        Weapon.SetActive(false);
    }
    public void UpLevelWeaPon3()
    {
        weapon3.SetActive(true);
        Time.timeScale = 1.0f;
        Weapon.SetActive(false);
    }
}
