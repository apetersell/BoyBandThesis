using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicStaff : MonoBehaviour {

	Image img;
	public float maxTimer;
	float timer;

	// Use this for initialization
	void Start () 
	{
		img = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
		if (timer >= maxTimer) 
		{
			timer = 0;
		}
		img.fillAmount = timer / maxTimer;
	}
}
