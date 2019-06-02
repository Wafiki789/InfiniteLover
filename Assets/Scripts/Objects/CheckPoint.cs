using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {
	GameObject level;
	Level levelScript;
	GameObject protagonist;
	Protagonist protagonistScript;
	ParallaxBackground backgroundScript;
    Symbol symbolScript;

	void Awake(){
		level = GameObject.FindGameObjectWithTag ("Level");
		levelScript = level.GetComponent<Level> ();

		protagonist = GameObject.Find ("Protagonist");
		protagonistScript = protagonist.GetComponent<Protagonist> ();

		backgroundScript = GameObject.Find ("Background").GetComponent<ParallaxBackground> ();
        symbolScript = GameObject.Find("Symbol").GetComponent<Symbol>();
	}

	void OnTriggerEnter(Collider coll){
		if (protagonistScript.respawning && coll.gameObject.name == "Protagonist") {
			Debug.Log ("Stop");
			levelScript.speed = 0;
			protagonistScript.goingDown = true;
			protagonistScript.index = 0;
			protagonistScript.setAction();
            protagonistScript.jumping = false;

            symbolScript.index = 0;
            symbolScript.changeSymbol();

			//backgroundScript.pause ();
		}
	}
}