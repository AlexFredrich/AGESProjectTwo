using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levers : MonoBehaviour {

    [SerializeField]
    private GameObject Door;
    [SerializeField]
    private GameObject Lever;

    private Animator anim;
    private Animator doorAnim;

    [SerializeField]
    private Light candleLight;

	// Use this for initialization
	void Start ()
    {
        anim = Lever.GetComponent<Animator>();
        doorAnim = Door.GetComponent<Animator>();
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && Input.GetButton("Fire1"))
        {
            
                //Lever and Door Animation
                
            anim.SetBool("Triggered", true);
            doorAnim.SetBool("opened", true);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            candleLight.GetComponent<Light>().enabled = true;

            

        }

    }

}
