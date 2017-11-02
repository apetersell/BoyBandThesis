using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct ScheduleUnit{
	public UnitType type;
	public int time;
}

public class TimetableGenerator : MonoBehaviour {
	

	public GameObject go_timeTable;
	[SerializeField]InputManager[] im_timeTableUnits;
	[SerializeField]List<ScheduleUnit> scheduleList;
	GlobalManager globe;
	public Transform node;
	// Use this for initialization
	void Start () {


		globe = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
		//go_timeTableUnits = new GameObject[12];
		im_timeTableUnits = node.GetComponentsInChildren<InputManager>();
//		Debug.Log (im_timeTableUnits.Length);
		//GameObject go_timeTableU
//		for(int i = 0;i<12;i++){
//			GameObject go_timeTableUnit = Instantiate(go_timeTable) as GameObject;
//			float width = go_timeTable.GetComponent<RectTransform>().sizeDelta.x;
//
//			go_timeTableUnit.GetComponent<RectTransform>().sizeDelta = new Vector2(width/12f,go_timeTableUnit.
//				GetComponent<RectTransform>().sizeDelta.y);
//			
//			go_timeTableUnit.transform.position = 
//				go_timeTable.transform.position + new Vector3((-5.5f+i)*width/12f,0,0);
//			
//			go_timeTableUnit.transform.SetParent(node,true);
//			//go_timeTableUnit.transform.localScale = Vector3.one;
//			go_timeTableUnit.name = (i+1).ToString();
//			go_timeTableUnit.AddComponent<InputManager>();
//			go_timeTableUnits[i]=go_timeTableUnit;
//		}
	}
	
	// Update is called once per frame
	void Update () {


		
	}

	void defaultUnitChangeToSleep(){
		for(int i = 0;i < 12;i++){
//			Debug.Log (i.ToString ());
			InputManager im_unit = im_timeTableUnits[i];
			im_unit.SendMessage("checkDefaultToSleep");

		}

		//StartCoroutine(waitForSeconds);
	}

	IEnumerator waitForGameStart(){
		yield return new WaitForSeconds(1f);
		//load scene
		globe.SendMessage("StartMiniGaming");
	}

	void timeArrangement(){
		defaultUnitChangeToSleep();

		scheduleList = new List<ScheduleUnit>();
		UnitType lastType = UnitType.None;
		//ScheduleUnit currentUnit;
		int hour = 0;
		for(int i = 0;i < 12;i++){
			InputManager im_unit = im_timeTableUnits[i];
			UnitType currentType = im_unit.MyType;
			if(currentType != lastType){
				if(hour > 0){
					ScheduleUnit currentUnit;// = new ScheduleUnit(lastType,hour);
					currentUnit.type = lastType;
					currentUnit.time = hour;//(currentUnit.type == UnitType.Sleep)? hour:hour*10;
					//currentUnit.time = hour;
					scheduleList.Add(currentUnit);
					Debug.Log(currentUnit.type.ToString()+hour.ToString());
				}
				lastType = currentType;
//				hour = 2;
				hour = 1;//timerPerUnit;
				//currentUnit = new ScheduleUnit(unit)//scheduleList.Add()
			}else{
//				hour+=2;
				hour+= 1;//timerPerUnit;
				if(i == 11){
					ScheduleUnit currentUnit;// = new ScheduleUnit(lastType,hour);
					currentUnit.type = lastType;
					currentUnit.time = hour;//(currentUnit.type == UnitType.Sleep)? hour:hour*10;//hour;
					scheduleList.Add(currentUnit);
					Debug.Log(currentUnit.type.ToString()+hour.ToString());
				}
			}
//			switch(currentType){
//			case UnitType.Dance:break;
//			case UnitType.Vocal:break;
//			case UnitType.PR:break;
//			case UnitType.Sleep:break;
//			}

		}

		globe.scheduleList = scheduleList;
		StartCoroutine(waitForGameStart()) ;
	}

	//void 

}
