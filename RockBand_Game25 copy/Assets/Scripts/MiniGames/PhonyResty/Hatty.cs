using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatty : MonoBehaviour {

	public float minY;
	PhonyRestyManager prm;
	// Use this for initialization
	void Start () 
	{
		prm = GameObject.Find ("Manager").GetComponent<PhonyRestyManager> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (transform.localPosition.y <= minY)
		{
			GameObject.Find ("HattyPlayer").GetComponent<HattyPlayer> ().changeFace (2);
			GameObject.Find ("HattyPlayer").GetComponent<HattyPlayer> ().currentHat = null;
			GameObject.Find ("HattyPlayer").GetComponent<HattyPlayer> ().score = 0;
			prm.frustrate (0);
			Destroy (this.gameObject);
		}
	}		
}
