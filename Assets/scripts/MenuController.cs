using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Kontroller odpowiedzialny za menu
/// </summary>
public class MenuController : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void EndGame()
    {
        Debug.Log("wyszedłeś z gry");
        Application.Quit();
    }

}