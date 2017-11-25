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
	public Sprite[] jPeSprites;
	public Sprite[] leeSprites;
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
		for (int i = 0; i < 12; i++) 
		{
			Image spriteFace = GameObject.Find (workingOn + "_" + (i + 1)).GetComponent<Image> ();
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
			spriteFace.sprite = findSprite (workingOn, type);

		}
	}

	public Sprite findSprite (string name, UnitType type) 
	{
		Sprite sprite = null;
		if (name == "JPe") 
		{
			if (type == UnitType.None) {
				sprite = jPeSprites [0];   
			}
			if (type == UnitType.Dance) { 
				sprite  = jPeSprites [1]; 
			}
			if (type == UnitType.Vocal) { 
				sprite = jPeSprites [2];   
			}
			if (type == UnitType.PR) {
				sprite = jPeSprites [3]; 
			}
		} else {
			if (type == UnitType.None) {
				sprite = leeSprites [0];
			}
			if (type == UnitType.Dance) {
				sprite = leeSprites [1];
			}
			if (type == UnitType.Vocal) {
				sprite = leeSprites [2];
			}
			if (type == UnitType.PR) {
				sprite = leeSprites [3];
			}
		}

		return sprite;
	}
}
