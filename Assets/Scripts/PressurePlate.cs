using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

    [SerializeField]
    private GameObject Door;

    private void OnTriggerStay(Collider item)
    {
        
        if(item.tag == "Item")
        {
            Door.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider item)
    {
        if(item.tag == "Item")
        {
            Door.SetActive(true);
        }
    }
}
