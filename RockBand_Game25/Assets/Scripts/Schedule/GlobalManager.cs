﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;
//using UnityEngine.UI;

public enum PlayerState{
	miniGaming,
	timescheduling,
	visualNoveling,
}

public class GlobalManager :  Singleton<GlobalManager>{
	protected GlobalManager () {}

	//Main Stats
	public float DanceScore = 0;
	public float VocalScore = 0;
	public float PRScore = 0;
	public float vocalScore = 0; 
	public float Stress = 0;
	public float jPeRelationship = 0;
	public float leeRelationship = 0; 
	public float stressMultiplier;

	//Stats affected by stress.
	public float effectiveDance;
	public float effectiveVocal;
	public float effectivePR;

	//Fans tracker
	public float totalFans;
	public float AigFans;
	public float LeeFans;
	public float JPFans;

	//Used to handle calendar display
	public float dateTimer;
	public float dayNightTimer;
	public bool night;

	public int scheduleSettledCount; //How much of the schedule has been filled out?
	public string upNext; //The name of the next mini-game.

	//Gets access to the VN text assets;
	public int dayIndex = 0;
	public int sceneLayers
	{
		get 
		{
			int layerNum = StoryManager.findLayers (dayIndex);
			return layerNum;
		}
	}
	string sceneToLoad;
	public TextAsset testText;
	public TextAsset currentTextAsset
	{
		get
		{
			if (dayIndex != 0) {
				TextAsset ta = Resources.Load<TextAsset> ("Dialog/Day" + dayIndex.ToString () + sceneToLoad);
				return ta;
			} else {
				TextAsset ta = testText;
				return ta; 
			}
		}
	}
		
	public List<ScheduleUnit> scheduleList; //The player's schedule.
	public List<ScheduleUnit> JPSchedule; 
	public List<ScheduleUnit> LeeSchedule;
	public int timePerUnit; //How much time is each schedule unit worth?
	[SerializeField]public int maxGameTimer = 0; //Max timer for current mini-game.
	[SerializeField]int currentIndex = 0; //Where we are on the schedule.
	[SerializeField]float currentGameTime = 0; //What the timer is currently at for the current mini-game. Ticks up to game timer.
	public PlayerState myState = PlayerState.timescheduling;
	UnitType currentGame;


	public bool JPPresent;
	public bool LeePresent;
	UnitType JPCurrentType;
	UnitType LeeCurrentType;
	public int JPIndex;
	public int LeeIndex;
	public int maxJPTime;
	public float currentJPTime;
	public int maxLeeTime;
	public float currentLeeTime; 
	GameObject LeeImage;
	GameObject JPImage;


	//Finds the string for the next actiivty.
	public string NextActivity{
		get{
			if(scheduleList.Count > 0){
				if(currentIndex + 1 < scheduleList.Count){
					return scheduleList[currentIndex+1].type.ToString();
				}else{
					return "End of the Week";
				}
			}
			return "";
		}
	}

	//Finds how much time is remaining;
	public float timeLeft
	{
		get{
			return maxGameTimer - currentGameTime;
		}
	}

	//Makes the GlobalManger into a Singleton
	public static GameObject instance;

