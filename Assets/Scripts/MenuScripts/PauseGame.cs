using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseGame : MonoBehaviour
{
    //Keeping track wether the game is paused or not
    private bool Paused = false;

    //Pause menu
    [SerializeField]
    private GameObject pauseMenu;


    // Update is called once per frame
    void Update ()
    {
        //If either players press pause it'll bring up the menu
        if(Input.GetButtonDown("Pause") || Input.GetButtonDown("Pause2"))
        {
            Paused = !Paused;
        }

        if(Paused)
        {
            //if the game is paused, time will freeze
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            
        }
        else if(!Paused)
        {
            //if the game isn't paused, time will resume
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }


    }

    //Continue button if someone wants to use the button, but can easily just press the same controller button as well.
    public void Continue()
    {
        
        Paused = false;
        pauseMenu.SetActive(false);

    }

    //Button to return to menu
    public void ExitGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
