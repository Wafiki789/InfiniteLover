using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour {
	Protagonist protagonistScript;
	Renderer[] rend;
	bool enemyIsDead = false;

	void Awake(){
		protagonistScript = GameObject.Find ("Protagonist").GetComponent<Protagonist>();
		rend = gameObject.GetComponentsInChildren<Renderer> ();
	}

	void OnTriggerEnter(Collider coll){
		string gameObjectName = coll.gameObject.name;
		if (gameObjectName == "Protagonist" && !enemyIsDead) {
			gameOver ();
		}
		else if(gameObjectName.Contains("Bullet")){
			Destroy (coll.gameObject);
			for (int i = 0; i < rend.Length; i++) {
				rend [i].enabled = false;
			}
			enemyIsDead = true;
			Invoke("makeVisible", 1.5f);
		}
	}

	void makeVisible() {
		for (int i = 0; i < rend.Length; i++) {
			rend [i].enabled = true;
		}
		enemyIsDead = false;
	}

	void gameOver(){
		protagonistScript.respawn ();
	}
}