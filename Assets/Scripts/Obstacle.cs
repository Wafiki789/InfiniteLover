using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
	Protagonist protagonistScript; 

	void Awake(){
		protagonistScript = GameObject.Find ("Protagonist").GetComponent<Protagonist>();
	}
	
	void OnTriggerEnter(Collider coll){
		string gameObjectName = coll.gameObject.name;
		if (gameObjectName == "Protagonist" && !protagonistScript.respawning) {
			gameOver ();
		}
	}

	void gameOver(){
		protagonistScript.respawn ();
	}
}