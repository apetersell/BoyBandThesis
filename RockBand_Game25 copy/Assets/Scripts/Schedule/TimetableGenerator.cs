using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//What a chunk of schedule means (what type of game is it, how much time is scheduled).
public struct ScheduleUnit
{
	public UnitType type;
	public int time;
}

public class TimetableGenerator : MonoBehaviour {
	
	[SerializeField]InputManager[] im_timeTableUnits; //Input managers from each of the schedule nodes.
	[SerializeField]List<ScheduleUnit> scheduleList; //List of chunked schedule units.
	List<ScheduleUnit> JPSchedule; 
	List<ScheduleUnit> LeeSchedule; 
	GlobalManager globe;
	public Transform node;

	// Use this for initialization
	void Start () 
	{
		globe = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
		im_timeTableUnits = node.GetComponentsInChildren<InputManager>(); // find the timetable units.
	}
	
	// Update is called once per frame
	void Update () 
	{


	
	}

	//Any blank slots are changed to sleep (never called?)
	void defaultUnitChangeToSleep(){
		for(int i = 0;i < 14;i++){
			InputManager im_unit = im_timeTableUnits[i];
			im_unit.SendMessage("checkDefaultToSleep");

		}

		//StartCoroutine(waitForSeconds);
	}

	//Pads out the time between schedule and first game so that its not too fast.
	IEnumerator waitForGameStart(){
		yield return new WaitForSeconds(1f);
		globe.SendMessage("StartMiniGaming");
	}

	//Called when the ready button is pressed. This is it building the schedule based on the nodes we filled in.
	void timeArrangement()
	{
		makeFriendSchedule ("JP");
		makeFriendSchedule ("Lee");
		defaultUnitChangeToSleep();
		scheduleList = new List<ScheduleUnit>();
		UnitType lastType = UnitType.None;
		int hour = 0; //How many units worth of time is the chunk.
		for(int i = 0;i < im_timeTableUnits.Length;i++)
		{
			InputManager im_unit = im_timeTableUnits[i]; //Grab a node from the schedule
			UnitType currentType = im_unit.MyType; // make the current type the type of the node we just pulled.

			if(currentType != lastType) //if that type we just pullled is not the same as the last type
			{
				if(i > 0) //and it's not the first node we pulled.
				{
					//Make a new schedule unit with this type and time.
					ScheduleUnit currentUnit;
					currentUnit.type = lastType;
					currentUnit.time = hour;
					scheduleList.Add(currentUnit);
				}
				//change Last type to the whatever the current type is and reset the timer.
				lastType = currentType; 
				hour = 1;
			}
			else //if it is the same as the last one we pulled.
			{
				hour+= 1; //add time to it.
				if(i == im_timeTableUnits.Length - 1)//if it's the last one in the schdule
				{
					//Make the schedule unit.
					ScheduleUnit currentUnit;// = new ScheduleUnit(lastType,hour);
					currentUnit.type = lastType;
					currentUnit.time = hour;//(currentUnit.type == UnitType.Sleep)? hour:hour*10;//hour;
					scheduleList.Add(currentUnit);
				}
			}
		}
		//Makes the schedule list on globe match the one we just made here.
		globe.scheduleList = scheduleList;
		StartCoroutine(waitForGameStart()) ;
	}

	void makeFriendSchedule(string name)
	{
		if (name == "JP") 
		{
			int hour = 0;
			UnitType lastType = UnitType.None;
			JPSchedule = new List<ScheduleUnit>(); 
			for (int i = 0; i < 14; i++) 
			{
				InputManager im_unit = im_timeTableUnits[i]; //Grab a node from the schedule
				UnitType currentType = im_unit.JPeGame; // make the current type the type of the node we just pulled.
//				Debug.Log (im_unit.JPeGame);

				if(currentType != lastType) //if that type we just pullled is not the same as the last type
				{
					if(i > 0) //and it's not the first node we pulled.
					{
						//Make a new schedule unit with this type and time.
						ScheduleUnit currentUnit; 
						currentUnit.type = lastType;
						currentUnit.time = hour;
						JPSchedule.Add(currentUnit);
						//Debug.Log ("Added: " + currentUnit.type.ToString () + " for " + currentUnit.time);
					}
					//change Last type to the whatever the current type is and reset the timer.
					lastType = currentType; 
					hour = 1;
				}
				else //if it is the same as the last one we pulled.
				{
					hour+= 1; //add time to it.
					if(i == 13)//if it's the last one in the schedule
					{
						//Make the schedule unit.
						ScheduleUnit currentUnit;//= new ScheduleUnit(lastType,hour);
						currentUnit.type = lastType;
						currentUnit.time = hour;//(currentUnit.type == UnitType.Sleep)? hour:hour*10;//hour;
						JPSchedule.Add(currentUnit);
						//Debug.Log ("Finished, Added: " + currentUnit.type.ToString () + " for " + currentUnit.time);
					}
				}
			}
			globe.JPSchedule = JPSchedule;
		}
		if (name == "Lee") 
		{
			int hour = 0;
			UnitType lastType = UnitType.None;
			LeeSchedule = new List<ScheduleUnit>(); 
			for (int i = 0; i < 14; i++) 
			{
				InputManager im_unit = im_timeTableUnits[i]; //Grab a node from the schedule
				UnitType currentType = im_unit.LeeGame; // make the current type the type of the node we just pulled.

				if(currentType != lastType) //if that type we just pullled is not the same as the last type
				{
					if(i > 0) //and it's not the first node we pulled.
					{
						//Make a new schedule unit with this type and time.
						ScheduleUnit currentUnit; 
						currentUnit.type = lastType;
						currentUnit.time = hour;
						LeeSchedule.Add(currentUnit);
					}
					//change Last type to the whatever the current type is and reset the timer.
					lastType = currentType; 
					hour = 1;
				}
				else //if it is the same as the last one we pulled.
				{
					hour+= 1; //add time to it.
					if(i == 13)//if it's the last one in the schdule
					{
						//Make the schedule unit.
						ScheduleUnit currentUnit;// = new ScheduleUnit(lastType,hour);
						currentUnit.type = lastType;
						currentUnit.time = hour;//(currentUnit.type == UnitType.Sleep)? hour:hour*10;//hour;
						LeeSchedule.Add(currentUnit);
					}
				}
			}
		}
		globe.LeeSchedule = LeeSchedule;
	}
}
