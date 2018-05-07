using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleScripts : MonoBehaviour {

    //Light component to turn on and off
    private Light candle;

    //Setting the intensity for candle flicker
    [SerializeField]
    float minIntensity;
    [SerializeField]
    float maxIntensity;

    [SerializeField]
    float minWaitTime;
    [SerializeField]
    float maxWaitTime;

	// Use this for initialization
	void Start ()
    {
        candle = GetComponent<Light>();
        StartCoroutine(CandleFlicker());
	}
    //The wait time between flickers that are random to stimulate a real candle
	IEnumerator CandleFlicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            candle.intensity = Random.Range(minIntensity, maxIntensity);
        }
    }
}

