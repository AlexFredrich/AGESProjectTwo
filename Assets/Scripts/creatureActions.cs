using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class creatureActions : MonoBehaviour {

    private NavMeshAgent nav;
    private Animator anim;

	// Use this for initialization
	void Start ()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        nav.speed = .4f;
        anim.speed = .4f;
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        anim.SetFloat("velocity", nav.velocity.magnitude);
	}
}
