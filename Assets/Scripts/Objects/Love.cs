using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Love : MonoBehaviour{
    Renderer rend;

    private void Awake()
    {
        rend = this.gameObject.GetComponentInChildren<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Protagonist")
        {
            //rend.enabled = false;
            print("Love");
        }
    }
}