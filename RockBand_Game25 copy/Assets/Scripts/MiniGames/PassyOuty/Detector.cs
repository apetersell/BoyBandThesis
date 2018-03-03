using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour {

	public GameObject NPC;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void  OnTriggerEnter2D (Collider2D coll)
	{
		PassyNPC p = coll.gameObject.GetComponent<PassyNPC> ();
		if (p != null) 
		{
			if (p.active) 
			{
				if (NPC == null) 
				{
					NPC = coll.gameObject;
					p.seePlayer (true);
				}
			}
		}
	}

	void OnTriggerExit2D (Collider2D coll)
	{
		PassyNPC p = null;
		if (NPC != null) 
		{
			p = NPC.GetComponent<PassyNPC> ();
		}
		if (coll.gameObject == NPC) 
		{
			p.seePlayer (false);
			if (p.hasFlier == false) 
			{
				p.gotFlier (false);
			}
		}
		NPC = null;
	}
}
