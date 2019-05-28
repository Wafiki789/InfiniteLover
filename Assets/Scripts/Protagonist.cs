using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Protagonist : MonoBehaviour {
	public GameObject bullet;
	public GameObject hook;
	int activeAction;

	public int index = 0;

	//Variables for the jump
	public float position;
	public float velocity = 0f;
	public float velocityChange;
	public float gravity;
    public float arc = 0.5f;
    public float friction;
    bool grounded = false;
	Vector3 initialPos;
	float moveAmount;

	public bool test;
	public bool hooked = false;

	bool sliding = false;
	bool pause = false;
	public bool respawning = false;
	public bool goingDown = false;
	bool goingUp = false;
    public bool jumping = true;

	public bool goalReached = false;

	bool minimalJump = false;
	public float minHeight;

	AudioSource[] sounds;
	AudioSource jumpSound;
	AudioSource shootSound;

	GameObject runningSprite;
	GameObject slidingSprite;

	int [] actionWheel;

	GameObject level;
	Level levelScript;
	Symbol symbolScript;
	Goal goalScript;
	ParallaxBackground backgroundScript;

    public LayerMask obstacles;

    private float testTimeJump;

	void Awake () {
		position = transform.position.y;
		initialPos = transform.position;

		sounds = GetComponents<AudioSource> ();
		jumpSound = sounds[0];
		shootSound = sounds[1];
	}

	void Start(){
        level = GameObject.FindGameObjectWithTag("Level");
        levelScript = level.GetComponent<Level>();
        symbolScript = GameObject.Find("Symbol").GetComponent<Symbol>();
		goalScript = GameObject.Find("Goal").GetComponent<Goal>();
		backgroundScript = GameObject.Find ("Background").GetComponent<ParallaxBackground> ();
        getActions ();

		runningSprite = transform.GetChild (0).gameObject;
		slidingSprite = transform.GetChild (1).gameObject;
	}

    void FixedUpdate(){
        if (!respawning && !hooked)
        {
            Vector3 nextPosJump = transform.position;
            position = nextPosJump.y;
            velocity -= gravity * Time.deltaTime;

			//I prefer it with test on, but we'll see!
			//Also, have a friction of 0.98
			if (test && velocity > 0) {
				velocity *= friction;
			} 
			else if (!test) {
				velocity *= friction;
			}

            if (minimalJump && velocity < minHeight)
            {
                minimalJump = false;
                velocity = arc + (minHeight - velocity);
            }

            position += velocity * Time.deltaTime;
            moveAmount = position - nextPosJump.y;
            VerticalCollisions(ref moveAmount);

            if (!grounded)
            {
                nextPosJump.y = position;
                //Debug.Log("Vel " + velocity + " Pos " + position);
            }
            else
            {
                nextPosJump.y += moveAmount;
            }

            transform.position = nextPosJump;
        }
    }

    void Update () {
		if (!goalReached) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				Debug.Log ("Pause");
				if (!pause) {
					Debug.Log ("if !pause");
					pause = true;
					Time.timeScale = 0;
				} else {
					Debug.Log ("else");
					pause = false;
					Time.timeScale = 1;
				}
			} 
			else if (Input.GetKeyDown (KeyCode.R)) {
				SceneManager.LoadScene ("splashScreen");
			}
			else if (Input.GetKeyDown (KeyCode.S)) {
				goalScript.loadNextLevel ();
			}

            //Key pressed: what action is made?
            if (Input.GetKeyDown (KeyCode.Space) && !pause && !respawning) {
				switch (activeAction) {
				case 0: //jump
					if (grounded) {
						//TODO velocityChange
						jump ();
						grounded = false;
						changeAction ();
					}
					break;

				case 1: //slide
					slide();
					changeAction ();
					break;

				case 2: //shoot
					shoot ();
					changeAction ();
					break;

				case 3: //hookshot
					hookShot ();
					changeAction ();
					break;
				}
			}

            //Key released: reset states + make the jump minimal if applicable
			if (Input.GetKeyUp (KeyCode.Space)) {
				runningSprite.SetActive (true);
				slidingSprite.SetActive (false);

				if (velocity > minHeight) {
					Debug.Log ("Min height");
					minimalJump = true;
				}
				else if (!grounded && velocity > 0) {  // If character is still ascending in the jump velocityChange / 2
					//Debug.Log (velocity);
					velocity = arc; // Limit the speed of ascent velocityChange / 2
				} 
				else if (sliding) {
					stopSlide ();
				}
			}

            //Handling the character's vertical positon
			


			if (goingUp && !pause) {
				Vector3 pos = transform.position;
				pos.y += 6f * Time.deltaTime;
				transform.position = pos;
			}
			else if (goingDown && !pause) {
				//Debug.Log ("goingDown");
				Vector3 pos = transform.position;
				position = pos.y;
				position += -6f * Time.deltaTime;
				float unitDowns = position - pos.y;
				VerticalCollisions (ref unitDowns);

				if (unitDowns != position - pos.y) {
					goingDown = false;
					respawning = false;
					levelScript.speed = levelScript.startingSpeed;
					//backgroundScript.moveAgain ();
				}

				if (!grounded) {
					pos.y = position;
				}
				else {
					pos.y += moveAmount;
				}

				pos.y = position;
				transform.position = pos;
			}
		}
	}

	void jump(){
		velocity = velocityChange;
		jumpSound.Play ();
        jumping = true;
        testTimeJump = Time.time;
	}

	void VerticalCollisions(ref float moveAmount){
		float directionY = Mathf.Sign (moveAmount);
        float charHeight = transform.localScale.y / 2;
        float charWidth = transform.localScale.x / 2;
        float rayLength = Mathf.Abs (moveAmount) + charHeight; //+skinWidth

        Vector3[] rayOrigins = new Vector3[3];
        for (int i = 0; i < rayOrigins.Length; i++) {
            rayOrigins[i] = transform.position;
        }
        rayOrigins[0].x -= charWidth;
        rayOrigins[2].x += charWidth;

		//Vector3 rayOrigin = transform.position;
		//rayOrigin.y += (directionY == 1) ? charHeight : -charHeight;
		RaycastHit hit;

        for (int i = 0; i < rayOrigins.Length; i++) {
            Debug.DrawRay(rayOrigins[i], Vector3.up * rayLength * directionY, Color.red);

            if (Physics.Raycast(rayOrigins[i], Vector3.up * directionY, out hit, rayLength, obstacles))
            { //collisionMask
                moveAmount = ((hit.distance - charHeight) * directionY); //(hit.distance - skinWidth)
                rayLength = hit.distance;
                grounded = true;
                velocity = 0;
                if (jumping) {
                    jumping = false;
                    Debug.Log(Time.time - testTimeJump);
                }
            }
            else if (grounded && jumping)
            {
                velocity = 0f;
                moveAmount = 0f;
                jumping = false;
            }
            else {
                grounded = false;
            }
        }
	}


	//TODO merge slide and stopSlide
	void slide(){
		runningSprite.SetActive (false);
		slidingSprite.SetActive (true);
		Vector3 size = transform.localScale;
		Vector3 nextPos = transform.position;
		size.y = size.y / 2;
		nextPos.y -= size.y/2;
		transform.localScale = size;
		transform.position = nextPos;
		sliding = true;
	}

	void stopSlide(){
		Vector3 nextPos = transform.position;
		Vector3 size = transform.localScale;
		nextPos.y += size.y / 2;
		size.y = size.y * 2;
		transform.position = nextPos;
		transform.localScale = size;
		sliding = false;
	}

	void shoot(){
        Vector3 bulletPos = transform.position;
        bulletPos.x += transform.localScale.x / 2 + bullet.transform.localScale.x / 2;
        Instantiate (bullet, bulletPos, new Quaternion(0,0,0,0));
		shootSound.Play ();
	}

	void hookShot(){
		Vector3 hookPos = transform.position;
		hookPos.x += transform.localScale.x / 2 + hook.transform.localScale.x / 2;
		Instantiate (hook, hookPos, new Quaternion(0,0,0,0));
		//Todo: Change it to another sound
		shootSound.Play ();
	}

	public void changeAction(){
        setAction();
		symbolScript.changeSymbol ();
	}

	public void respawn(){
		float speedTemp = levelScript.speed;
		levelScript.speed = 0f;
		respawning = true;
		goingUp = true;
		velocity = 0;
		grounded = false;
		//backgroundScript.pause ();
		Invoke ("rewind", 1f);

	}

	void rewind(){
		goingUp = false;
		levelScript.rewind ();
		//backgroundScript.respawn ();
	}

	public void getActions(){
		actionWheel = levelScript.giveActions ();
        setAction();
    }

    public void setAction() {
        activeAction = actionWheel[index];
        index++;
        if (index == actionWheel.Length)
        {
            index = 0;
        }
    }
}




//Cemetery

/*



//Cancel jumping when reaching the ground again.

/*void 
 * 
 * 
 * OnCollisionEnter(Collision coll){
		Debug.Log ("Collision");
		GameObject floor = coll.gameObject;
		Vector3 nextPos = initialPos;
		transform.position = nextPos;

		velocity = 0f;
		jumping = false;
	}


*/




//Resources:

/*
Jump implementation (with longer): https://gamedev.stackexchange.com/questions/29617/how-to-make-a-character-jump
Raycast: https://answers.unity.com/questions/196381/how-do-i-check-if-my-rigidbody-player-is-grounded.html
AnimationCurve: https://docs.unity3d.com/ScriptReference/AnimationCurve.html
Collision detection: http://higherorderfun.com/blog/2012/05/20/the-guide-to-implementing-2d-platformers/
https://katyscode.wordpress.com/2013/01/18/2d-platform-games-collision-detection-for-dummies/

*/