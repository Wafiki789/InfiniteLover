using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteKiller : MonoBehaviour {
	void OnTriggerEnter(Collider coll){
		if (coll.tag == "Projectile") {
			Destroy (coll.gameObject);
		}
	}
}