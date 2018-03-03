using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity.Example;

public class KeyboardControls : MonoBehaviour 
{
	public static int currentChoice;
	public static int numOfChoices;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		controls ();
		if (Input.GetKeyDown (KeyCode.Return)) 
		{
			GetComponent<ExampleDialogueUI> ().SetOption (currentChoice);
		}
	}

	public static void controls()
	{
		//Debug.Log (currentChoice);
		if (Input.GetKeyDown (KeyCode.UpArrow)) 
		{
			currentChoice++;
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) 
		{
			currentChoice--;
		}

		if (currentChoice > numOfChoices) 
		{
			currentChoice = 0;
		}
		if (currentChoice < 0) 
		{	
			currentChoice = numOfChoices;
		}
	}
}
