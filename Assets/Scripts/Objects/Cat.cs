using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {
    Protagonist protagonistScript;
	Vector3 initialPos;
	Vector3 dummyPos;
	GameObject catModel;

    void Start()
    {
        protagonistScript = GameObject.Find("Protagonist").GetComponent<Protagonist>();
		catModel = transform.GetChild (0).transform.gameObject;
    }

    void OnTriggerEnter(Collider coll)
    {
        string gameObjectName = coll.gameObject.name;
        if (gameObjectName.Contains("Bullet"))
        {
            Destroy(coll.gameObject);
			catModel.SetActive (false);
            Invoke("makeVisible", 1f);
            gameOver();
        }
    }

    void makeVisible()
    {
		catModel.SetActive (true);
    }

    void gameOver()
    {
        protagonistScript.respawn();
    }
}
