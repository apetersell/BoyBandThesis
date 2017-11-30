using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showScores : MonoBehaviour {
	//public Text txtDance,txtVocal,txtPR,txtEnergy;
	GlobalManager gm;
	public bool stressIndicator;
	public bool relationshipStatus; 
	public bool Lee; 
	public bool JP; 
	string relationship;
	float multiplier; 

	void Start(){
		gm = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
		updateStats();
	}

	void Update()
	{
		float flashSpeed = 1;
		if (stressIndicator) 
		{
			Color lerpingColor = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time*flashSpeed, 1));
			GetComponent<Text> ().color = lerpingColor;
		}
	}

	void updateStats()
	{
		if (stressIndicator) {
			if (gm.Stress > 0) {
				GetComponent<Text> ().text = " - " + Mathf.RoundToInt (gm.Stress).ToString () +
				"\n\n - " + Mathf.RoundToInt (gm.Stress).ToString () +
				"\n\n - " + Mathf.RoundToInt (gm.Stress).ToString ();
			} else {
				GetComponent<Text> ().text = "";
			}
		} else if (relationshipStatus) {
			if (JP) {
				GetComponent<Text> ().text = status (gm.jPeRelationship) + "\n x" + multi (gm.jPeRelationship); 
			}
			if (Lee) {
				GetComponent<Text> ().text = status (gm.leeRelationship) + "\n x" + multi (gm.leeRelationship);
			}
		}
		else 
		{
			GetComponent<Text> ().text = "Dance:" + Mathf.RoundToInt (gm.DanceScore).ToString () +
			"\n\nVocal:" + Mathf.RoundToInt (gm.VocalScore).ToString () +
			"\n\nPR:" + Mathf.RoundToInt (gm.PRScore).ToString ();
		}
	}

	string status (float score)
	{
		string result = null;
		if (score <= 20) {
			result = "Mortal Enemy";
		} else if (score > 20 && score <= 40) {
			result = "Frenemy";
		} else if (score > 40 && score <= 60) {
			result = "Bro";
		} else if (score > 60 && score <= 80) {
			result = "Best Bro";
		} else{
			result = "BFF <3";
		}
		return result;
	}

	float multi (float score) 
	{
		float result = 0;
		if (score <= 20) {
			result = 0.5f;
		} else if (score > 20 && score <= 40) {
			result = 0.66f;
		} else if (score > 40 && score <= 60) {
			result = 1;
		} else if (score > 60 && score <= 80) {
			result = 1.5f;
		} else{
			result = 2;
		}
		return result;
	}
}
