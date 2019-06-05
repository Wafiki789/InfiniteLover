using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject boulder;
    public GameObject wall;
    public GameObject spike;
    public GameObject checkPoint;
    public GameObject goal;
    public GameObject level;
    public GameObject platform;

    [TextArea(10,10)]
    public string levelString;

    private float xPos = 11;
    private float originalyPos = -4;
    private float yPos;
    Vector3 spawnPos;

    bool isOnPlatform = false;

    int platformCounter;

    void Awake()
    {
        yPos = originalyPos;
        spawnPos = new Vector3(xPos, yPos, 0);
        for (int i = 0; i < levelString.Length; i++) {
            if (levelString[i] == 's') {
                Instantiate(spike, spawnPos, Quaternion.identity, level.transform);
            }
            else if (levelString[i] == 'w') {
                Instantiate(wall, spawnPos, Quaternion.identity, level.transform);
            }
            else if (levelString[i] == 'b') {
                Instantiate(boulder, spawnPos, Quaternion.identity, level.transform);
            }
            else if (levelString[i] == ',') {
                PushXForward(1);
            }
            else if (levelString[i] == '.') {
                PushXForward(4);
            }
            else if (levelString[i] == ';') {
                PushXForward(2);
            }
            else if (levelString[i] == 'c') {
                Instantiate(checkPoint, spawnPos, Quaternion.identity, level.transform);
            }
            else if (levelString[i] == 'g') {
                Instantiate(goal, spawnPos, Quaternion.identity, level.transform);
            }
            else if (levelString[i] == 'p') {

                /*Branching platforms are an edge-case and could be done manually?
                 For regular platforms, to adapt the height for the obstacles, maybe put a OnTriggerEnter function on them
                 and uplift them when they collide.

                After xPos has increased by...

                [] -> end of beginning of alternate platform
                 */


                //Vector3 platformSpawnPos = new Vector3(xPos + (platform.transform.localScale.x / 2) -0.5f, yPos, 0);

                Instantiate(platform, spawnPos, Quaternion.identity, level.transform);
                //yPos += platform.transform.localScale.y;
                yPos += 2;
                isOnPlatform = true;
                //platformCounter = (int)platform.transform.localScale.x;
                platformCounter = 10;
            }


            spawnPos = new Vector3(xPos, yPos, 0);
        }
    }

    void PushXForward(int xUnits) {
        xPos += xUnits;

        if (isOnPlatform) {
            platformCounter -= xUnits;
            if (platformCounter <= 0) {
                isOnPlatform = false;
                yPos = originalyPos;
            }
        }
    }
}