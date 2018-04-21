using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levers : MonoBehaviour {

    [SerializeField]
    private GameObject Door;

    private Animator anim;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && Input.GetButton("Interact"))
        {
            
                //Lever and Door Animation
                
                anim.SetBool("Triggered", true);
                Door.SetActive(false);
                gameObject.GetComponent<BoxCollider>().enabled = false;

            

        }

    }

}
