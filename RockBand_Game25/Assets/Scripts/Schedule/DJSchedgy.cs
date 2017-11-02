using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DJSchedgy : MonoBehaviour {

	public AudioClip scheduleMusic;
	public AudioClip selectedTrack;
	public AudioClip VNMusic;
	public AudioClip[] miniGameSongs;

	GlobalManager globe;
	AudioSource aud;

	void Awake () {
		globe = GetComponent<GlobalManager> ();
		aud = GetComponent<AudioSource> ();
		for (int i = 0; i < miniGameSongs.Length; i++) 
		{
			Text title;
			title = GameObject.Find ("SongTitle" + (i + 1)).GetComponent<Text> ();
			title.text = miniGameSongs[i].name;
		}
	}
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (globe.myState == PlayerState.timescheduling)
		{
			if (aud.clip != scheduleMusic) 
			{
				aud.Stop ();
				aud.clip = scheduleMusic;
				aud.Play ();
			} 
		}
		else if (globe.myState == PlayerState.miniGaming) 
		{
			if (aud.clip != selectedTrack) 
			{
				aud.Stop ();
				aud.clip = selectedTrack;
				aud.Play ();
			}
		}else if(globe.myState == PlayerState.visualNoveling){
			if (aud.clip != VNMusic) 
			{
				aud.Stop ();
				aud.clip = VNMusic;
				aud.Play ();
			}
		}
	}
}
