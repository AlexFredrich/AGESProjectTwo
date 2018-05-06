using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class MenuFunctionality : MonoBehaviour
{
    [SerializeField]
    private GameObject MainOptions, Credits;
    [SerializeField]
    private GameObject playGameButton, returnButton;
    [SerializeField]
    private CanvasGroup uiElement;
    [SerializeField]
    private Slider progressBarSlider;

    private bool done = false;
    private static string sceneToLoad;

    [SerializeField]
    EventSystem eventSystem;
    private GameObject storeSelcted;

    public void StartGame(string nameOfSceneToLoad)
    {

        sceneToLoad = nameOfSceneToLoad;
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1));
        storeSelcted = eventSystem.firstSelectedGameObject;
    }


    public void ReturnToMenu()
    {
        MainOptions.SetActive(true);
        Credits.SetActive(false);
        eventSystem.SetSelectedGameObject(playGameButton);
        
    }

    public void CreditsScreen()
    {
        MainOptions.SetActive(false);
        Credits.SetActive(true);
        eventSystem.SetSelectedGameObject(returnButton);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

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
                // done = true;
                StartCoroutine(LoadSceneAsync(sceneToLoad));
                break;
            }
            yield return new WaitForEndOfFrame();

        }
    }

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
