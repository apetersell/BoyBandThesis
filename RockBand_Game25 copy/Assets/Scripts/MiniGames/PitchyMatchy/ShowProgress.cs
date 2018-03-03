using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowProgress : MonoBehaviour {
	//public Slider slider;
	public Text text;
	GlobalManager globe;

	// Use this for initialization
	void Start () {
		globe = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
	}
	
	// Update is called once per frame
	void Update () {
		if(globe){
			text.text = globe.timeLeft.ToString();
			
		}
		//slider.value = globe.timeLeft
	}
}
