using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPressurePlate : MonoBehaviour {

    [SerializeField]
    private GameObject Door;
    [SerializeField]
    private int requiredMass;

    private Animator anim;
    private Animator doorAnim;

    [SerializeField]
    private Light lightbulb;

    private void Start()
    {
        anim = GetComponent<Animator>();
        doorAnim = Door.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider item)
    {
        //The door will only open if an item is placed on the trigger with the right mass
        if (item.tag == "Item" && item.GetComponent<Rigidbody>().mass == requiredMass)
        {
            anim.SetBool("Triggered", true);
            //Will be replaced with a door opening animation
            doorAnim.SetBool("opened", true);
            lightbulb.GetComponent<Light>().enabled = true;
        }

    }
}
