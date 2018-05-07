using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLocated : MonoBehaviour {


    public bool alive = true;
    //If the player enters the eye sight of the creature, it will invoke the check sight function
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Eyes")
        {
            other.transform.parent.GetComponent<creatureActions>().CheckSight();
        }
    }

}
