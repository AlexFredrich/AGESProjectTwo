using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBGM : MonoBehaviour {

    private AudioSource audio;
	// Use this for initialization
	void Start ()
    {
        audio = GetComponent<AudioSource>();
        audio.timeSamples = audio.clip.samples - 1;
        audio.pitch = -1;
        audio.Play();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
