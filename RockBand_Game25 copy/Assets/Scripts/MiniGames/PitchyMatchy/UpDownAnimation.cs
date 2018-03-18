using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownAnimation : MonoBehaviour 
{
	Transform tracker;
	public float highThresh; 
	public float lowThresh;
	Animator anim;
	bool Up;
	bool Down;
	bool coughing;
	public Animator [] musicNotes;
	public AudioClip coughSound; 
	AudioSource auds;

	// Use this for initialization
	void Start () 
	{
		tracker = GameObject.Find ("Point").transform;
		anim = GetComponent<Animator> ();
		auds = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		anim.SetBool ("Up", Up);
		anim.SetBool ("Down", Down);
		if (tracker.position.y >= highThresh) 
		{
			Up = true;
			Down = false;
		} else if (tracker.position.y < highThresh && tracker.position.y > lowThresh) 
		{
			Up = false;
			Down = false;
		} else {
			Up = false;
			Down = true;
		}

		foreach (Animator anim in musicNotes) 
		{
			anim.SetBool ("Coughing", coughing);
		}
	}

	public void doCough()
	{
		if (!coughing) 
		{
			coughing = true;
			anim.SetTrigger ("Cough");
			auds.PlayOneShot (coughSound);
		}
	}

	public void stopCough ()
	{
		coughing = false;
	}
}
