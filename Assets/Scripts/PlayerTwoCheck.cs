using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoCheck : MonoBehaviour {

    [SerializeField]
    private GameObject GameEnd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Female")
        {
            GameEnd.GetComponent<EndGame>().playerTwoReady = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Female")
        {
            GameEnd.GetComponent<EndGame>().playerTwoReady = false;
        }
    }
}
