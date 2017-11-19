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

	//Returns the VN scene to load.
	public static string determineScene(float dayIndex, float dance, float vocal, float pr, float stress, float jPe, float lee)
	{
		//Updates the local refrence to the main stats;
		danceScore = dance;
		vocalScore = vocal;
		prScore = pr;
		stressLevel = stress;
		jPeRelationship = jPe;
		leeRelationship = lee;

		string result = "";
		//Calls different string functions depeding on the day;
		if (dayIndex == 1) 
		{
			result = DayOne();
		}
		else if(dayIndex == 2)
		{
			result = DayTwo();
		}

		return result;
	}

	//String functions for each day.
		
	// PRE - DEBUT **************************************************************************************************************
	static string DayOne()
	{
		float minPRScore = 300;
		float minVocalScore = 300;
		float minDanceScore = 300;
		float maxStress = 300;

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
		float minPRScore = 600;
		float minVocalScore = 600;
		float minDanceScore = 600;
		float maxStress = 600;
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
}
