using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TrangChu : MonoBehaviour
{
    public void ChoiMoi()
    {
        SceneManager.LoadScene(1);
    }

    public void HuongDan()
    {
        SceneManager.LoadScene(2);
    }

    public void Thoat()
    {
        Application.Quit();
    }

    public void ThoatHd() {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ThoatMenu()
    {
        SceneManager.LoadScene(0);
    }
}
