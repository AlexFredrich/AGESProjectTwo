﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

    [SerializeField]
    private GameObject Door;
    [SerializeField]
    private int requiredMass;

    private void OnTriggerStay(Collider item)
    {
        //The door will only open if an item is placed on the trigger with the right mass
        if(item.tag == "Item" && item.GetComponent<Rigidbody>().mass == requiredMass)
        {
            //Will be replaced with a door opening animation
            Door.SetActive(false);
        }

    }

    private void OnTriggerExit(Collider item)
    {
        //If the item falls of the button closes door or should it snap in to place? Close door animation
        if(item.tag == "Item")
        {
            Door.SetActive(true);
        }
    }
}
