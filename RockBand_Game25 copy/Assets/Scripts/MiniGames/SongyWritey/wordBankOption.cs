using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wordBankOption : MonoBehaviour {

	LyricGenerator lg;
	public int index;
	public Color usual;
	public Color used;
	RectTransform rt;
	public bool alreadyUsed;

	// Use this for initialization
	void Start () 
	{
		lg = GameObject.Find ("Lyric").GetComponent<LyricGenerator> ();
		rt = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (index == lg.selectedIndex) 
		{
			transform.GetChild (0).gameObject.SetActive (true);
		} 
		else 
		{
			transform.GetChild (0).gameObject.SetActive (false);
		}

		if (alreadyUsed) {
			GetComponent<Text> ().color = used;
		} else {
			GetComponent<Text> ().color = usual;
		}
	}
}
