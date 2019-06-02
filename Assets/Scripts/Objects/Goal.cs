using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {
	public string nextLevel;
	Level levelScript;
	ParallaxBackground backgroundScript;
	GameObject heart;

	void Awake(){
		levelScript = GameObject.FindGameObjectWithTag ("Level").GetComponent<Level> ();
		backgroundScript = GameObject.Find ("Background").GetComponent<ParallaxBackground> ();
		heart = gameObject.transform.Find ("Heart").gameObject;
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.name == "Protagonist") {
			coll.gameObject.GetComponent<Protagonist> ().goalReached = true;
			levelScript.speed = 0f;
			//backgroundScript.pause ();
			Invoke ("loadNextLevel", 1f);

			//Faster: find child or instantiate gameobject?
			heart.GetComponent<Renderer>().enabled = true;
		}
	}

	public void loadNextLevel(){
		SceneManager.LoadScene (nextLevel);
	}
}