using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseGame : MonoBehaviour
{
    private bool Paused = false;

    [SerializeField]
    private GameObject pauseMenu;


    // Update is called once per frame
    void Update ()
    {
        if(Input.GetButtonDown("Pause") || Input.GetButtonDown("Pause2"))
        {
            Paused = !Paused;
        }

        if(Paused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            
        }
        else if(!Paused)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }


    }

    public void Continue()
    {
        Paused = false;
        pauseMenu.SetActive(false);

    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
