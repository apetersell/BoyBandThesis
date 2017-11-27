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
	Text relationshipDisplay;
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
	public AudioClip smallCheer;
	public AudioClip midCheer;
	public AudioClip bigCheer;
	public int inARow;
	public float valueOfMatch;
	public float relationshipMultiplier = 1;
	bool theEnd;
	GameObject scoreBoard;
	GlobalManager globe;
	AudioSource auds;
   	private Clock clock;

	void Awake ()
	{
	   clock = Clock.Instance;
	}

	// Use this for initialization
	void Start () {
		globe = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
		scoreDisplay = GameObject.Find ("Score").GetComponent<Text> ();
		multiplierDisplay = GameObject.Find ("Multiplier").GetComponent<Text> ();
		inARowDisplay = GameObject.Find ("In a Row").GetComponent<Text> ();
		relationshipDisplay = GameObject.Find ("Relationship").GetComponent<Text> ();
		scoreBoard = GameObject.Find ("ScoreBoard");
		auds = GetComponent<AudioSource> ();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		pointValue ();
		displayScores ();
		determineRelationshipMulti ();
	}

	public void scorePoints (bool hit)
	{
		if (hit) 
		{
			score += valueOfMatch;
			if (globe != null) 
			{
				if (game == "PR") 
				{
					globe.PRScore += valueOfMatch;
				}
				if (game == "Sing") 
				{
					globe.VocalScore += valueOfMatch;
				}
				if (game == "Dance") 
				{
					globe.DanceScore += valueOfMatch;
				}
			}
			if (game != "Sing") 
			{
			   inARow++;
			   hits++;
			   auds.clip = hitSound;
			   auds.PlayScheduled(clock.AtNextSixteenth());
			}
			if (inARow == firstMulti) 
			{
			   auds.clip = smallCheer;
			   auds.PlayScheduled(clock.AtNextSixteenth());
			}
			if (inARow == secondMulti) 
			{
			   auds.clip = midCheer;
			   auds.PlayScheduled(clock.AtNextSixteenth());
			}
			if (inARow == thirdMulti) 
			{
			   auds.clip = bigCheer;
			   auds.PlayScheduled(clock.AtNextSixteenth());
         }
		} 
		else 
		{
			inARow = 0;
			misses++;
			globe.Stress += 10;
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
	}

	void displayScores ()
	{
		scoreDisplay.text = "Score: " + score.ToString (); 
		multiplierDisplay.text = "Multiplier: x" + multiplier.ToString (); 
		inARowDisplay.text = "Combo: " + inARow.ToString (); 
		relationshipDisplay.text = "Friendship: x" + relationshipMultiplier.ToString ();
	}

	void determineRelationshipMulti ()
	{
		if (globe.JPPresent) {
			if (globe.LeePresent) {
				relationshipMultiplier = (multi (globe.jPeRelationship)) * (multi (globe.leeRelationShip));
			} else {
				relationshipMultiplier = multi (globe.jPeRelationship);
			}
		} else if (globe.LeePresent) {
			relationshipMultiplier = multi (globe.leeRelationShip);
		} else {
			relationshipMultiplier = 1;
		}
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