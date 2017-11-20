using System.Collections;
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
	public float leeRelationShip = 0; 
	public float stressMultiplier;

	//Stats affected by stress.
	public float effectiveDance;
	public float effectiveVocal;
	public float effectivePR;

	public int scheduleSettledCount; //How much of the schedule has been filled out?
	public string upNext; //The name of the next mini-game.

	//Gets access to the VN text assets;
	public int dayIndex = 0;
	string sceneToLoad;
	public TextAsset currentTextAsset
	{
		get{
			TextAsset ta = Resources.Load<TextAsset>("Dialog/Day"+dayIndex.ToString()+sceneToLoad);
			return ta;
		}
	}
		
	public List<ScheduleUnit> scheduleList; //The player's schedule.
	public int timePerUnit; //How much time is each schedule unit worth?
	[SerializeField]public int maxGameTimer = 0; //Max timer for current mini-game.
	[SerializeField]int currentIndex = 0; //Where we are on the schedule.
	[SerializeField]float currentGameTime = 0; //What the timer is currently at for the current mini-game. Ticks up to game timer.
	public PlayerState myState = PlayerState.timescheduling;

	//Finds the string for the next actiivty.
	public string NextActivity{
		get{
			if(scheduleList.Count > 0){
				if(currentIndex + 1 < scheduleList.Count){
					return scheduleList[currentIndex+1].type.ToString();
				}else{
					return "End of the Day";
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
		dayIndex++; //Moves the day counter up 1.
		float songUnit = (GameObject.Find ("GlobalStats").GetComponent<DJSchedgy> ().selectedTrack.length) / 12f;
		timePerUnit = Mathf.RoundToInt (songUnit);

		myState = PlayerState.miniGaming;
		loadMiniGame();

	}

	//Loads each mini-game;
	void loadMiniGame(){
		currentGameTime = 0; //Resets timer;
		UnitType currentType = scheduleList[currentIndex].type;
		maxGameTimer = scheduleList[currentIndex].time * timePerUnit;	//Sets game timer to the time from schedule.

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

	void Update()
	{
		//Calculates stats minus stress.
		effectiveDance = DanceScore - Stress;
		effectiveVocal = VocalScore - Stress;
		effectivePR = PRScore - Stress;

		//Resets the game.
		if (Input.GetKeyDown (KeyCode.R)) 
		{
			SceneManager.LoadScene (0);
			Destroy (this.gameObject);
		}
			
		//Mini-game stuff exclusive update.
		if(myState == PlayerState.miniGaming)
		{
			currentGameTime += Time.deltaTime; // ticks up the mini-game timer.
			if(currentGameTime > maxGameTimer)
			{
				currentIndex ++; //Move to next game in schedule.
				if(currentIndex < scheduleList.Count) //Load new game if we're not at the end of the list.
				{
					loadMiniGame();
				}
				else // If We're doine with the current schedule.
				{
					scheduleList.Clear();
					sceneToLoad = StoryManager.determineScene(dayIndex, effectiveDance, effectiveVocal, effectivePR, Stress, jPeRelationship, leeRelationShip);
					if (!StoryManager.scenesVisited.Contains (currentTextAsset.name)) 
					{
						StoryManager.scenesVisited.Add (currentTextAsset.name);
					}
					SceneManager.LoadScene("VN");
					myState = PlayerState.visualNoveling;
					scheduleSettledCount = 0;
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

		}

	}

	//Fills in schedule during planning phase. Makes "Ready" show up when the schedule is full.
	public void scheduleSettle(){
		scheduleSettledCount ++;
		if(scheduleSettledCount >= 12){
			GameObject.Find("BtnReady").SendMessage("showBtnReady");
		}
	}
}
