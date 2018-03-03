using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightDisplay : MonoBehaviour {

	public Sprite day;
	public Sprite night;
	GlobalManager gm;
	Image img;

	// Use this for initialization
	void Start () 
	{
		gm = GameObject.Find ("GlobalStats").GetComponent<GlobalManager> ();
		img = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gm.night) {
			img.sprite = night;
		} else {
			img.sprite = day;
		}
	}
}
