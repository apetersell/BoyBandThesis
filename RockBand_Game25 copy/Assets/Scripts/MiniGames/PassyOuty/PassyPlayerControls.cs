using System.Collections; 
using System.Collections.Generic; 
using UnityEngine;

public class PassyPlayerControls : MonoBehaviour {

	public Sprite[] sprites; 
	SpriteRenderer sr; 
	float startingScaleX;
	public float activeFrames;
	public float activeTimer;
	bool playerActive;
	Detector detectBoxL; 
	Detector detectBoxR;

	void Awake ()
	{
		startingScaleX = transform.localScale.x;
		activeTimer = activeFrames;
		detectBoxL = GameObject.Find ("LeftDetector").GetComponent<Detector>(); 
		detectBoxR = GameObject.Find ("RightDetector").GetComponent<Detector> ();

	}

	// Use this for initialization
	void Start () {

		sr = GetComponent<SpriteRenderer> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		controls ();
		
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
			sr.sprite = sprites [0];
		}
	}

	void setPlayerActive (int dir)
	{
		playerActive = true;
		if (dir == 1) 
		{
			sr.sprite = sprites [1];
			if (detectBoxR.NPC != null) 
			{
				PassyNPC p = detectBoxR.NPC.GetComponent<PassyNPC> ();
				if (p.active) 
				{
					p.gotFlier (true);
					sr.sprite = sprites [2];
				}
			}
		}
		if (dir == -1) 
		{
			sr.sprite = sprites [3];
			if (detectBoxL.NPC != null) 
			{
				PassyNPC p = detectBoxL.NPC.GetComponent<PassyNPC> ();
				if (p.active) 
				{
					p.gotFlier (true);
					sr.sprite = sprites [4];
				}
			}
		}
	}
}
