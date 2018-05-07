using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class MenuFunctionality : MonoBehaviour
{
    //UI Elements
    [SerializeField]
    private GameObject MainOptions, Credits;
    [SerializeField]
    private GameObject playGameButton, returnButton;
    [SerializeField]
    private CanvasGroup uiElement;
    [SerializeField]
    private Slider progressBarSlider;

    //Next scene
    private static string sceneToLoad;

    //Making sure that the controller navigation works
    [SerializeField]
    EventSystem eventSystem;
    private GameObject storeSelcted;

    //Starting the game that brings the loading screen up and then loads the next level
    public void StartGame(string nameOfSceneToLoad)
    {

        sceneToLoad = nameOfSceneToLoad;
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1));
        storeSelcted = eventSystem.firstSelectedGameObject;
    }

    //Returning to the menu from the credits screen
    public void ReturnToMenu()
    {
        MainOptions.SetActive(true);
        Credits.SetActive(false);
        eventSystem.SetSelectedGameObject(playGameButton);
        
    }

    //Bringing up the credits screen and making sure controller navigation works
    public void CreditsScreen()
    {
        MainOptions.SetActive(false);
        Credits.SetActive(true);
        eventSystem.SetSelectedGameObject(returnButton);
    }

    //Exiting the game
    public void ExitGame()
    {
        Application.Quit();
    }

    //If you use the mouse, the controllers will still work
    private void Update()
    {
        if(eventSystem.currentSelectedGameObject != storeSelcted)
        {
            if (eventSystem.currentSelectedGameObject == null)
                eventSystem.SetSelectedGameObject(storeSelcted);
            else
                storeSelcted = eventSystem.currentSelectedGameObject;
        }
    }

    //Fading to a black screen
    public IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float start, float end, float lerpTime = 1f)
    {
        float timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentageComple = timeSinceStarted / lerpTime;

        while(true)
        {
            timeSinceStarted = Time.time - timeStartedLerping;
            percentageComple = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComple);

            canvasGroup.alpha = currentValue;

            if (percentageComple >= 1)
            {
                
                StartCoroutine(LoadSceneAsync(sceneToLoad));
                break;
            }
            yield return new WaitForEndOfFrame();

        }
    }

    //Loading the next level
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return new WaitForSeconds(0.1f);

        var task = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

        while (!task.isDone)
        {
            progressBarSlider.value = task.progress;
            yield return null;

        }
        progressBarSlider.value = 1;
        yield return new WaitForSeconds(.2f);
        SceneManager.UnloadSceneAsync("Menu");
    }
}
