using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerformanceFanDisplay : MonoBehaviour 
{
	Image img;
	public Text display;
	GlobalManager globe;
	// Use this for initialization
	void Start () 
	{
		globe = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
		img = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		img.fillAmount = (globe.AigFans / globe.numberOfFansRequired);
		display.text = "Fans: " + Mathf.Round(globe.AigFans).ToString ();
	}
}
