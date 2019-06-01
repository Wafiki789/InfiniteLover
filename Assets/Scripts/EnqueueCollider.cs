using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnqueueCollider : MonoBehaviour
{
    public EndlessManager endlessScript;
    void OnTriggerEnter(Collider coll) {
        endlessScript.EnqueueObstacle(coll.gameObject);
        print(coll.tag + coll.name);
    }
}