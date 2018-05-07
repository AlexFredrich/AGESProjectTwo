using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneCheck : MonoBehaviour
{

    [SerializeField]
    private GameObject GameEnd;

    //Checking if player is in the zone
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Male")
        {
            GameEnd.GetComponent<EndGame>().playerOneReady = true;
        }
    }
    //Checking if player has left the zone
    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Male")
        {
            GameEnd.GetComponent<EndGame>().playerOneReady = false;
        }
    }

}
