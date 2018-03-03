using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockUnit : MonoBehaviour {

	VocalBlockManager vbm;
	GlobalManager globe;
	public float pointOfNoReturn = -4.718f;
	
	// Use this for initialization
	void Start () {

		vbm = GameObject.Find ("VocalBlockManager").GetComponent<VocalBlockManager> ();
		globe = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
		
	}
	
	// Update is called once per frame
	void Update () {
		float speed = vbm.speed;
		float endPos = vbm.EndPosX;
		transform.Translate(Time.deltaTime*speed*new Vector3(-1,0,0));

		if (transform.position.x < pointOfNoReturn) 
		{
			if (GetComponent<SpriteRenderer> ().color != GetComponentInParent<LineUnit> ().scored) 
			{
				if (GetComponentInParent<LineUnit> ().unwinnable == false) 
				{
					GetComponentInParent<LineUnit> ().unwinnable = true;
					GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ().misses++;
					GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ().inARow = 0;
					globe.Stress += 10;
				}
			}
		}

		if(transform.position.x < endPos)
		{
			if (GetComponent<SpriteRenderer> ().color == GetComponentInParent<LineUnit> ().scored) 
			{
				GetComponentInParent<LineUnit> ().alreadyPassed++;
			}
			Destroy(gameObject);
		}
	}
}
