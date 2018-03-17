using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;
using UnityEngine.UI;
//using UnityEngine.UI;

public enum PlayerState{
	miniGaming,
	timescheduling,
	visualNoveling,
}

public class GlobalManager :  Singleton<GlobalManager>{
	protected GlobalManager () {}

	//Turn on in inspector when debugging minigames
	public bool miniGameDebug;

	//Main Stats
	public float DanceScore = 0;
	public float VocalScore = 0;
	public float PRScore = 0;
	public float Stress = 0;
	public float jPeRelationship = 0;
	public float leeRelationship = 0; 
	public float stressMultiplier;

	//Career Stats
	public float TalkShowScore = 0;

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

	//What your stats are at the beginning of the week.  Used for end of week result screen.
	public float SOWdance;
	public float SOWvocal;
	public float SOWpr;
	public float SOWlee;
	public float SOWjp;
	public float SOWstress;
	public float SOWEDance;
	public float SOWEvocal;
	public float SOWEpr;
	public string SOWstring;
	public bool isStopped;

	//Peformance Stuff
	public bool performance; //Tells the manager whether or not we are currently in a performance.
	public List <int> performanceDates = new List<int>();  //All the specific days that there will be performances.
	public float numberOfFansRequired; //The number of fans required to pass the current phase.

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
	public AudioClip countDown; //Sound effect counting down to next mini-game;
	bool playedCountdown; //Makes sure the sound only plays once per block.
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
			if (!miniGameDebug) {
				if (scheduleList.Count > 0) {
					if (currentIndex + 1 < scheduleList.Count) {
						return scheduleList [currentIndex + 1].type.ToString ();
					} else {
						return "End of the Week";
					}
				}
			} else {
				return "DEBUG MODE";
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

		Screen.SetResolution(1600, 900, true);
	}

	//Called when mini-game phase begins.
	public void StartMiniGaming()
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
		if (performance) {
			maxGameTimer = su.time;
		} else {
			maxGameTimer = su.time * timePerUnit;	
		}
		//Sets game timer to the time from schedule. If it is a performance, the time we take the time is seconds directly from the unity
			//if it is normal day. we take the time from the unity and multiply it by the time per unit.

		currentGame = currentType;

//		Debug.Log(currentIndex+"."+currentType.ToString()+" for		 "+maxGameTimer+"seconds");
		//Loads game type depending on schedule list.
		switch(currentType){
		case UnitType.Dance:SceneManager.LoadScene("PoseyMatchy");break;
		case UnitType.Vocal:SceneManager.LoadScene("PitchyMatchy");break;
		case UnitType.PR:SceneManager.LoadScene("PassOutFliers");break;
		case UnitType.Sleep:SceneManager.LoadScene("PhonyResty");break;
		case UnitType.TalkShow:SceneManager.LoadScene ("TalkeyShowey");break;
		case UnitType.Songwriting:SceneManager.LoadScene ("SongyWritey");break;
		case UnitType.StreetModeling:SceneManager.LoadScene ("DressyUppy");break;
		}


		//If the current mini-game isn't sleeping, add stress.
		if(currentType != UnitType.Sleep)
		{
			//if(Stress + currentTime*10f< 1000f){
			if (!performance) 
			{
				Stress += maxGameTimer * stressMultiplier;
			}
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
			if (performance) {
				maxJPTime = su.time;
			} else {
				maxJPTime = su.time * timePerUnit;
			}
//			Debug.Log(currentIndex+"."+su.type.ToString()+" for "+maxJPTime+"seconds");
		}

