using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showScores : MonoBehaviour {
	//public Text txtDance,txtVocal,txtPR,txtEnergy;
	GlobalManager gm;
	public bool fans;
	public bool relationshipStatus; 
	public bool Lee; 
	public bool JP; 
	public bool Dance;
	public bool Vocal;
	public bool PR;
	public bool Stress;
	string relationship;
	float multiplier; 
	Text t;
	GUIStyle style = new GUIStyle(); 

	void Start(){
		gm = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
		t = GetComponent<Text> ();
		updateStats();
		style.richText = true;
	}
	 
	void Update()
	{
		float flashSpeed = 1;
	}

	void updateStats()
	{
		if (Dance) {
			t.text = Mathf.Round (gm.DanceScore).ToString () + "<color=#F49AC1FF> (-" + Mathf.Round(gm.Stress) + ")</color>"; 
		} else if (Vocal) {
			t.text = Mathf.Round (gm.VocalScore).ToString () + "<color=#F49AC1FF> (-" + Mathf.Round (gm.Stress) + ")</color>"; 
		} else if (PR) {
			t.text = Mathf.Round (gm.PRScore).ToString () + "<color=#F49AC1FF> (-" + Mathf.Round (gm.Stress) + ")</color>"; 
		} else if (Stress) {
			t.text = Mathf.Round(gm.Stress).ToString ();
		} else if (fans) {
			t.text = gm.totalFans + " (Aig " + Mathf.Round (gm.AigFans) + "/<color=#9B61CEFF>J-Pe " + Mathf.Round (gm.JPFans) + " </color>/<color=#F49AC1FF>Lee " + Mathf.Round (gm.LeeFans) + "</color>)"; 
		}else if (relationshipStatus) {
			if (JP) {
				t.text = status (gm.jPeRelationship) + " x" + multi (gm.jPeRelationship); 
			}
			if (Lee) {
				t.text = status (gm.leeRelationship) + " x" + multi (gm.leeRelationship);
			}
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
