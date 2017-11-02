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
	[SerializeField]GameObject[] go_timeTableUnits;
	[SerializeField]List<ScheduleUnit> scheduleList;
	// Use this for initialization
	void Start () {
		go_timeTableUnits = new GameObject[12];
		//GameObject go_timeTableU
		for(int i = 0;i<12;i++){
			GameObject go_timeTableUnit = Instantiate(go_timeTable) as GameObject;
			float width = go_timeTable.GetComponent<RectTransform>().sizeDelta.x;
			go_timeTableUnit.GetComponent<RectTransform>().sizeDelta = new Vector2(width/12f,go_timeTableUnit.
				GetComponent<RectTransform>().sizeDelta.y);
			go_timeTableUnit.transform.position = 
				go_timeTable.transform.position + new Vector3((-5.5f+i)*width/12f,0,0);
			go_timeTableUnit.transform.SetParent(go_timeTable.transform.parent,true);
			go_timeTableUnit.name = (i+1).ToString();
			go_timeTableUnit.AddComponent<InputManager>();
			go_timeTableUnits[i]=go_timeTableUnit;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void defaultUnitChangeToSleep(){
		for(int i = 0;i < 12;i++){
			GameObject go_unit = go_timeTableUnits[i];
			go_unit.GetComponent<InputManager>().SendMessage("checkDefaultToSleep");

		}

		//StartCoroutine(waitForSeconds);
	}

	IEnumerator waitForSeconds(){
		yield return new WaitForSeconds(2f);
		//load scene
	}

	void timeArrangement(){
		defaultUnitChangeToSleep();

		scheduleList = new List<ScheduleUnit>();
		UnitType lastType = UnitType.None;
		//ScheduleUnit currentUnit;
		int hour = 0;
		for(int i = 0;i < 12;i++){
			GameObject go_unit = go_timeTableUnits[i];
			UnitType currentType = go_unit.GetComponent<InputManager>().MyType;
			if(currentType != lastType){
				if(hour > 0){
					ScheduleUnit currentUnit;// = new ScheduleUnit(lastType,hour);
					currentUnit.type = lastType;
					currentUnit.time = hour;
					scheduleList.Add(currentUnit);
					Debug.Log(currentUnit.type.ToString()+hour.ToString());
				}
				lastType = currentType;
				hour = 2;
				//currentUnit = new ScheduleUnit(unit)//scheduleList.Add()
			}else{
				hour+=2;
				if(i == 11){
					ScheduleUnit currentUnit;// = new ScheduleUnit(lastType,hour);
					currentUnit.type = lastType;
					currentUnit.time = hour;
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


	}

}
