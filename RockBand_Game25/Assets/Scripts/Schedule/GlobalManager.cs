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

	public int scheduleSettledCount; 
	public string upNext;

	//public Dictionary allTextAssets;
	public int dayIndex = 0;
	string sceneToLoad;
	public TextAsset currentTextAsset{
		get{
			TextAsset ta = Resources.Load<TextAsset>("Dialog/Day"+dayIndex.ToString()+sceneToLoad);
			return ta;
		}
	}

//	public string[] VNScensNames;
//	int dayIndex;

	public List<ScheduleUnit> scheduleList;
	public int timerPerUnit;
	[SerializeField]public int currentTime = 0;
	[SerializeField]int currentIndex = 0; 
	[SerializeField]float timeCounter = 0;
	public PlayerState myState = PlayerState.timescheduling;
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

	public float timeLeft{
		get{
			return currentTime - timeCounter;
		}
	}
	public static GameObject instance;

	void Start() {
		
		if(instance == null){
			instance = gameObject;
		}else{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(instance);
	}
		
	void StartMiniGaming(){
		currentIndex = 0;
		dayIndex++;
		float songUnit = (GameObject.Find ("GlobalStats").GetComponent<DJSchedgy> ().selectedTrack.length) / 12f;
		timerPerUnit = Mathf.RoundToInt (songUnit);

		myState = PlayerState.miniGaming;
		loadMiniGame();

	}

	void loadMiniGame(){
		timeCounter = 0;
		UnitType currentType = scheduleList[currentIndex].type;
		currentTime =scheduleList[currentIndex].time*timerPerUnit;	

		Debug.Log(currentIndex+"."+currentType.ToString()+" for "+currentTime+"seconds");
		switch(currentType){
		case UnitType.Dance:SceneManager.LoadScene("PoseyMatchy");break;
		case UnitType.Vocal:SceneManager.LoadScene("PitchyMatchy");break;
		case UnitType.PR:SceneManager.LoadScene("PassOutFliers");break;
		case UnitType.Sleep:SceneManager.LoadScene("Sleep");break;
		}


		//lessen stress when
		if(currentType != UnitType.Sleep)
		{
			//if(Stress + currentTime*10f< 1000f){
			Stress += stressMultiplier;
			//}else{
			//	Stress = 1000f;
			//	//game over
			//}

		}
	}

	void Update(){

		if (Input.GetKeyDown (KeyCode.R)) 
		{
			SceneManager.LoadScene (0);
			Destroy (this.gameObject);
		}

//		UnitType nextTime
//		upNext = cu

		if(myState == PlayerState.miniGaming)
		{
			timeCounter += Time.deltaTime;
			if(timeCounter > currentTime)
			{
				currentIndex ++;
				if(currentIndex < scheduleList.Count)
				{
					loadMiniGame();
				}
				else
				{
					scheduleList.Clear();

					sceneToLoad = StoryManager.determineScene(dayIndex, DanceScore, VocalScore, PRScore, Stress, jPeRelationship, leeRelationShip);
					if (!StoryManager.scenesVisited.Contains (currentTextAsset.name)) 
					{
						StoryManager.scenesVisited.Add (currentTextAsset.name);
					}
					SceneManager.LoadScene("VN");
					myState = PlayerState.visualNoveling;
					scheduleSettledCount = 0;
				}
				//switch to next game

			}

		}
		else if(myState == PlayerState.timescheduling)
		{
			
		}
		else if(myState == PlayerState.visualNoveling)
		{

		}

	}

//	int scheduleSettledCount = 0;
	//public Image i_ready;
	public void scheduleSettle(){
		scheduleSettledCount ++;
//		Debug.Log(scheduleSettledCount);
		if(scheduleSettledCount >= 12){
			//make the ui turns up
			GameObject.Find("BtnReady").SendMessage("showBtnReady");
		}
	}
}
