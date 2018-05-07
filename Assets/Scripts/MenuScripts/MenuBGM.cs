using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBGM : MonoBehaviour {
    //Playing the audio but in reverse.
    private AudioSource audio;
	// Use this for initialization
	void Start ()
    {
        audio = GetComponent<AudioSource>();
        audio.timeSamples = audio.clip.samples - 1;
        audio.pitch = -1;
        audio.Play();
	}
	
}
