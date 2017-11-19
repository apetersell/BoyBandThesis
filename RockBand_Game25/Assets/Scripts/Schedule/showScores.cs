using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showScores : MonoBehaviour {
	//public Text txtDance,txtVocal,txtPR,txtEnergy;
	GlobalManager gm;
	public bool stressIndicator;

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
		if (stressIndicator) 
		{
			if (gm.Stress > 0) {
				GetComponent<Text> ().text = " - " + Mathf.RoundToInt (gm.Stress).ToString () +
					"\n\n - " + Mathf.RoundToInt (gm.Stress).ToString () +
					"\n\n - " + Mathf.RoundToInt (gm.Stress).ToString ();
			} else {
				GetComponent<Text> ().text = "";
			}
		} 
		else 
		{
			GetComponent<Text> ().text = "Dance:" + Mathf.RoundToInt (gm.DanceScore).ToString () +
			"\n\nVocal:" + Mathf.RoundToInt (gm.VocalScore).ToString () +
			"\n\nPR:" + Mathf.RoundToInt (gm.PRScore).ToString ();
		}
	}
}
