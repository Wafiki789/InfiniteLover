using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symbol : MonoBehaviour {
	public GameObject attackSymbol;
	public GameObject jumpSymbol;
	public GameObject slideSymbol;
	GameObject currentSymbol;
	GameObject nextSymbol;
	GameObject nextNextSymbol;

	Vector3 position;
    Vector3 scale;

    private float distance = 1.75f;
    private float scaling = 1.75f;

    Level levelScript;

	int activeAction;
	int nextAction;
	int nextNextAction;

	int [] actionWheel;

	public int index = 0;
	int nextIndex;
	int nextNextIndex;

	void Start () {
		levelScript = GameObject.FindGameObjectWithTag ("Level").GetComponent<Level>();
		getActions ();
        changeSymbol();
	}

	void getActions(){
		actionWheel = levelScript.giveActions ();
	}

	public void changeSymbol(){
		activeAction = actionWheel [index];

		if (currentSymbol != null) {
			Destroy (currentSymbol);
		}
		switch (activeAction) {
		case 0:
			currentSymbol = Instantiate (jumpSymbol);
			break;
		case 1:
			currentSymbol = Instantiate (slideSymbol);
			break;
		case 2:
			currentSymbol = 	Instantiate (attackSymbol);
			break;
		}

		position = transform.position;
        scale = transform.localScale;
		currentSymbol.transform.position = position;

		index++;
		if (index == actionWheel.Length) {
			index = 0;
		}

		nextIndex = index;
		nextAction = actionWheel[nextIndex];

		if (nextSymbol != null) {
			Destroy (nextSymbol);
		}
		switch (nextAction) {
		case 0:
			nextSymbol = Instantiate (jumpSymbol);
			break;
		case 1:
			nextSymbol = Instantiate (slideSymbol);
			break;
		case 2:
			nextSymbol = Instantiate (attackSymbol);
			break;
		case 3:
			nextSymbol = Instantiate (attackSymbol);
			break;
		}

		position.x += distance;
        scale /= scaling;
		nextSymbol.transform.position = position;
        nextSymbol.transform.localScale = scale;

		nextIndex++;
		if (nextIndex == actionWheel.Length) {
			nextIndex = 0;
		}

		nextNextIndex = nextIndex;
		nextNextAction = actionWheel [nextNextIndex];

		if (nextNextSymbol != null) {
			Destroy (nextNextSymbol);
		}
		switch (nextNextAction) {
		case 0:
			nextNextSymbol = Instantiate (jumpSymbol);
			break;
		case 1:
			nextNextSymbol = Instantiate (slideSymbol);
			break;
		case 2:
			nextNextSymbol = 	Instantiate (attackSymbol);
			break;
		}

		position.x += distance;
        scale /= scaling;
		nextNextSymbol.transform.position = position;
        nextNextSymbol.transform.localScale = scale;
	}
}