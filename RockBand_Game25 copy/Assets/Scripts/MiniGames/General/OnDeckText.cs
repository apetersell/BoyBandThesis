using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnDeckText : MonoBehaviour {

	GlobalManager globe;

	// Use this for initialization
	void Start () 
	{

		globe = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
		if (globe != null) 
		{
			GetComponent<Text>().text = "Next: " + globe.NextActivity;
		}
	}
	// Update is called once per frame
	void Update () 
	{

	}
}
