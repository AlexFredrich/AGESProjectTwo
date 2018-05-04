using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuFunctionality : MonoBehaviour
{
    [SerializeField]
    private GameObject Instructions, MainOptions, Credits;
    

    public void ToInstructions()
    {
        Instructions.SetActive(true);
        MainOptions.SetActive(false);
    }

    public void StartGame()
    {
        //Loading Screen?
    }

    public void ReturnToMenu()
    {
        MainOptions.SetActive(true);
        Credits.SetActive(false);
        Instructions.SetActive(false);
    }

    public void CreditsScreen()
    {
        MainOptions.SetActive(false);
        Credits.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
