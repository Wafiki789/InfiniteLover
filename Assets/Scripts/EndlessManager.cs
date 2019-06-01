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


    GameObject[] spikesArray;
    GameObject[] wallsArray;

    Queue<GameObject> spikes;
    Queue<GameObject> walls;

    //Probabilities
    public float wasteProbs;
    public float minTimeBetweenObstacles;
    public float maxTimeBetweenObstacles;
    public float smallerWallsProbs;
    public float smallerSpikesProbs;

    public float smallGapTime;
    public float mediumGapTime;

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

    private bool pressedSpace;

    void Awake() {
        spikesArray = GameObject.FindGameObjectsWithTag("Spike");
        wallsArray = GameObject.FindGameObjectsWithTag("Wall");

        spikes = new Queue<GameObject>();
        walls = new Queue<GameObject>();

        for (int i = 0; i < spikesArray.Length; i++) {
            spikes.Enqueue(spikesArray[i]);
        }
        for (int i = 0; i < wallsArray.Length; i++) {
            walls.Enqueue(wallsArray[i]);
        }

        levelScript = level.GetComponent<Level>();
        startingPos = startingAnchor.position;
        actionWheel = levelScript.giveActions();
        for (int i = 0; i < actionWheel.Length; i++) {
            print(actionWheel[i]);
        }

        activeAction = actionWheel[actionWheel.Length - 1];
        setAction();

        SwitchAction();

        this.enabled = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            timer = Time.time;
            pressedSpace = true;
        }
        else if (pressedSpace && Input.GetKey(KeyCode.Space) && Time.time - timer >= timeBeforeReset) {
            SceneManager.LoadScene("JumpSlideInfiniteGym");
        }
    }

    public void setAction() {
        lastAction = activeAction;
        activeAction = actionWheel[index];
        index++;
        if (index == actionWheel.Length) {
            index = 0;
        }
    }

    void SwitchAction() {
        switch (activeAction) {
            case 0: //jump
                setAction();
                StartCoroutine(GenerateObstacle(spikes));

                break;

            case 1: //slide
                setAction();
                StartCoroutine(GenerateObstacle(walls));
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

    IEnumerator GenerateObstacle(Queue<GameObject> obstacles) {
        bool wasted = false;
        float timeAdjustment = 0f;

        float randomNumber = Random.Range(1,100);

        if (randomNumber >= wasteProbs || wastedActionsInARow == actionWheel.Length - 1) {
            //Instantiate(obstacle, startingPos, Quaternion.identity);
            if (obstacles.Count != 0) {
                GameObject currentObstacle = obstacles.Dequeue();
                currentObstacle.transform.position = startingPos;

                if (currentObstacle.tag == "Wall") {
                    int wallLength = EnlargeObstacle(currentObstacle, obstacles, smallerWallsProbs, 4, 17);
                    timeAdjustment += (wallLength - 1) * 0.09f;
                }
                else if (currentObstacle.tag == "Spike") {
                    if (Random.Range(0, 4) > 0) {
                        int spikeLength = EnlargeObstacle(currentObstacle, obstacles, smallerSpikesProbs, 3, 7);
                        timeAdjustment += (spikeLength - 1) * 0.3f;

                        print(spikeLength + " " + timeAdjustment);
                    }
                    //(GameObject obstacle, Queue<GameObject> obstacles, float smallerProbs, int max1, int max2)
                }
            }
            else {
                Debug.LogError("Empty Queue!");
            }
            wastedActionsInARow = 0;
        }
        else {
            wasted = true;
            wastedActionsInARow++;

            //If we have to jump (0) right before a wall (1)
            if (lastAction == 0 && activeAction == 1) {
                timeAdjustment = 0.6f;
            }
        }

        if (!wasted) {
            float randomGapTime = Random.Range(0, 100);

            if (randomGapTime < smallGapTime) {
                if (timeAdjustment + minTimeBetweenObstacles > 1) {
                    randomGapTime = Random.Range(minTimeBetweenObstacles + timeAdjustment, 1f + timeAdjustment);
                }
                else {
                    randomGapTime = Random.Range(minTimeBetweenObstacles + timeAdjustment, 1f);
                }
            }
            else if (randomGapTime < mediumGapTime) {
                if (timeAdjustment + minTimeBetweenObstacles > 1.5f) {
                    randomGapTime = Random.Range(minTimeBetweenObstacles + timeAdjustment, 1.5f + timeAdjustment);
                }
                else {
                    randomGapTime = Random.Range(minTimeBetweenObstacles + timeAdjustment, 1.5f);
                }
            }
            else {
                if (timeAdjustment + minTimeBetweenObstacles > maxTimeBetweenObstacles) {
                    randomGapTime = Random.Range(minTimeBetweenObstacles + timeAdjustment, maxTimeBetweenObstacles + timeAdjustment);
                }
                else {
                    randomGapTime = Random.Range(minTimeBetweenObstacles + timeAdjustment, maxTimeBetweenObstacles);
                }
            }
            print(timeAdjustment + " " + randomGapTime);
            yield return new WaitForSeconds(randomGapTime);
        }
        else {
            yield return new WaitForSeconds(Random.Range(0f, 1f));
        }

        SwitchAction();
    }

    int EnlargeObstacle(GameObject obstacle, Queue<GameObject> obstacles, float smallerProbs, int max1, int max2) {
        float smallerObstacle = Random.Range(0, 100);
        int obstacleLength = 1;

        if (smallerObstacle < smallerProbs) {
            obstacleLength = Random.Range(1, max1);
        }
        else {
            obstacleLength = Random.Range(1, max2);
        }

        float scaleX = obstacle.transform.localScale.x;
        Vector3 nextPos = startingPos;
        nextPos.x += scaleX;

        for (int i = 0; i < obstacleLength - 1; i++) {
            obstacle = obstacles.Dequeue();
            obstacle.transform.position = nextPos;
            nextPos.x += scaleX;
        }

        return obstacleLength;
    }

    public void EnqueueObstacle(GameObject obstacle) {
        if (obstacle.tag == "Spike") {
            spikes.Enqueue(obstacle);
        }
        else if (obstacle.tag == "Wall") {
            walls.Enqueue(obstacle);
        }
    }

    public void GameOver() {
        gameOverUI.SetActive(true);
        this.enabled = true;
        levelScript.speed = 0f;
        StopAllCoroutines();
    }
}