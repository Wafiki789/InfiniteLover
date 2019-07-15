using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUI : MonoBehaviour
{
    public float speed;
    public bool isChanging;

    void Update() {
        transform.RotateAround(transform.position, Vector3.forward, speed * Time.deltaTime);

        /*if (Quaternion.Angle(transform.rotation, closingRotation.transform.rotation) <= 0.01f && !opening) {
            print("ChangeSpeed");
            speed *= -1;
            opening = !opening;
        }*/
    }
}