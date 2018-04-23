using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCreature : MonoBehaviour {

    [SerializeField]
    private AudioClip creatureGrowlOne;
    [SerializeField]
    private AudioClip creatureGrowlTwo;
    [SerializeField]
    private int pauseTime = 3;

    private bool keepPlaying = true;
    private AudioSource audio;


	// Use this for initialization
	void Start ()
    {
        audio = GetComponent<AudioSource>();
        StartCoroutine(CreatureGrowl());
	}
	

    IEnumerator CreatureGrowl()
    {
        while(keepPlaying)
        {
            audio.PlayOneShot(creatureGrowlOne);
            yield return new WaitForSeconds(pauseTime);
            audio.PlayOneShot(creatureGrowlTwo);
            yield return new WaitForSeconds(pauseTime);
        }
    }
}
