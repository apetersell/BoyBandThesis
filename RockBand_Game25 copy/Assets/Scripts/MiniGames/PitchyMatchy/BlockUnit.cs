using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockUnit : MonoBehaviour {

	VocalBlockManager vbm;
	GlobalManager globe;
	public float pointOfNoReturn = -4.718f; 
	public bool lookedAt;
	
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
					if (!lookedAt) 
					{
						lookedAt = true;
						GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ().scorePoints (false);
						GameObject.Find ("Aig-mini-vocal").GetComponent<UpDownAnimation> ().doCough ();
					}
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
