using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
    [Range(1,10)]
	public float startingSpeed;
    [HideInInspector]
	public float speed;

	float acceleration = 30;
	Vector3 pos;
	public action[] actionWheel;

	public enum action{
		jump,
		slide,
		shoot,
		hook
	}

	void Awake () {
		pos = transform.position;
		speed = startingSpeed;
	}

	void FixedUpdate () {
		pos.x -= speed * Time.deltaTime;
		transform.position = pos ;
		if (speed < 0) {
			speed -= acceleration * Time.deltaTime;
		}
	}

	public int[] giveActions(){
		int[] actions = new int[actionWheel.Length];
		for (int i = 0; i < actionWheel.Length; i++) {
			actions [i] = (int)actionWheel [i];
		}
		return actions;
	}

	public void rewind(){
		speed = -30;
        //TODO: Reactivate boulders and next/previous/reverse actions
	}
}