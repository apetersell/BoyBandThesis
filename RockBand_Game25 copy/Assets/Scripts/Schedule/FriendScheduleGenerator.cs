using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendScheduleGenerator : MonoBehaviour 
{
	public struct ScheduleUnit
	{
		public string name;
		public UnitType type; 
		public int time;
	}
	public Color danceColor;
	public Color vocalColor;
	public Color prColor;
	public Color restColor;
	public Color blank;
	public UnitType[] jPeGames;
	public UnitType[] leeGames;
	public ShuffleBag<UnitType> jPeBag;
	public ShuffleBag<UnitType> leeBag;
	[SerializeField]InputManager[] im_timeTableUnits; //Input managers from each of the schedule nodes.
	GlobalManager globe;
	public Transform table;

	void Awake ()
	{
		jPeBag = new ShuffleBag<UnitType> (); 
		for (int i = 0; i < jPeGames.Length; i++)
		{
				jPeBag.Add (jPeGames [i]);
		}

	
		leeBag = new ShuffleBag<UnitType> (); 
		for (int i = 0; i < leeGames.Length; i++) 
		{
			leeBag.Add (leeGames [i]);
		}
	}

	// Use this for initialization
	void Start () 
	{
		globe = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
		im_timeTableUnits = table.GetComponentsInChildren<InputManager>(); // find the timetable units.
		if (globe.myState == PlayerState.timescheduling) 
		{
			makeFriendSchedule ("JPe");
			makeFriendSchedule ("Lee");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void makeFriendSchedule(string workingOn)
	{
		for (int i = 0; i < 14; i++) 
		{
			Image icon = GameObject.Find (workingOn + "_" + (i + 1)).GetComponent<Image> ();
			UnitType type = UnitType.None;
			if (workingOn == "JPe") 
			{
				type = jPeBag.Next ();
				im_timeTableUnits [i].JPeGame = type;
			}
			if (workingOn == "Lee") 
			{
				type = leeBag.Next ();
				im_timeTableUnits [i].LeeGame = type;
			}
			icon.color = findColor (type);

		}
	}

	public Color findColor (UnitType type) 
	{
		Color result = Color.white;
		if (type == UnitType.None) {
			result = blank;   
		}
		if (type == UnitType.Dance) { 
			result = danceColor; 
		}
		if (type == UnitType.Vocal) { 
			result = vocalColor;   
		}
		if (type == UnitType.PR) {
			result = prColor; 
		}
		if (type == UnitType.Sleep){
			result = restColor;
		}
		return result;
	}
}
