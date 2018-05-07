using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStart : MonoBehaviour {

    //Similar to the game end, checking for players in the zone before fading to black and in to the next scene

    public bool playerOneReady = false;
    public bool playerTwoReady = false;
    [SerializeField]
    private CanvasGroup uiElement;


    private void Update()
    {
        if(playerOneReady == true && playerTwoReady == true)
        {
            StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1));
        }
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float start, float end, float lerpTime = 1f)
    {
        float timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentageComple = timeSinceStarted / lerpTime;

        while (true)
        {
            timeSinceStarted = Time.time - timeStartedLerping;
            percentageComple = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComple);

            canvasGroup.alpha = currentValue;

            if (percentageComple >= 1)
            {
                break;
            }
            yield return new WaitForEndOfFrame();

        }

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Environment");
    }

}
