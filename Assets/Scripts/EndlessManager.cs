using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    int lastAction;
    int index;

    int wastedActionsInARow = 0;

    public GameObject gameOverUI;
    float timer = 0f;
    float timeBeforeReset = 2f;

    void Awake() {
        levelScript = level.GetComponent<Level>();
        startingPos = startingAnchor.position;
        actionWheel = levelScript.giveActions();
        activeAction = actionWheel[actionWheel.Length - 1];
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

        this.enabled = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            timer = Time.time;
        }
        else if (Input.GetKey(KeyCode.Space) && Time.time - timer >= timeBeforeReset) {
            SceneManager.LoadScene("JumpSlideInfiniteGym");
        }
        print("Timer" + timer);
    }

    public void setAction() {
        lastAction = activeAction;
        activeAction = actionWheel[index];
        index++;
        if (index == actionWheel.Length) {
            index = 0;
        }
    }

    IEnumerator GenerateObstacle(GameObject obstacle) {
        bool wasted = false;
        float timeAdjustment = 0f;

        float randomNumber = Random.Range(1,100);

        if (randomNumber >= wasteProbs || wastedActionsInARow == actionWheel.Length - 1) {
            Instantiate(obstacle, startingPos, Quaternion.identity, level.transform);
            wastedActionsInARow = 0;
        }
        else {
            wasted = true;
            wastedActionsInARow++;

            //If we have to jump (0) right before a wall (1)
            if (lastAction == 0 && activeAction == 1) {
                print("Jump after wall");
                timeAdjustment = 0.5f;
            }
        }

        if (!wasted) {
            float randomGapTime = Random.Range(0, 100);
            //print(randomGapTime);

            if (randomGapTime < 30) {
                randomGapTime = Random.Range(minTimeBetweenObstacles + timeAdjustment, 1.5f);
            }
            else {
                randomGapTime = Random.Range(minTimeBetweenObstacles + timeAdjustment, maxTimeBetweenObstacles);
            }
            //print(randomGapTime);
            yield return new WaitForSeconds(randomGapTime);
        }
        else {
            yield return new WaitForSeconds(Random.Range(0f, 1f));
        }

        switch (activeAction) {
            case 0: //jump
                setAction();
                StartCoroutine(GenerateObstacle(spike));
                
                break;

            case 1: //slide
                setAction();
                StartCoroutine(GenerateObstacle(wall));
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

    public void GameOver() {
        gameOverUI.SetActive(true);
        this.enabled = true;
        levelScript.speed = 0f;
    }
}