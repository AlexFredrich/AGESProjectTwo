using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneCheck : MonoBehaviour
{

    [SerializeField]
    private GameObject GameEnd;


    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Male")
        {
            GameEnd.GetComponent<EndGame>().playerOneReady = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Male")
        {
            GameEnd.GetComponent<EndGame>().playerOneReady = false;
        }
    }

}
