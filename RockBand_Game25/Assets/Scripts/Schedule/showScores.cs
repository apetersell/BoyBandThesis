using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showScores : MonoBehaviour {
	//public Text txtDance,txtVocal,txtPR,txtEnergy;
	GlobalManager gm;

	void Start(){
		gm = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
		updateStats();
	}

	void updateStats(){
		//GlobalManager gm = (GlobalManager)
		GetComponent<Text>().text = "Dance:" + Mathf.RoundToInt(gm.DanceScore).ToString() +
			"\nVocal:" + Mathf.RoundToInt(gm.VocalScore).ToString() +
			"\nPR:" + Mathf.RoundToInt(gm.PRScore).ToString() +
			"\nStress:" + Mathf.RoundToInt(gm.Stress).ToString() ;
	}
}
