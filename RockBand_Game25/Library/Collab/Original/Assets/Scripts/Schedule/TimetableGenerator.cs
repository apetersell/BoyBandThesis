using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimetableGenerator : MonoBehaviour {

	public GameObject go_timeTable;

	// Use this for initialization
	void Start () {
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
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
