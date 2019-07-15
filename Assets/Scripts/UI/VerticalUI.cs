using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalUI : MonoBehaviour
{
    public GameObject attackSymbol;
    public GameObject jumpSymbol;
    public GameObject slideSymbol;
    GameObject currentSymbol;
    GameObject nextSymbol;
    GameObject nextNextSymbol;

    Vector3 position;

    private float distance = 1.75f;

    Level levelScript;

    int activeAction;
    int nextAction;
    int nextNextAction;

    int[] actionWheel;

    public int index = 0;
    int nextIndex;
    int nextNextIndex;

    private float accumulator = 0f;

    private bool changing = false;

    void Start() {
        levelScript = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>();
        getActions();
        changeSymbol();
        StartingPos();
    }

    void getActions() {
        actionWheel = levelScript.giveActions();
    }

    private void FixedUpdate() {
        if (changing) {
            accumulator += 0.1f * Time.deltaTime;

            position = transform.position;
            position.y += 0.1f * Time.deltaTime;
            currentSymbol.transform.position = position;

            position.y += distance + 0.1f * Time.deltaTime;
            nextNextSymbol.transform.position = position;

            position.y += distance + 0.1f * Time.deltaTime;
            nextNextSymbol.transform.position = position;

            if (accumulator >= distance) {
                changing = false;
            }
        }
    }

    public void changeSymbol() {
        activeAction = actionWheel[index];

        if (currentSymbol != null) {
            Destroy(currentSymbol);
        }
        switch (activeAction) {
            case 0:
                currentSymbol = Instantiate(jumpSymbol);
                break;
            case 1:
                currentSymbol = Instantiate(slideSymbol);
                break;
            case 2:
                currentSymbol = Instantiate(attackSymbol);
                break;
        }

        index++;
        if (index == actionWheel.Length) {
            index = 0;
        }

        nextIndex = index;
        nextAction = actionWheel[nextIndex];

        if (nextSymbol != null) {
            Destroy(nextSymbol);
        }
        switch (nextAction) {
            case 0:
                nextSymbol = Instantiate(jumpSymbol);
                break;
            case 1:
                nextSymbol = Instantiate(slideSymbol);
                break;
            case 2:
                nextSymbol = Instantiate(attackSymbol);
                break;
            case 3:
                nextSymbol = Instantiate(attackSymbol);
                break;
        }

        nextIndex++;
        if (nextIndex == actionWheel.Length) {
            nextIndex = 0;
        }

        nextNextIndex = nextIndex;
        nextNextAction = actionWheel[nextNextIndex];

        if (nextNextSymbol != null) {
            Destroy(nextNextSymbol);
        }
        switch (nextNextAction) {
            case 0:
                nextNextSymbol = Instantiate(jumpSymbol);
                break;
            case 1:
                nextNextSymbol = Instantiate(slideSymbol);
                break;
            case 2:
                nextNextSymbol = Instantiate(attackSymbol);
                break;
        }
        //changing = true;
    }

    void StartingPos() {
        position = transform.position;
        currentSymbol.transform.position = position;

        position.y += distance;
        nextSymbol.transform.position = position;

        position.y += distance;
        nextNextSymbol.transform.position = position;
    }
}
