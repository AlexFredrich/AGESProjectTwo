using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

    //Checks if players are in their zones
    public bool playerOneReady = false;
    public bool playerTwoReady = false;

    //Panel to fade to
    [SerializeField]
    private CanvasGroup uiElement;

	
	// Update is called once per frame
	void Update ()
    {
        //Making sure that both players are in their zones before loading the next scene
        if(playerOneReady == true && playerTwoReady == true)
        {
            StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1));
        }
	
        
	}
    //Fading to black and the load screen
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
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Menu");
    }
}
