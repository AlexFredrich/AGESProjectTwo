using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLever : MonoBehaviour {

    [SerializeField]
    private GameObject Door;
    [SerializeField]
    private GameObject Lever;
    [SerializeField]
    private GameObject wardrobe;
    [SerializeField]
    private Transform fallenWardrobe;
    [SerializeField]
    private Light candlelight;

    private Animator anim;
    private Animator doorAnim;

    // Use this for initialization
    void Start()
    {
        anim = Lever.GetComponent<Animator>();
        doorAnim = Door.GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetButton("Fire1"))
        {

            //Lever and Door Animation

            anim.SetBool("Triggered", true);
            doorAnim.SetBool("opened", true);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            wardrobe.transform.position = fallenWardrobe.position;
            wardrobe.transform.rotation = fallenWardrobe.rotation;
            candlelight.GetComponent<Light>().enabled = true;



        }

    }
}
