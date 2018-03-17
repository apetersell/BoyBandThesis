using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandFan : MonoBehaviour 
{
	Animator anim;
	public bool JPe;
	GlobalManager globe;
	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();
		globe = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (JPe) {
			anim.SetBool ("Loving", globe.JPPresent);
		} else {
			anim.SetBool ("Loving", globe.LeePresent);
		}
	}
}
