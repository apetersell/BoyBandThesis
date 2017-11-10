﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Beat;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public string game;
	Text scoreDisplay;
	Text multiplierDisplay;
	Text inARowDisplay;
	public float score;
	public float baseValue;
	public float firstMulti;
	public float secondMulti;
	public float thirdMulti;  
	public float multiplier = 1;
	public float gameTimerMax;
	public float gameTimer;
	public float hits;
	public float misses;
	public float streak;
	public AudioClip hitSound;
	public AudioClip smallCheer;
	public AudioClip midCheer;
	public AudioClip bigCheer;
	public int inARow;
	public float valueOfMatch;
	bool theEnd;
	GameObject scoreBoard;
	GlobalManager globe;
	AudioSource auds;
   private Clock clock;

	void Awake ()
	{
	   clock = Clock.Instance;
		gameTimer = gameTimerMax;
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
	void Update () {

		pointValue ();
		displayScores ();
//		handleEnding ();
		
	}

	public void scorePoints (bool hit)
	{
		if (hit) {
			score += valueOfMatch;
			if (globe != null) 
			{
				if (game == "PR") {
					globe.PRScore += valueOfMatch;
				}
				if (game == "Sing") {
					globe.VocalScore += valueOfMatch;
				}
				if (game == "Dance") {
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
		}
	}

	void pointValue ()
	{
		valueOfMatch = baseValue * multiplier;
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
		multiplierDisplay.text = "Multiplier: X" + multiplier.ToString (); 
		inARowDisplay.text = "Combo: " + inARow.ToString (); 
	}

	void handleEnding ()
	{
		gameTimer -= Time.deltaTime;
		if (gameTimer <= 0) {
			theEnd = true;
		} else {
			theEnd = false;
		}

		if (theEnd) {
//			endText.SetActive (true);
			scoreBoard.SetActive (false);
//			if (Input.GetKeyDown(KeyCode.R))
//			{
//				SceneManager.LoadScene ("PoseyMatchy");
//			}
		} else {
//			endText.SetActive (false);
			scoreBoard.SetActive (true);
		}
	}

}