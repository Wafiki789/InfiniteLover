using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousAction : MonoBehaviour {
    public bool isActive;

    Symbol symbolScript;

    private void Awake(){
        isActive = true;
        symbolScript = GameObject.Find("Symbol").GetComponent<Symbol>();
    }

    void OnTriggerEnter(Collider other){
        if (isActive){
            GameObject otherObject = other.gameObject;
            if (otherObject.name == "Protagonist"){
                Protagonist protagonistScript = otherObject.GetComponent<Protagonist>();
                protagonistScript.index-=2;
                protagonistScript.setAction();

                for (int i = 0; i < protagonistScript.actionWheel.Length - 1; i++) {
                    symbolScript.changeSymbol();
                }
            }
        }
    }
}