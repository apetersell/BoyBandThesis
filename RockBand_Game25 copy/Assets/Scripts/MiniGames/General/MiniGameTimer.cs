using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameTimer : MonoBehaviour {

	GlobalManager globe;

	// Use this for initialization
	void Start () {

		globe = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (globe != null) 
		{
			GetComponent<Image> ().fillAmount = globe.timeLeft / globe.maxGameTimer;
		}
	}
}
