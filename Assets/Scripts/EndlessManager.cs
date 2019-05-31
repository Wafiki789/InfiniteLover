using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessManager : MonoBehaviour {
    //Temporary: we'll randomly generate the actions at the beginning, maybe even change them along the way
    
    public GameObject level;
    Level levelScript;

    //Prefabs
    public GameObject spike;
    public GameObject wall;
    //public GameObject boulder;

    //Probabilities
    public float wasteProbs;
    public float minTimeBetweenObstacles;
    public float maxTimeBetweenObstacles;

    public Transform startingAnchor;
    private Vector3 startingPos;

    int[] actionWheel;
    int activeAction;
    int index;

    //9 -3
    void Awake() {
        levelScript = level.GetComponent<Level>();
        startingPos = startingAnchor.position;
        actionWheel = levelScript.giveActions();
        setAction();

        switch (activeAction) {
            case 0: //jump
                StartCoroutine(GenerateObstacle(spike));
                setAction();
                break;

            case 1: //slide
                StartCoroutine(GenerateObstacle(wall));
                setAction();
                break;

                /*case 2: //shoot
                    shoot();
                    setAction();
                    break;

                case 3: //hookshot
                    hookShot();
                    setAction();
                    break;*/
        }
    }

    public void setAction() {
        activeAction = actionWheel[index];
        index++;
        if (index == actionWheel.Length) {
            index = 0;
        }
    }

    IEnumerator GenerateObstacle(GameObject obstacle) {
        bool wasted = false;

        float randomNumber = Random.Range(1,100);

        if (randomNumber >= wasteProbs) {
            Instantiate(obstacle, startingPos, Quaternion.identity, level.transform);
        }
        else {
            wasted = true;
            Debug.Log("Wasted!");
        }

        if (!wasted) {
            float randomGapTime = Random.Range(0, 100);
            print(randomGapTime);

            if (randomGapTime < 30) {
                randomGapTime = Random.Range(minTimeBetweenObstacles, 1.5f);
            }
            else {
                randomGapTime = Random.Range(minTimeBetweenObstacles, maxTimeBetweenObstacles);
            }
            print(randomGapTime);
            yield return new WaitForSeconds(randomGapTime);
        }
        else {
            yield return new WaitForSeconds(Random.Range(0f, 1f));
        }

        switch (activeAction) {
            case 0: //jump
                StartCoroutine(GenerateObstacle(spike));
                setAction();
                break;

            case 1: //slide
                StartCoroutine(GenerateObstacle(wall));
                setAction();
                break;

                /*case 2: //shoot
                    shoot();
                    setAction();
                    break;

                case 3: //hookshot
                    hookShot();
                    setAction();
                    break;*/
        }
    }
}