	void Start() 
	{
		if(instance == null){
			instance = gameObject;
		}else{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(instance);
	}

	//Called when mini-game phase begins.
	void StartMiniGaming()
	{
		currentIndex = 0; //
		JPIndex = 0;
		LeeIndex = 0;
		dayIndex++; //Moves the day counter up 1.
		float songUnit = (GameObject.Find ("GlobalStats").GetComponent<DJSchedgy> ().selectedTrack.length) / 14f;
		timePerUnit = Mathf.RoundToInt (songUnit);
		dateTimer = 0;
		dayNightTimer = 0;
		night = false;
		myState = PlayerState.miniGaming;
		loadMiniGame();
		advanceFriendSchedule ("JP");
		advanceFriendSchedule ("Lee");

	}

	//Loads each mini-game;
	void loadMiniGame()
	{
		currentGameTime = 0; //Resets timer;
		ScheduleUnit su = scheduleList [currentIndex];
		UnitType currentType = su.type;
		maxGameTimer = su.time * timePerUnit;	//Sets game timer to the time from schedule.

		currentGame = currentType;

		Debug.Log(currentIndex+"."+currentType.ToString()+" for "+maxGameTimer+"seconds");
		//Loads game type depending on schedule list.
		switch(currentType){
		case UnitType.Dance:SceneManager.LoadScene("PoseyMatchy");break;
		case UnitType.Vocal:SceneManager.LoadScene("PitchyMatchy");break;
		case UnitType.PR:SceneManager.LoadScene("PassOutFliers");break;
		case UnitType.Sleep:SceneManager.LoadScene("Sleep");break;
		}


		//If the current mini-game isn't sleeping, add stress.
		if(currentType != UnitType.Sleep)
		{
			//if(Stress + currentTime*10f< 1000f){
			Stress += maxGameTimer * stressMultiplier;
			//}else{
			//	Stress = 1000f;
			//	//game over
			//}
		}


	}

	void advanceFriendSchedule (string name)
	{
		if (name == "JP")
		{
			currentJPTime = 0;
			ScheduleUnit su = JPSchedule [JPIndex];
			JPCurrentType = su.type;
			maxJPTime = su.time * timePerUnit;
		}

		if (name == "Lee") 
		{
			currentLeeTime = 0;
			ScheduleUnit su = LeeSchedule [LeeIndex];
			LeeCurrentType = su.type;
			maxLeeTime = su.time * timePerUnit;
		}
	}
		

	void Update()
	{
		//Calculate total fans.
		totalFans = Mathf.Round(AigFans) + Mathf.Round (JPFans) + Mathf.Round(LeeFans);

		//Caps on Relationship Values
		if (jPeRelationship > 100) 
		{
			jPeRelationship = 100;
		}
		if (jPeRelationship < 0) 
		{
			jPeRelationship = 0;
		}
		if (leeRelationship > 100) 
		{
			leeRelationship = 100;
		}
		if (leeRelationship < 0) 
		{
			leeRelationship = 0;
		}


		//Calculates stats minus stress.
		effectiveDance = DanceScore - Stress;
		effectiveVocal = VocalScore - Stress;
		effectivePR = PRScore - Stress;
		if (effectiveDance < 0) 
		{
			effectiveDance = 0;
		}
		if (effectiveVocal < 0) 
		{
			effectiveVocal = 0;
		}
		if (effectivePR < 0) 
		{
			effectivePR = 0;
		}
		//Resets the game.
		if (Input.GetKeyDown (KeyCode.R)) 
		{
			SceneManager.LoadScene ("Title");
			Destroy (this.gameObject);
		}
			
		//Mini-game stuff exclusive update.
		if(myState == PlayerState.miniGaming)
		{
			findFriendObject ();
			handleCalendar ();
			if (LeeImage != null) 
			{
				LeeImage.SetActive (LeePresent);
			}
			if (JPImage != null) 
			{
				JPImage.SetActive (JPPresent);
			}
			JPPresent = matching (currentGame, JPCurrentType);
			LeePresent = matching (currentGame, LeeCurrentType);

			if (LeeCurrentType == UnitType.PR) 
			{
				LeeFans += Time.deltaTime;
			}
			if (JPCurrentType == UnitType.PR) 
			{
				JPFans += Time.deltaTime;
			}
			
			currentGameTime += Time.deltaTime; // ticks up the mini-game timer.
			currentJPTime += Time.deltaTime;
			currentLeeTime += Time.deltaTime; 
			if(currentGameTime > maxGameTimer)
			{
				currentIndex ++; //Move to next game in schedule.
				if(currentIndex < scheduleList.Count) //Load new game if we're not at the end of the list.
				{
					JPImage = null;
					LeeImage = null;
					loadMiniGame();
				}
				else // If We're done with the current schedule.
				{
					scheduleList.Clear();
					sceneToLoad = StoryManager.determineScene(dayIndex, effectiveDance, effectiveVocal, effectivePR, Stress, jPeRelationship, leeRelationship);
					if (!StoryManager.scenesVisited.Contains (currentTextAsset.name)) 
					{
						StoryManager.scenesVisited.Add (currentTextAsset.name);
					}
					SceneManager.LoadScene("VN");
					GetComponent<DJSchedgy> ().shuffle ();
					myState = PlayerState.visualNoveling;
					scheduleSettledCount = 0;
				}
			}
			if (currentJPTime > maxJPTime) 
			{
				JPIndex++;
				if (JPIndex < JPSchedule.Count) {
					advanceFriendSchedule ("JP");
				} else {
					JPSchedule.Clear ();
				}
			}
			if (currentLeeTime > maxLeeTime) 
			{
				LeeIndex++;
				if (LeeIndex < LeeSchedule.Count) {
					advanceFriendSchedule ("Lee");
				} else {
					LeeSchedule.Clear ();
				}
			}
		}
		//Schedule exclusive update.
		else if(myState == PlayerState.timescheduling)
		{
			
		}
		//VN exclusive update.
		else if(myState == PlayerState.visualNoveling)
		{
//			if (Input.GetKeyDown (KeyCode.Space)) 
//			{
//				StoryManager.updateStats (effectiveDance, effectiveVocal, effectivePR, Stress, jPeRelationship, leeRelationship);
//				Debug.Log ("Dance: " + StoryManager.danceScore);
//				Debug.Log ("Vocal: " + StoryManager.vocalScore);
//				Debug.Log ("PR: " + StoryManager.prScore);
//				Debug.Log ("Stress: " + StoryManager.stressLevel);
//				Debug.Log ("J-Pe: " + StoryManager.jPeRelationship);
//				Debug.Log ("Lee: " + StoryManager.leeRelationship);
//			}
		}

	}

	//Fills in schedule during planning phase. Makes "Ready" show up when the schedule is full.
	public void scheduleSettle(){
		scheduleSettledCount ++;
		if(scheduleSettledCount >= 14){
			GameObject.Find("BtnReady").SendMessage("showBtnReady");
		}
	}

	bool matching (UnitType first, UnitType second)
	{
		return first == second;
	}

	void findFriendObject ()
	{
		if (LeeImage == null) {
			LeeImage = GameObject.Find ("obj_Lee");
		}
		if (JPImage == null) {
			JPImage = GameObject.Find ("obj_JP");
		}
	}
		
	void handleCalendar ()
	{
		dateTimer += Time.deltaTime;
		dayNightTimer += Time.deltaTime;
		if (dateTimer >= timePerUnit * 2) 
		{
			GetComponent<CalendarTracker> ().advanceDay ();
			dateTimer = 0;
		}

		if (dayNightTimer >= (timePerUnit)) 
		{
			if (night) {
				night = false;
			} else {
				night = true;
			}
			dayNightTimer = 0;
		}
	}
}
