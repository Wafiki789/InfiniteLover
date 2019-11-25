using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextAction : MonoBehaviour{
    public bool isActive;

    Symbol symbolScript;

    private void Awake(){
        isActive = true;
        symbolScript = GameObject.Find("Symbol").GetComponent<Symbol>();
    }

    void OnTriggerEnter(Collider other){
        if (isActive) {
            GameObject otherObject = other.gameObject;
            if (otherObject.name == "Protagonist"){
                Protagonist protagonistScript = otherObject.GetComponent<Protagonist>();
                protagonistScript.changeIndex(1);
                protagonistScript.setAction();
                symbolScript.changeSymbol();
            }
        }
    }
}