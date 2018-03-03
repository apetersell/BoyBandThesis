using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class Background : MonoBehaviour {

	public Sprite[] backgrounds; 
	Image img;

	// Use this for initialization
	void Start () 
	{
		img = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[YarnCommand("BG")]
	public void changeBackground(string name)
	{
		if (name == "Dorms") 
		{
			img.sprite = backgrounds [0];
		}
		if (name == "Backstage") 
		{
			img.sprite = backgrounds [1];
		}
		if (name == "OnStage") 
		{
			img.sprite = backgrounds [2];
		}
	}
		

}
