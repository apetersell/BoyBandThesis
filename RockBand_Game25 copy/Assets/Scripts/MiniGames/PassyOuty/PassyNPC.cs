using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassyNPC : MonoBehaviour {

	public Sprite[] sprites;
	public bool active = true;
	public bool hasFlier;
	SpriteRenderer sr;
	ScoreManager sm;
	public Vector3 startPos;
	public Vector3 endPos; 
	public float speed;
	float lerpPercent;
	public GameObject exclaim;

	// Use this for initialization
	void Start () {

		sr = GetComponent<SpriteRenderer> ();
		sm = GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ();
		exclaim = transform.GetChild (0).gameObject;
		exclaim.SetActive (false);
		
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
		sr.sprite = sprites [0];
		transform.position = startPos;
	}
		
	public void gotFlier (bool got)
	{
		active = false;
		if (got) {
			sr.sprite = sprites [1];
			hasFlier = true;
			sm.scorePoints (true);
			transform.GetChild (1).gameObject.GetComponent<HitEffect> ().active = true;
		} else {
			sr.sprite = sprites [2];
			sm.scorePoints (false);
		}
	}

	public void seePlayer (bool sees)
	{
		exclaim.SetActive (sees);
	}
}
