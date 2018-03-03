using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendFaces : MonoBehaviour {

	public Sprite[] jPe;
	public Sprite[] lee;
	Image img;
	public bool isJpe;

	// Use this for initialization
	void Start () 
	{
		img = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void changeSprite (UnitType type)
	{
		if (isJpe) {
			if (type == UnitType.None) {
				img.sprite = jPe [0];
			}
			if (type == UnitType.Dance) {
				img.sprite = jPe [1];
			}
			if (type == UnitType.Vocal) {
				img.sprite = jPe [2];
			}
			if (type == UnitType.PR) {
				img.sprite = jPe [3];
			}
		} else {
			if (type == UnitType.None) {
				img.sprite = lee [0];
			}
			if (type == UnitType.Dance) {
				img.sprite = lee [1];
			}
			if (type == UnitType.Vocal) {
				img.sprite = lee [2];
			}
			if (type == UnitType.PR) {
				img.sprite = lee [3];
			}
		}
	}
}
