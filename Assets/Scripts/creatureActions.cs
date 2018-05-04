using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class creatureActions : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Transform creatureEyes;
    [SerializeField]
    private Transform[] waypoints;
    [SerializeField]
    private float idlingTime = 5;
    [SerializeField]
    private GameObject deathCam;
    [SerializeField]
    private Transform camPosition;

    private NavMeshAgent nav;
    private Animator anim;
    private AudioSource creatureSounds;
    private bool alive = true;
    private string state = "idle";
    private int destinationPoint = 0;
    private bool creatureGrowling = true;

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
        anim.SetFloat("velocity", nav.velocity.magnitude);

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

            //losing sight of the player//
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if(distance > 10f)
            {
                state = "search";
            }
            else if(nav.remainingDistance <= nav.stoppingDistance + 1f && !nav.pathPending)
            {
                if(player.GetComponent<playerLocated>().alive)
                {
                    state = "kill";
                    player.GetComponent<playerLocated>().alive = false;
                    player.GetComponent<playerMovement1>().enabled = false;
                    deathCam.SetActive(true);
                    deathCam.transform.position = Camera.main.transform.position;
                    deathCam.transform.rotation = Camera.main.transform.rotation;
                    Camera.main.gameObject.SetActive(false);
                    creatureSounds.pitch = .7f;
                    creatureSounds.PlayOneShot(foundGrowl);
                    Invoke("reset", 2f);

                }
            }
          
        }

        if(state == "kill")
        {
            deathCam.transform.position = Vector3.Slerp(deathCam.transform.position, camPosition.position, 10f * Time.deltaTime);
            deathCam.transform.rotation = Quaternion.Slerp(deathCam.transform.rotation, camPosition.rotation, 10f * Time.deltaTime);
            anim.speed = 1f;
            nav.SetDestination(deathCam.transform.position);

        }
	}

    IEnumerator IdlePause()
    {
        //transform.Rotate(0f, 90f * Time.deltaTime, 0f);
        yield return new WaitForSeconds(idlingTime);
        state = "idle";
    }

    private void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
