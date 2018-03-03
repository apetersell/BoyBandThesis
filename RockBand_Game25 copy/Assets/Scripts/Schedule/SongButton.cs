using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongButton : MonoBehaviour {

	public int trackNum;
	DJSchedgy dj; 
	// Use this for initialization
	void Start () 
	{
		dj = GameObject.Find ("GlobalStats").GetComponent<DJSchedgy> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeTrack ()
	{
		dj.selectedTrack = dj.miniGameSongs [trackNum];
		Debug.Log ("Clicked " + gameObject.name);
	}
}
