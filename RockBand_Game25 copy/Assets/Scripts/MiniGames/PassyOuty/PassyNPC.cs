using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassyNPC : MonoBehaviour {

	public bool active = true;
	public bool hasFlier;
	SpriteRenderer sr;
	ScoreManager sm;
	public Vector3 startPos;
	public Vector3 endPos; 
	public float speed;
	float lerpPercent;
	Animator anim;

	// Use this for initialization
	void Start () {

		sr = GetComponent<SpriteRenderer> ();
		sm = GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ();
		anim = GetComponent<Animator> ();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		movement ();
	}

	void movement ()
	{
		lerpPercent += Time.deltaTime/speed;
		transform.position = Vector3.Lerp(startPos, endPos, lerpPercent);

		if (transform.position == endPos) 
		{
			Destroy (this.gameObject);
		}
	}

	void reset () 
	{
		active = true;
		transform.position = startPos;
	}
		
	public void gotFlier (bool got)
	{
		active = false;
		if (got) {
			hasFlier = true;
			sm.scorePoints (true);
			anim.SetTrigger ("Hit");
		} else {
			sm.scorePoints (false);
			anim.SetTrigger ("Miss");
		}
	}

	public void seePlayer (bool sees)
	{
		anim.SetTrigger ("SeePlayer");
	}
}
