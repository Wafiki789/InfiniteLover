using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCollider : MonoBehaviour {
	Protagonist protagonistScript; 

	void Awake(){
		protagonistScript = GameObject.Find ("Protagonist").GetComponent<Protagonist>();
	}

    void OnTriggerEnter(Collider coll) {
        GameObject collGameObject = coll.gameObject;
        if (collGameObject.name == "Protagonist")
        {
            gameOver();
        }
        else if (collGameObject.tag == "Projectile")
        {
            Destroy(collGameObject);
        }
        else if (collGameObject.name.Contains("Floor") || collGameObject.name.Contains("Platform")) {
            Destroy(GetComponent<FloorCollider>());
        }
	}

	void gameOver(){
		protagonistScript.respawn ();
	}
}