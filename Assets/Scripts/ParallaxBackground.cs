using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour {
	/*public GameObject[] backGroundElements1;
	public GameObject[] backGroundElements2;
	public float speed1;
	public float speed2;
	float basicSpeed1;
	float basicSpeed2;

	float acceleration1;
	float acceleration2;

	float backGroundElements1Pos;
	float backGroundElements2Pos;

	void Start () {
		basicSpeed1 = speed1;
		basicSpeed2 = speed2;
		acceleration1 = speed1 * 3;
		acceleration2 = speed2 * 3;

		backGroundElements1Pos = backGroundElements1 [1].transform.position.x;
		backGroundElements2Pos = backGroundElements2 [1].transform.position.x;
	}

	void Update () {
		moveObjects (backGroundElements1, speed1, backGroundElements1Pos);
		moveObjects (backGroundElements2, speed2, backGroundElements2Pos);

		if (speed1 < 0) {
			speed1 -= acceleration1 * Time.deltaTime;
			speed2 -= acceleration2 * Time.deltaTime;
		}
	}

	void moveObjects(GameObject[] backgroundElements, float speed, float backGroundElementsPos){
		for (int i = 0; i < backgroundElements.Length; i++) {
			GameObject thisElement = backgroundElements [i];
			Vector3 newPos =	thisElement.transform.position;
			newPos.x -= speed * Time.deltaTime;

            //Debug.Log (i + ": " + newPos.x);
            if (newPos.x < -40f)
            {
                //Debug.Log ("Time to move!");
                newPos.x = backGroundElementsPos;
            }
            else if (newPos.x > 31f) {
                newPos.x = backGroundElementsPos;
            }

			thisElement.transform.position = newPos;
		}
	}

	public void pause(){
		speed1 = 0f;
		speed2 = 0f;
	}

	public void respawn(){
		speed1 = -basicSpeed1;
		speed2 = -basicSpeed2;
	}

	public void moveAgain(){
		speed1 = basicSpeed1;
		speed2 = basicSpeed2;
	}*/
}