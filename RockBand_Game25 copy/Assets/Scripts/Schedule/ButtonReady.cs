using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonReady : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Image>().enabled = false;
	}

	void showBtnReady(){
		GetComponent<Image>().enabled = true;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
