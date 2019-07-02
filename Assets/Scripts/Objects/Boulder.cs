using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour {
    ObjectsManager objectManagerScript;
	Protagonist protagonistScript;
	Renderer[] rend;
	bool boulderIsDestroyed = false;



	void Awake(){
		protagonistScript = GameObject.Find ("Protagonist").GetComponent<Protagonist>();
		rend = gameObject.GetComponentsInChildren<Renderer> ();
        objectManagerScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<ObjectsManager>();
	}

	void OnTriggerEnter(Collider coll){
		string gameObjectName = coll.gameObject.name;
        if (gameObjectName == "Protagonist" && !boulderIsDestroyed) {
            gameOver();
        }
        else if (gameObjectName.Contains("Bullet") && !boulderIsDestroyed) {
            Destroy(coll.gameObject);
            for (int i = 0; i < rend.Length; i++) {
                objectManagerScript.destroyedBoulders.Add(gameObject);
                rend[i].enabled = false;
            }
            boulderIsDestroyed = true;
        }
        else if (coll.gameObject.tag == "RendBorder" && protagonistScript.respawning) {
            makeVisible();
        }
	}

	void makeVisible() {
		for (int i = 0; i < rend.Length; i++) {
			rend [i].enabled = true;
		}
		boulderIsDestroyed = false;
	}

	void gameOver(){
		protagonistScript.respawn ();
	}
}