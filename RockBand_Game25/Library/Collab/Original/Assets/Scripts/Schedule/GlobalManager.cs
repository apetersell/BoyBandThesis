using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;

public enum PlayerState{
	miniGaming,
	timescheduling,
}

public class GlobalManager :  Singleton<GlobalManager>{
	protected GlobalManager () {}

	public float DanceScore = 0,VocalScore = 0,PRScore = 0 ,Stress = 0;
	public int scheduleSettledCount; 


	public List<ScheduleUnit> scheduleList;
	[SerializeField]public int currentTime = 0;
	[SerializeField]int currentIndex = 0;
	[SerializeField]float timeCounter = 0;
	[SerializeField]public PlayerState myState = PlayerState.timescheduling;

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
		myState = PlayerState.miniGaming;
		loadMiniGame();

	}

	void loadMiniGame(){
		timeCounter = 0;
		UnitType currentType = scheduleList[currentIndex].type;
		currentTime =scheduleList[currentIndex].time;	

		Debug.Log(currentIndex+"."+currentType.ToString()+" for "+currentTime+"seconds");
		switch(currentType){
		case UnitType.Dance:SceneManager.LoadScene(3);break;
		case UnitType.Vocal:SceneManager.LoadScene(1);break;
		case UnitType.PR:SceneManager.LoadScene(2);break;
		case UnitType.Sleep:SceneManager.LoadScene(4);break;
		}

		if(currentType == UnitType.Sleep){
			if(Stress - 30f*currentTime > 0){
				Stress -= currentTime*30f;
				
			}else{
				Stress = 0;
			}
		}else{
			if(Stress + currentTime*10f< 300){
				Stress += currentTime*10f;
			}else{
				Stress = 300;
				//game over
			}

		}
	}

	void Update(){
		if(myState == PlayerState.miniGaming){
			timeCounter += Time.deltaTime;
			if(timeCounter > currentTime){
				
				currentIndex ++;
				if(currentIndex < scheduleList.Count){
					
					loadMiniGame();
				}else{
					scheduleList.Clear();
					SceneManager.LoadScene(0);
					myState = PlayerState.timescheduling;
					scheduleSettledCount = 0;
				}
				//switch to next game

			}

		}else if(myState == PlayerState.timescheduling){
			
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
