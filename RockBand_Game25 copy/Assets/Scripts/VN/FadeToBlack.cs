using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Yarn.Unity;
using UnityEngine.UI; 

public class FadeToBlack : MonoBehaviour 
{
	public Color alphadOut;
	public float tweenSpeed;
	public DialogueRunner d;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[YarnCommand("fadeIn")]
	public void fadeIn()
	{
		GetComponent<Image> ().DOFade (0, tweenSpeed).OnComplete(turnOffFading);
		d.fading = true;
	}

	[YarnCommand("fadeOut")]
	public void fadeOut()
	{
		GetComponent<Image> ().DOFade (255, tweenSpeed).OnComplete(turnOffFading);
		d.fading = true;
	}

	void turnOffFading()
	{
		d.fading = false;
	}
}
