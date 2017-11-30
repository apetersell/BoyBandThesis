using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMusic : MonoBehaviour {



	// Use this for initialization
	void Start () {

		GlobalManager globe = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
		if (globe != null) 
		{
			Destroy (this.gameObject);
		}

//		Screen.SetResolution (1920, 1080, true);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
