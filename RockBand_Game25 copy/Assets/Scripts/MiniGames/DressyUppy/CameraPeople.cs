using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPeople : MonoBehaviour {

	public float speed;
	float posX;
	public int brandIndex;
	public Sprite[] brands;
	SpriteRenderer tshirt;
	public float endPos;
	public bool spawnedRight; 
	public float scoreZone;
	ScoreManager sm;
	DressyUppyControls duc;
	public bool scored;
	SpriteRenderer face;
	public Sprite[] faces;

	// Use this for initialization
	void Start () 
	{
		posX = transform.position.x;
		tshirt = transform.GetChild (0).GetComponent<SpriteRenderer> ();
		face = transform.GetChild (1).GetComponent<SpriteRenderer> ();
		sm = GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ();
		duc = GameObject.Find ("Aig").GetComponent<DressyUppyControls> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!scored) {
			face.color = Color.clear;
		} else {
			face.color = Color.white;
		}
			
		if (spawnedRight) 
		{
			posX -= speed * Time.deltaTime;
			if (posX < scoreZone && posX > 0) 
			{
				if (duc.outfitIndex == brandIndex && !scored && !duc.hidden && duc.facingRight) 
				{
					sm.scorePoints (true);
					face.sprite = faces [0];
					scored = true;
				}
			}

			if (posX < 0 && !scored) 
			{
				sm.scorePoints (false);
				face.sprite = faces [1];
				scored = true;
			}
			
		} else {
			posX += speed * Time.deltaTime;
			if (posX > scoreZone && posX < 0) 
			{
				if (duc.outfitIndex == brandIndex && !scored && !duc.hidden && !duc.facingRight) 
				{
					sm.scorePoints (true);
					face.sprite = faces [0];
					scored = true;
				}
			}

			if (posX > 0 && !scored) 
			{
				sm.scorePoints (false);
				face.sprite = faces [1];
				scored = true;
			}
		}
		transform.position = new Vector3 (posX, transform.position.y, transform.position.z);
		tshirt.sprite = brands [brandIndex];
		if (spawnedRight) {
			if (transform.position.x <= endPos) {
				Destroy (this.gameObject);
			}
		} else {
			if (transform.position.y >= endPos) {
				Destroy (this.gameObject);
			}
		}
	}
}
