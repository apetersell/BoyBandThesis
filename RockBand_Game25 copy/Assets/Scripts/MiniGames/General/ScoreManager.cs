using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Beat;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public string game; //Name of the current min-game
	Text scoreDisplay; // UI stuff
	Text multiplierDisplay;
	Text inARowDisplay;
	public float score; //Your score
	public float baseValue; //How much is a match worth before multipliers
	public float firstMulti; //How many notes do you need to hit in a row before multipliers
	public float secondMulti; 
	public float thirdMulti;  
	public float multiplier = 1; //How much you score is multlipied by this number
	public float hits;
	public float misses;
	public float streak;
	public AudioClip hitSound;
	public AudioClip fanSound;
	public AudioClip smallCheer;
	public AudioClip midCheer;
	public AudioClip bigCheer;
	public int inARow;
	public float valueOfMatch;
	public float relationshipMultiplier = 1;
	public float danceFanValue;
	public float vocalFanValue;
	public float PRFanValue;
	bool theEnd;
	GameObject scoreBoard;
	GlobalManager globe;
	AudioSource auds;
   	private Clock clock;
	public GameObject fanBar;

	void Awake ()
	{
	   clock = Clock.Instance;
		if (game == "PR" || game == "Sing" || game == "Dance") {
			fanBar = GameObject.Find ("FanBar");
		}
	}

	// Use this for initialization
	void Start () {
		globe = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
		scoreDisplay = GameObject.Find ("Score").GetComponent<Text> ();
		multiplierDisplay = GameObject.Find ("Multiplier").GetComponent<Text> ();
		inARowDisplay = GameObject.Find ("In a Row").GetComponent<Text> ();
		scoreBoard = GameObject.Find ("ScoreBoard");
		auds = GetComponent<AudioSource> ();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		pointValue ();
		displayScores ();
		determineRelationshipMulti ();
		fanBar.SetActive (globe.performance);
	}

	public void scorePoints (bool hit)
	{
		if (!globe.isStopped) 
		{
			if (hit) 
			{
				if (globe.performance) 
				{
					score += performancePointValue ();
					globe.AigFans += performancePointValue ();
				} 
				else 
				{
					score += valueOfMatch;
					if (globe != null) {
						if (game == "PR" || game == "Talk Show") {
							globe.PRScore += valueOfMatch;
							globe.AigFans++;
						}
						if (game == "Sing" || game == "Songwriting") {
							globe.VocalScore += valueOfMatch;
						}
						if (game == "Dance" || game == "Street Modeling") {
							globe.DanceScore += valueOfMatch;
						}
					}
				}
				inARow++;
				hits++;
				if (globe.performance) {
					auds.clip = fanSound;
					auds.PlayScheduled (clock.AtNextSixteenth ());
				} else {
					auds.clip = hitSound;
					auds.PlayScheduled (clock.AtNextSixteenth ());
				}
				if (inARow == firstMulti) {
					auds.clip = smallCheer;
					auds.PlayScheduled (clock.AtNextSixteenth ());
				}
				if (inARow == secondMulti) {
					auds.clip = midCheer;
					auds.PlayScheduled (clock.AtNextSixteenth ());
				}
				if (inARow == thirdMulti) {
					auds.clip = bigCheer;
					auds.PlayScheduled (clock.AtNextSixteenth ());
				}
			} else {
				if (!globe.isStopped) {
					inARow = 0;
					misses++;
//					globe.Stress += 1;
				}
			}
		}
	}

	void pointValue ()
	{
		valueOfMatch = (baseValue * multiplier) * relationshipMultiplier;
		if (inARow < firstMulti) {
			multiplier = 1;
		} else if (inARow >= firstMulti && inARow < secondMulti) {
			multiplier = 2;
		} else if (inARow >= secondMulti && inARow < thirdMulti) {
			multiplier = 3;
		} else if (inARow >= thirdMulti) 
		{
			multiplier = 4;
		}
		if (inARow > streak) 
		{
			streak = inARow;
		}

		danceFanValue = Mathf.Round (globe.effectiveDance / 50);
		vocalFanValue = Mathf.Round (globe.effectiveVocal / 50);
		PRFanValue = Mathf.Round (globe.effectivePR / 50);

		if (danceFanValue < 1) 
		{
			danceFanValue = 1;
		}
		if (vocalFanValue < 1) 
		{
			vocalFanValue = 1;
		}
		if (PRFanValue < 1) 
		{
			PRFanValue = 1;
		}
	}

	void displayScores ()
	{
		float trueMulti = multiplier * relationshipMultiplier;
		if (!globe.performance) {
			scoreDisplay.text = "Score: " + Mathf.Round (score).ToString (); 
		} else {
			scoreDisplay.text = "FansPerHit: " + performancePointValue ().ToString ();
		}
		multiplierDisplay.text = "Multiplier: x" + trueMulti.ToString (); 
		inARowDisplay.text = "Combo: " + inARow.ToString (); 
	}

	void determineRelationshipMulti ()
	{
		if (globe.JPPresent) {
			if (globe.LeePresent) {
				relationshipMultiplier = (multi (globe.jPeRelationship)) * (multi (globe.leeRelationship));
			} else {
				relationshipMultiplier = multi (globe.jPeRelationship);
			}
		} else if (globe.LeePresent) {
			relationshipMultiplier = multi (globe.leeRelationship);
		} else {
			relationshipMultiplier = 1;
		}
	}

	float performancePointValue ()
	{
		float result = 0;
		float stat = 0;
		if (game == "PR" || game == "Talk Show") 
		{
			stat = PRFanValue;
		}
		if (game == "Sing" || game == "Songwriting") {
			stat = vocalFanValue;
		}
		if (game == "Dance" || game == "Street Modeling") {
			stat = danceFanValue;
		}

		result = Mathf.Round((stat * multiplier) * relationshipMultiplier); 

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