		if (name == "Lee") 
		{
			currentLeeTime = 0;
			ScheduleUnit su = LeeSchedule [LeeIndex];
			LeeCurrentType = su.type;
			if (performance) {
				maxLeeTime = su.time;
			} else {
				maxLeeTime = su.time * timePerUnit;
			}
		}
	}
		

	void Update()
	{
		if (Stress <= 0) 
		{
			Stress = 0;
		}
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
		effectiveDance = DanceScore - Mathf.Round(Stress);
		effectiveVocal = VocalScore - Mathf.Round (Stress);
		effectivePR = PRScore - Mathf.Round(Stress);
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
			Destroy (this.gameObject);
			SceneManager.LoadScene ("Title");
		}
			
		//Mini-game stuff exclusive update.
		if(myState == PlayerState.miniGaming)
		{
			findFriendObject ();
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				loadVNScene ();
			}
			if (!miniGameDebug) 
			{
				handleCalendar ();
			}
			if (LeeImage != null) 
			{
				LeeImage.SetActive (LeePresent);
			}
			if (JPImage != null) 
			{
				JPImage.SetActive (JPPresent);
			}
			if (!miniGameDebug) 
			{
				JPPresent = matching (currentGame, JPCurrentType);
				LeePresent = matching (currentGame, LeeCurrentType);
			}

			if (LeeCurrentType == UnitType.PR) 
			{
				LeeFans += Time.deltaTime;
			}
			if (JPCurrentType == UnitType.PR) 
			{
				JPFans += Time.deltaTime;
			}

			if (currentGameTime >= (maxGameTimer - countDown.length) && !playedCountdown && NextActivity != "End of the Week") 
			{
				GetComponent<AudioSource> ().PlayOneShot (countDown);
				playedCountdown = true;
			}
			currentGameTime += Time.deltaTime; // ticks up the mini-game timer.
			currentJPTime += Time.deltaTime;
			currentLeeTime += Time.deltaTime; 
			if (currentGameTime > maxGameTimer) 
			{
				if (!miniGameDebug) {
					currentIndex++; //Move to next game in schedule.
					if (currentIndex < scheduleList.Count) { //Load new game if we're not at the end of the list.
						playedCountdown = false;
						JPImage = null;
						LeeImage = null;
						loadMiniGame ();
					} else 
					{ // If We're done with the current schedule.
						if (!isStopped) 
						{
							GameObject.Find ("PGScreen").GetComponent<PostGameResults> ().startPostGame ();	
							isStopped = true;
						}
					}
				}
			}

			if (currentJPTime > maxJPTime) {
				JPIndex++;
				if (JPIndex < JPSchedule.Count) {
					advanceFriendSchedule ("JP");
				} else {
					JPSchedule.Clear ();
				}
			}
			if (currentLeeTime > maxLeeTime) {
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
			SOWstring = GameObject.Find ("ThisWeek").GetComponent<Text> ().text;
		}
		//VN exclusive update.
		else if(myState == PlayerState.visualNoveling)
		{
			isStopped = false;
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

	public void loadVNScene()
	{
		scheduleList.Clear ();
		sceneToLoad = StoryManager.determineScene (dayIndex, effectiveDance, effectiveVocal, effectivePR, Stress, jPeRelationship, leeRelationship);
		if (!StoryManager.scenesVisited.Contains (currentTextAsset.name)) 
		{
			StoryManager.scenesVisited.Add (currentTextAsset.name);
		}
		SceneManager.LoadScene ("VN");
		GetComponent<DJSchedgy> ().shuffle ();
		myState = PlayerState.visualNoveling;
		scheduleSettledCount = 0;
		if (performanceDates.Contains (dayIndex)) {
			GetComponent<PerformanceScheduler> ().makePerformanceSchedule (dayIndex);
			performance = true;
		} else {
			performance = false;
		}

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

	//Used at the start of each week, so what know what scores the player has at the beginning of the week.
	public void saveStartofWeekScores(string oneWeekLater)
	{
		SOWdance = DanceScore;
		SOWvocal = VocalScore;
		SOWpr = PRScore;
		SOWlee = leeRelationship;
		SOWjp = jPeRelationship;
		SOWstress = Stress;
		SOWEDance = effectiveDance;
		SOWEvocal = effectiveVocal;
		SOWEpr = effectivePR;
		SOWstring = oneWeekLater;
	}
}
