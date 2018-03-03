using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressyUppyControls : MonoBehaviour 
{
	public bool hidden;
	public Sprite[] outfits;
	public int outfitIndex;
	SpriteRenderer sr;
	public bool facingRight;
	public GameObject hiddenUI;
	public GameObject exposedUI;


	// Use this for initialization
	void Start () 
	{
		sr = GetComponent<SpriteRenderer> ();
		facingRight = true;
		hidden = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		sr.sprite = outfits [outfitIndex];
		sr.flipX = facingRight;
		controls ();
		handleHiding ();
	}

	void controls ()
	{
		if (hidden) {
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				outfitIndex = 0;
			}

			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				outfitIndex = 1;
			}

			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				outfitIndex = 2;
			}

			if (Input.GetKeyDown (KeyCode.DownArrow)) {
				hidden = false;
			}
		} 
		else 
		{
			if (Input.GetKeyDown (KeyCode.RightArrow)) 
			{
				facingRight = true;

			}

			if (Input.GetKeyDown (KeyCode.LeftArrow)) 
			{
				facingRight = false;
			}

			if (Input.GetKeyDown (KeyCode.UpArrow)) 
			{
				hidden = true;
			}
		}
	}

	void handleHiding ()
	{
		hiddenUI.SetActive (hidden);
		if (hidden) {
			exposedUI.SetActive (false);
			sr.sortingOrder = 1;
		} else {
			exposedUI.SetActive (true);
			sr.sortingOrder = 3;
		}
	}
}
