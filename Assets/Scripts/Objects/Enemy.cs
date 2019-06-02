using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO:
/*
Finir le jump DONE
Awake: set la position en y (pour pas avoir a la hardcoder tout le temps)
Designer le premier niveau DONE
Comment s'occuper des vies/reset?
Faire un splash-screen + instructions
Tite-animation avec coeur
*/

public class Enemy : MonoBehaviour {
	Protagonist protagonistScript;
    Renderer rend;
    bool enemyIsDead = false;

	void Awake(){
		protagonistScript = GameObject.Find ("Protagonist").GetComponent<Protagonist>();
        rend = gameObject.GetComponent<Renderer>();
	}

	void OnTriggerEnter(Collider coll){
		string gameObjectName = coll.gameObject.name;
		if (gameObjectName == "Protagonist" && !enemyIsDead) {
			gameOver ();
		}
		else if(gameObjectName.Contains("Bullet")){
			Destroy (coll.gameObject);
            rend.enabled = false;
            enemyIsDead = true;
            Invoke("makeVisible", 1f);
		}
	}

    void makeVisible() {
        rend.enabled = true;
        enemyIsDead = false;
    }

	void gameOver(){
		protagonistScript.respawn ();
	}
}