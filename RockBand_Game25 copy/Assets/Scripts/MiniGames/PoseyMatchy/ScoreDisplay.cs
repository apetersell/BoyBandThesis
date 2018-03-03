using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	ScoreManager sm;

	// Use this for initialization
	void Start () {

		sm = GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		if (gameObject.name == "Your Score") 
		{
			GetComponent<Text> ().text = "Your Score: " + sm.score;
		}
		if (gameObject.name == "Hits") 
		{
			GetComponent<Text> ().text = "Hit: " + sm.hits;
		}
		if (gameObject.name == "Misses") 
		{
			GetComponent<Text> ().text = "Missed: " + sm.misses;
		}
		if (gameObject.name == "LongestStreak") 
		{
			GetComponent<Text> ().text = "Longest Streak: " + sm.streak;
		}
		
	}
}
