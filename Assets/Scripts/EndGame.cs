using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

    public bool playerOneReady = false;
    public bool playerTwoReady = false;

    [SerializeField]
    private CanvasGroup uiElement;

	
	// Update is called once per frame
	void Update ()
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
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Menu");
    }
}
