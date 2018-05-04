using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {

    [SerializeField]
    private GameObject Door;

    private Animator doorAnim;

    private void Start()
    {
        doorAnim = Door.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            doorAnim.SetBool("opened", true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            doorAnim.SetBool("opened", false);
        }
    }

}
