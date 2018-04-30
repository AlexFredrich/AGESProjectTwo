using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class creatureActions : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Transform creatureEyes;
    [SerializeField]
    private Transform[] waypoints;
    [SerializeField]
    private float idlingTime = 5;

    private NavMeshAgent nav;
    private Animator anim;
    private AudioSource creatureSounds;
    private bool alive = true;
    private string state = "idle";
    private int destinationPoint = 0;

    [SerializeField]
    private AudioClip foundGrowl;


	// Use this for initialization
	void Start ()
    {
        creatureSounds = GetComponent<AudioSource>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        nav.speed = .4f;
        anim.speed = .4f;
		
	}

    public void CheckSight()
    {
        if(alive)
        {
            RaycastHit rayHit;
            if(Physics.Linecast(creatureEyes.position, player.transform.position, out rayHit))
            {
                if(state != "kill")
                {
                    state = "chase";
                    nav.speed = .8f;
                    anim.speed = .8f;
                    creatureSounds.PlayOneShot(foundGrowl);
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        anim.SetFloat("velocity", nav.velocity.magnitude);

        if(state == "idle")
        {
            if (waypoints.Length == 0)
                return;
            nav.destination = waypoints[destinationPoint].position;
            destinationPoint = (destinationPoint + 1) % waypoints.Length;
            state = "walk";
        }

        if(state == "walk")
        {
            if(nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
            {
                state = "search";
            }
        }

        if(state == "search")
        {
            StartCoroutine(IdlePause());
        }

        if(state == "chase")
        {
            nav.destination = player.transform.position;
        }
	}

    IEnumerator IdlePause()
    {
        yield return new WaitForSeconds(idlingTime);
        state = "idle";
    }
}
