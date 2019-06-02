using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookTarget : MonoBehaviour {
	Protagonist protagonistScript;

	void Awake(){
		protagonistScript = GameObject.Find ("Protagonist").GetComponent<Protagonist>();
	}

	void OnTriggerEnter(Collider coll){
		string gameObjectName = coll.gameObject.name;
		Debug.Log (gameObjectName);

		if (gameObjectName == "Protagonist") {
			protagonistScript.hooked = false;
		}
		else if(gameObjectName.Contains("Hook")){
			Debug.Log ("hooked");
			protagonistScript.hooked = true;
		}
	}
}