using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class creatureActions : MonoBehaviour {
    //The player it's hunting
    [SerializeField]
    private GameObject player;
    //Where the creature can see
    [SerializeField]
    private Transform creatureEyes;
    //What points the creature follows
    [SerializeField]
    private Transform[] waypoints;
    //How long the creature should stay at the waypoints
    [SerializeField]
    private float idlingTime = 5;
    //The camera that takes over if the player is caught
    [SerializeField]
    private GameObject deathCam;
    //The position that camera will take
    [SerializeField]
    private Transform camPosition;

    //Navmesh for the creature
    private NavMeshAgent nav;
    //The animations
    private Animator anim;
    //The source for the growls
    private AudioSource creatureSounds;
    //Whether the player is alive
    private bool alive = true;
    //The states depending on what the creature sees
    private string state = "idle";
    //Initial spot for the way points
    private int destinationPoint = 0;
    //For the conintuous growl
    private bool creatureGrowling = true;

    //Different growls the creature makes
    [SerializeField]
    private AudioClip foundGrowl;
    [SerializeField]
    private int TimeBetweenGrowls;
    [SerializeField]
    private AudioClip normalGrowl;


	// Use this for initialization
	void Start ()
    {
        creatureSounds = GetComponent<AudioSource>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        nav.speed = .5f;
        anim.speed = .5f;
        StartCoroutine(CreatureGrowling());
	}

    //If the player is still alive and the player enters the the eye sight area, check if the monster can see the player and react if it does
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
    //Making the creautre growl every so often
    IEnumerator CreatureGrowling()
    {
        while(creatureGrowling)
        {
            creatureSounds.PlayOneShot(normalGrowl);
            yield return new WaitForSeconds(TimeBetweenGrowls);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //The creatures walk speed
        anim.SetFloat("velocity", nav.velocity.magnitude);

        //If the creature is idle, find a new waypoint to walk to
        if(state == "idle")
        {
            if (waypoints.Length == 0)
                return;
            nav.destination = waypoints[destinationPoint].position;
            destinationPoint++;
            if(destinationPoint >= waypoints.Length)
            {
                destinationPoint = 0;
            }
            state = "walk";
        }
        //If the creature is walking, move to the destination and upon getting closer search
        if(state == "walk")
        {
            if(nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
            {
                state = "search";
            }
        }
        //Search will h=keep the creature there for a certain amount of time
        if(state == "search")
        {
            StartCoroutine(IdlePause());
        }
        //If the creature sees the player, it will begin to chase them. if the player is far enough it will return to it's path
        if(state == "chase")
        {
            nav.destination = player.transform.position;

            //losing sight of the player//
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if(distance > 3f)
            {
                state = "search";
            }
            //If the creature catches the player it will move in to the kill state 
            else if(nav.remainingDistance <= nav.stoppingDistance + .65f && !nav.pathPending)
            {
                if(player.GetComponent<playerLocated>().alive)
                {
                    state = "kill";
                    player.GetComponent<playerLocated>().alive = false;
                    if (player.name == "Female")
                        player.GetComponent<playerMovement2>().enabled = false;
                    else if (player.name == "Male")
                        player.GetComponent<playerMovement1>().enabled = false;
                    deathCam.SetActive(true);
                    deathCam.transform.position = Camera.main.transform.position;
                    deathCam.transform.rotation = Camera.main.transform.rotation;
                    Camera.main.gameObject.SetActive(false);
                    creatureSounds.pitch = .7f;
                    creatureSounds.PlayOneShot(foundGrowl);
                    Invoke("Reset", 2f);

                }
            }
          
        }
        //Kill state invokes the kill cam and then restarts the level
        if(state == "kill")
        {
            deathCam.transform.position = Vector3.Slerp(deathCam.transform.position, camPosition.position, 10f * Time.deltaTime);
            deathCam.transform.rotation = Quaternion.Slerp(deathCam.transform.rotation, camPosition.rotation, 10f * Time.deltaTime);
            anim.speed = 1f;
            nav.SetDestination(deathCam.transform.position);

        }
	}
    //The searching wait
    IEnumerator IdlePause()
    {
        //transform.Rotate(0f, 90f * Time.deltaTime, 0f);
        yield return new WaitForSeconds(idlingTime);
        state = "idle";
    }
    //Reseting the level
    private void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
