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

    [TextArea(10,10)]
    public string levelString;

    

    private float xPos = 11;
    Vector3 spawnPos;

    void Awake()
    {
        spawnPos = new Vector3(xPos, -3, 0);
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
                xPos += 1;
            }
            else if (levelString[i] == '.') {
                xPos += 4;
            }
            else if (levelString[i] == ';') {
                xPos += 2;
            }
            else if (levelString[i] == 'c') {
                Instantiate(checkPoint, spawnPos, Quaternion.identity, level.transform);
            }
            else if (levelString[i] == 'g') {
                Instantiate(goal, spawnPos, Quaternion.identity, level.transform);
            }


            spawnPos = new Vector3(xPos, -3, 0);
        }
    }
}