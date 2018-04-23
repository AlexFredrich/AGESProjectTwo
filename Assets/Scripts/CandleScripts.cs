using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleScripts : MonoBehaviour {

    Light candle;

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

	IEnumerator CandleFlicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            candle.intensity = Random.Range(minIntensity, maxIntensity);
        }
    }
}

