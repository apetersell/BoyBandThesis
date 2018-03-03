using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarTracker : MonoBehaviour 
{
	//Information for displaying the date
	public struct Month 
	{
		public string name;
		public int numberOfDays; 
	}
	public string displayDate;
	public string oneWeeklater;
	List <Month> calendar = new List <Month> ();
	public string [] monthName; 
	public int[] daysIntheMonth;
	Month currentMonth;
	Month advancedMonth;
	string displayName;
	string displayNameA; 
	int monthIndex;
	int monthIndexA;
	int dateIndex = 1;
	int dateIndexA = 7;

	int daysThisMonth;
	int daysThisMonthA;

	// Use this for initialization
	void Start () 
	{
		for (int i = 0; i < monthName.Length; i++) 
		{
			Month m;
			m.name = monthName [i];
			m.numberOfDays = daysIntheMonth [i];
			calendar.Add (m);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		displayDate = displayName + ", " + dateIndex + suffix (dateIndex);
		oneWeeklater = displayNameA + ", " + dateIndexA + suffix (dateIndexA);
		currentMonth = calendar [monthIndex];
		advancedMonth = calendar [monthIndexA];
		displayName = currentMonth.name;
		displayNameA = advancedMonth.name;
		daysThisMonth = currentMonth.numberOfDays;
		daysThisMonthA = advancedMonth.numberOfDays;

		if (dateIndex > daysThisMonth) 
		{
			monthIndex++;
			dateIndex = 1;
		}

		if (dateIndexA > daysThisMonthA) 
		{
			monthIndexA++;
			dateIndexA = 1;
		}

		if (monthIndex > monthName.Length) 
		{
			monthIndex = 0;
		}

		if (monthIndexA > monthName.Length) 
		{
			monthIndex = 0;
		}

//		if (Input.GetKeyDown (KeyCode.Space)) 
//		{
//			advanceDay ();
//		}
		
	}

	string suffix(int date)
	{
		string result = "";
		if (date == 1 || date == 21 || date == 31) {
			result = "st";
		} else if (date == 2 || date == 22) {
			result = "nd";
		} else if (date == 3 || date == 23) {
			result = "rd";
		} else {
			result = "th";
		}
		return result;
	}

	public void advanceDay ()
	{
		dateIndex++;
		dateIndexA++;
	}
}
