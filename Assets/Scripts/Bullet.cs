using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public float speed = 10f;
	public float expiration = 0.2f;
	Vector3 pos;
    private bool firstFrame = true;

	void Update () {
        if (firstFrame){
            firstFrame = false;
            //Invoke("getDestroyed", expiration);
            pos = transform.position;
            pos.x += transform.localScale.x;
        }
        else{
            pos.x += speed * Time.deltaTime;
        }

        transform.position = pos;
    }

	//void getDestroyed(){
		//Destroy (this.gameObject);
	//}
}