using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineUnit : MonoBehaviour {

	public Color scored;
	public List<SpriteRenderer> blocks = new List<SpriteRenderer>();
	int banked;
	public int alreadyPassed;
	public bool unwinnable = false;
	ScoreManager sm;
	AudioSource auds;
	HitEffect he;

	// Use this for initialization
	void Start () 
	{
		scored = GameObject.Find ("Point").GetComponent<SpriteRenderer> ().color;
		sm = GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ();
		auds = GameObject.Find ("ScoreManager").GetComponent<AudioSource> ();
		he = GameObject.Find ("HitEffect").GetComponent<HitEffect> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (this.gameObject.transform.childCount <= 0) 
		{
			Destroy (this.gameObject);
		}
			
		if (unwinnable == false) 
		{
			for (int i = 0; i < blocks.Count; i++) 
			{
				if (blocks [i] != null) 
				{
					if (banked < blocks.Count)
					if (blocks [i].color == scored) 
					{
						banked++;
					} 
					else 
					{
						banked = alreadyPassed;
					}

					if (banked == blocks.Count) 
					{
						sm.hits++;
						sm.inARow++;
						auds.PlayOneShot (sm.hitSound);
						he.active = true;
						if (sm.inARow == sm.firstMulti) 
						{
							auds.PlayOneShot (sm.smallCheer);
						}
						if (sm.inARow == sm.secondMulti) 
						{
							auds.PlayOneShot (sm.midCheer);
						}
						if (sm.inARow == sm.thirdMulti) 
						{
							auds.PlayOneShot (sm.bigCheer);
						}
						unwinnable = true;
					}
				} 
				else 
				{
					banked = alreadyPassed;
				}
			}
		}
	}
}
