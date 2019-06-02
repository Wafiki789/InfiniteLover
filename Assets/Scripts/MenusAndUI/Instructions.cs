using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour {
	void Start () {
		Text text;
		text = GetComponent<Text> ();
		text.text = "Use the space bar to do actions.\n" +
			"Watch the action wheel at the top for what your next actions will be.";
	}
}