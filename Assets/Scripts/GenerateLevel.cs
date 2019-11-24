﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject boulder;
    public GameObject wall;
    public GameObject spike;
    public GameObject animal;
    public GameObject checkPoint;
    public GameObject goal;
    public GameObject level;
    public GameObject platform;
    public GameObject floatyPlatform;

    [TextArea(10,10)]
    public string levelString;

    private float xPos = 11;
    private float originalyPos = -4;
    private float yPos;
    Vector3 spawnPos;

    private float lastXPos = 0;
    private float lastYPos = 0;

    bool isOnPlatform = false;

    float platformCounter;

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
            else if (levelString[i] == 'a') {
                Instantiate(animal, spawnPos, Quaternion.identity, level.transform);
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
                Instantiate(platform, spawnPos, Quaternion.identity, level.transform);
                //yPos += platform.transform.localScale.y;
                yPos += 2;
                isOnPlatform = true;
                //platformCounter = (int)platform.transform.localScale.x;
                platformCounter = 10;
                spawnPos = new Vector3(xPos, yPos, 0);

            }
            else if (levelString[i] == 'f') {
                Instantiate(floatyPlatform, spawnPos, Quaternion.identity, level.transform);
                yPos += 2;
                isOnPlatform = true;
                platformCounter = 1;
                spawnPos = new Vector3(xPos, yPos, 0);
                lastXPos = xPos;
            }
            else if (char.IsDigit(levelString, i)) {
                int digits = 1;
                for (int j = 0; j < levelString.Length; j++) {
                    if (char.IsDigit(levelString, i + j + 1)) {
                        digits++;
                    }
                    else {
                        break;
                    }
                }

                int objectAmount = int.Parse(levelString.Substring(i, digits));

                GameObject objectType = null;
                float distanceBetweenObjects = 0.5f;

                bool isAPlatform = false;

                if (levelString[i + digits] == 's') {
                    objectType = spike;
                }
                else if (levelString[i + digits] == 'w') {
                    objectType = wall;
                }
                else if (levelString[i + digits] == 'p') {
                    objectType = platform;
                    distanceBetweenObjects = 1;
                    isAPlatform = true;
                }
                else if (levelString[i + digits] == 'f') {
                    objectType = floatyPlatform;
                    distanceBetweenObjects = 1;
                    isAPlatform = true;
                    lastXPos = xPos;
                }

                float xOrigin = spawnPos.x;

                if (isAPlatform) {
                    for (int j = 0; j < objectAmount; j++) {
                        Instantiate(objectType, spawnPos, Quaternion.identity, level.transform);
                        xPos += distanceBetweenObjects;
                        spawnPos = new Vector3(xPos, yPos, 0);
                    }

                    //platformCounter = objectAmount;
                    platformCounter = objectAmount;
                    isOnPlatform = true;
                    yPos += 2;
                    xPos = xOrigin;
                    lastXPos = xOrigin;
                    spawnPos = new Vector3(xPos, yPos, 0);
                }
                else {
                    for (int j = 0; j < objectAmount; j++) {
                        Instantiate(objectType, spawnPos, Quaternion.identity, level.transform);
                        PushXForward(distanceBetweenObjects);
                    }
                }

                i += digits;
            }
            else if (levelString[i] == '[') {
                lastYPos = yPos;
                yPos = originalyPos;
                xPos = lastXPos;
                spawnPos = new Vector3(xPos, yPos, 0);
            }
            else if (levelString[i] == ']') {
                yPos = lastYPos;
                spawnPos = new Vector3(xPos, yPos, 0);
            }
            print(levelString[i] + " height " + yPos);
        }
    }

    void PushXForward(float xUnits) {
        xPos += xUnits;

        if (isOnPlatform) {
            platformCounter -= xUnits;
            if (platformCounter <= 0) {
                isOnPlatform = false;
                yPos = originalyPos;
            }
        }
        spawnPos = new Vector3(xPos, yPos, 0);
    }
}