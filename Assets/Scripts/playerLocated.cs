using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLocated : MonoBehaviour {

    public bool alive = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Eyes")
        {
            other.transform.parent.GetComponent<creatureActions>().CheckSight();
        }
    }

}
