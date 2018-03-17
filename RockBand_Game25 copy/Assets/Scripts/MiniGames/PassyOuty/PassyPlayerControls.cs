using System.Collections; 
using System.Collections.Generic; 
using UnityEngine;

public class PassyPlayerControls : MonoBehaviour {

	public float activeFrames;
	public float activeTimer;
	bool playerActive;
	Detector detectBoxL; 
	Detector detectBoxR;
	Animator anim;
	bool hit;

	void Awake ()
	{
		activeTimer = activeFrames;
		detectBoxL = GameObject.Find ("LeftDetector").GetComponent<Detector>(); 
		detectBoxR = GameObject.Find ("RightDetector").GetComponent<Detector> ();
		anim = GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () 
	{

		
	}
	
	// Update is called once per frame
	void Update () 
	{

		controls ();
		anim.SetBool ("Hit", hit);
		
	}

	void controls () 
	{

		if (playerActive) 
		{
			activeTimer--;
		} 
		else 
		{
			if (Input.GetKeyDown(KeyCode.RightArrow)) 
			{
				setPlayerActive (1);
			}
			if (Input.GetKeyDown (KeyCode.LeftArrow)) 
			{
				setPlayerActive (-1);
			}
		}

		if (activeTimer <= 0) 
		{
			playerActive = false;
			activeTimer = activeFrames;
		}
	}

	void setPlayerActive (int dir)
	{
		playerActive = true;
		if (dir == 1) 
		{
			anim.SetTrigger ("Right");
			if (detectBoxR.NPC != null) {
				PassyNPC p = detectBoxR.NPC.GetComponent<PassyNPC> ();
				if (p.active) {
					p.gotFlier (true);
					hit = true;
				} else {
					hit = false;
				}
			} else {
				hit = false;
			}
		}
		if (dir == -1) 
		{
			anim.SetTrigger ("Left");
			if (detectBoxL.NPC != null) {
				PassyNPC p = detectBoxL.NPC.GetComponent<PassyNPC> ();
				if (p.active) {
					p.gotFlier (true);
					hit = true;
				} else {
					hit = false;
				}
			} else {
				hit = false;
			}
		}
	}
}
