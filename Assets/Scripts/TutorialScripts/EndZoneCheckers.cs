using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZoneCheckers : MonoBehaviour {

    [SerializeField]
    private Light playerOneLight;
    [SerializeField]
    private Light playerTwoLight;
    [SerializeField]
    private GameObject gameStart;

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Male")
        {
            playerOneLight.GetComponent<Light>().enabled = true;
            gameStart.GetComponent<GameStart>().playerOneReady = true;
            
        }
        else if(other.name == "Female")
        {
            playerTwoLight.GetComponent<Light>().enabled = true;
            gameStart.GetComponent<GameStart>().playerTwoReady = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Male")
        {
            playerOneLight.GetComponent<Light>().enabled = false;
            gameStart.GetComponent<GameStart>().playerOneReady = false;

        }
        else if (other.name == "Female")
        {
            playerTwoLight.GetComponent<Light>().enabled = false;
            gameStart.GetComponent<GameStart>().playerTwoReady = false;
        }
    }

}
