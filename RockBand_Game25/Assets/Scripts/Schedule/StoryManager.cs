using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour {

	//Local reference to the main stats. Updates every time we determine a new scene.
	public static float danceScore;
	public static float vocalScore;
	public static float prScore;
	public static float stressLevel;
	public static float jPeRelationship;
	public static float leeRelationship;

	public static float statMeterFull = 1000f;
	public static float statHighThresh1 = 5000f;
	public static float statLowThresh1 = 2000;
	public static float fanMileStone1 = 5000;
	public static float averageRange = 500;

	//Keeps track of all the scenes we've already visited;
	public static List<string> scenesVisited = new List<string>(); 

	//Returns true if the stat passed is higher than the other main stats. 
	public static bool isHighestStats(float score)
	{
		if(score >= danceScore
			&& score >= vocalScore
			&&score >= prScore)
		{
			return true;
		}

		return false;
	}

	//Returns true if all the stats are within a certain range of each other.
	public static bool isWellRounded ()
	{
		float difDanceVocal = Mathf.Abs (danceScore - vocalScore); 
		float difDancePR = Mathf.Abs (danceScore - prScore);
		float difVocalPr = Mathf.Abs (vocalScore - prScore);
		if (difDanceVocal <= averageRange && difDancePR <= averageRange && difVocalPr <= averageRange) 
		{
			return true;
		}

		return false;
	}

	//Updates the local reference to stats;
	public static void updateStats (float dance, float vocal, float pr, float stress, float jPe, float lee)
	{
		danceScore = dance;
		vocalScore = vocal;
		prScore = pr;
		stressLevel = stress;
		jPeRelationship = jPe;
		leeRelationship = lee;
	}
	//Returns the VN scene to load.
	public static string determineScene(float dayIndex, float dance, float vocal, float pr, float stress, float jPe, float lee)
	{
		updateStats (dance, vocal, pr, stress, jPe, lee);
		string result = "";
		//Calls different string functions depeding on the day;
		if (dayIndex == 1) {
			result = DayOne (); 
		} else if (dayIndex == 2) {
			result = DayTwo ();
		} else if (dayIndex == 3) {
			result = DayThree ();
		} else if (dayIndex == 4) {
			result = "";
		}

		return result;
	}

	//Detrmines how many layers deep a VN section is (how many nodes do we have to navigate?)
	public static int findLayers (int dayIndex)
	{
		int result = 0;
		if (dayIndex == 4) {
			result = 6;
		} else {
			result = 1;
		}
		return result;
	}

	//On days with multiple layers, finds what new node to navigate to.
	public static string findNewNode (string day, int layer)
	{
		string result = "";
		if (day == "Day4") 
		{
			if (layer == 6) 
			{
				if (danceScore + vocalScore + prScore < statLowThresh1) {
					result = "LowStats";
				} else if (isWellRounded ()) 
				{
					result = "AllRounder";
				}
				else 
				{
					if (isHighestStats (danceScore)) {
						result = "Dance";
					} else if (isHighestStats (vocalScore)) {
						result = "Vocals";
					} else {
						result = "Publicity";
					}
				}
			}
			if (layer == 5) 
			{
				result = "PreConcert";
			}
			if (layer == 4) 
			{
				result = "PostConcert";
			}
			if (layer == 3) 
			{
				if (danceScore + vocalScore + prScore >= statHighThresh1) {
					result = "GoodConcert";
				} else if (danceScore + vocalScore + prScore < statLowThresh1) {
					result = "BadConcert";
				} else {
					result = "OKConcert";
				}
			}
			if (layer == 2) 
			{
				if (danceScore + vocalScore + prScore < statLowThresh1) {
					result = "LowStats2";
				} else if (isWellRounded ()) 
				{
					result = "AllRounder2";
				}
				else 
				{
					if (isHighestStats (danceScore)) {
						result = "Dance2";
					} else if (isHighestStats (vocalScore)) {
						result = "Vocals2";
					} else {
						result = "Publicity2";
					}
				}
			}
		}

		return result;
		
	}
	//String functions for each day.
		
	// PRE - DEBUT **************************************************************************************************************
	static string DayOne()
	{
		float minPRScore = 500;
		float minVocalScore = 500;
		float minDanceScore = 500;
		float maxStress = 500;

		string result = "";

		if(prScore < minPRScore && vocalScore < minVocalScore && danceScore < minDanceScore)
		{
			result = "D";
		}
		if(isHighestStats(prScore))
		{
			result = "A";
		}
		else if(isHighestStats(vocalScore))
		{
			result = "B";
		}
		else
		{
			result = "C";
		}

		if(result == "")
		{
			result = "D";
		}

		return result;
	}

	static string DayTwo()
	{
		float minPRScore = 1000;
		float minVocalScore = 1000;
		float minDanceScore = 1000;
		float maxStress = 1000;
		string result = "";
		if(stressLevel > maxStress)
		{
			result = "D";
		}
		else if(isHighestStats(prScore))
		{
			if (scenesVisited.Contains ("Day1A")) 
			{
				result = "A";
			}
			else
			{
				result = "B";
			}
		}
		else if(isHighestStats(vocalScore) && danceScore < minDanceScore && prScore < minPRScore)
		{
			result = "C";
		}
		else
		{
			//dance is highest
			if(prScore < minPRScore && vocalScore < minVocalScore && danceScore < minDanceScore)
			{
				if (scenesVisited.Contains ("Day1D"))
				{
					result = "E";
				}
				else
				{
					result = "F";
				}
			}
			else if(vocalScore < minVocalScore && prScore < minPRScore)
			{
				result = "C";
			}
			else
			{
				result = "G";
			}
		}
		if(result == ""){
			result = "F";
		}

		return result;
	}

	static string DayThree() 
	{
		float minPRScore = 1500;
		float minVocalScore = 1500;
		string result = "";

		if (jPeRelationship >= leeRelationship) {
			if (prScore >= minPRScore) { 
				result = "B";
			} else {
				result = "A";
			}
		} else {
			if (vocalScore >= minVocalScore) {
				result = "D";
			} else {
				result = "C";
			}
		}
		
		return result;
	}
}
