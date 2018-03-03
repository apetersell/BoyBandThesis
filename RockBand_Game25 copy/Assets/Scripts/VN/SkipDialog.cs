using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipDialog : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			GlobalManager gm = FindObjectOfType(typeof(GlobalManager)) as GlobalManager;
			if(gm){
				//Camera.main.
				gm.myState = PlayerState.timescheduling;
				SceneManager.LoadScene("Main");
			}else{
				SceneManager.LoadScene("Main");
			}

		}
	}
}
