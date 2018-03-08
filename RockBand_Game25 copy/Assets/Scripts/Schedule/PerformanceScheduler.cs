using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceScheduler : MonoBehaviour {

	List <ScheduleUnit> performanceSchedule = new List <ScheduleUnit> ();
	public List<UnitType> debut = new List<UnitType>();
	public List<int> debutUnitTimes = new List<int> ();
	GlobalManager globe; 
	public List<AudioClip> performanceSongs = new List<AudioClip> ();

	// Use this for initialization
	void Start () 
	{
		globe = GetComponent<GlobalManager> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void makePerformanceSchedule(int day)
	{
		performanceSchedule.Clear ();
		if (day == globe.performanceDates[0]) 
		{
			for (int i = 0; i < debut.Count; i++) 
			{
				ScheduleUnit su;
				su.type = debut [i];
				su.time = debutUnitTimes [i];
				performanceSchedule.Add (su);
			}
		}

		globe.scheduleList = performanceSchedule;
		globe.JPSchedule = performanceSchedule;
		globe.LeeSchedule = performanceSchedule;
		GetComponent<DJSchedgy> ().selectedTrack = performanceSongs [0];
	}
}
