using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCollider : MonoBehaviour {
	Protagonist protagonistScript; 

	void Awake(){
		protagonistScript = GameObject.Find ("Protagonist").GetComponent<Protagonist>();
	}

	void OnTriggerEnter(Collider coll){
		Debug.Log ("Hit");
		GameObject collGameObject = coll.gameObject;
		if (collGameObject.name == "Protagonist") {
			gameOver ();
		}
		else if(collGameObject.tag == "Projectile"){
			Destroy (collGameObject);
		}
	}

	void gameOver(){
		protagonistScript.respawn ();
	}
}
