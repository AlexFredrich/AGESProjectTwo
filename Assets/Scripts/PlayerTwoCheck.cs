using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoCheck : MonoBehaviour {


    //Object with the game end script
    [SerializeField]
    private GameObject GameEnd;

    //Checking if player is in zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Female")
        {
            GameEnd.GetComponent<EndGame>().playerTwoReady = true;
        }
    }
    //Checking if player has left the zone
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Female")
        {
            GameEnd.GetComponent<EndGame>().playerTwoReady = false;
        }
    }
}
