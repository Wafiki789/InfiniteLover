using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteKiller : MonoBehaviour {
	void OnTriggerEnter(Collider coll){
		Debug.Log ("Coucou");
		if (coll.tag == "Projectile") {
			Debug.Log ("Bye");
			Destroy (coll.gameObject);
		}
	}
}