using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HattyPlayer : MonoBehaviour {

	public float speed;
	float xPos;
	public float max;
	public float min;
	public int score = 0;
	public AudioClip sound;
	public Text t;
	public Sprite [] sprites;
	bool differentFace;
	float faceTimer;
	public GameObject hatty;
	public GameObject currentHat; 

	// Use this for initialization
	void Awake ()
	{
		xPos = transform.localPosition.x;
	}

	void Start () 
	{
		xPos = transform.localPosition.x;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (differentFace) 
		{
			faceTimer += Time.deltaTime;
		}

		if (faceTimer > .5f) 
		{
			GetComponent<SpriteRenderer> ().sprite = sprites [0];
			faceTimer = 0;
			differentFace = false;
		}
			
		t.text = score.ToString ();
		transform.localPosition = new Vector3 (xPos, transform.localPosition.y, transform.localPosition.z);
		if (Input.GetKey (KeyCode.RightArrow) && xPos <= max) 
		{
			xPos += speed;
		}

		if (Input.GetKey (KeyCode.LeftArrow) && xPos >= min) 
		{
			xPos -= speed;
		}

		if (currentHat == null) 
		{
			GameObject newHat = Instantiate (hatty) as GameObject;
			//newHat.GetComponent<Hatty> ().hp = this;
			currentHat = this.gameObject;
			float randomX = Random.Range (min, max);
			newHat.transform.SetParent (this.transform.parent);
			newHat.transform.localPosition = new Vector3 (randomX, newHat.transform.localPosition.y, newHat.transform.localPosition.z);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Hatty")) 
		{
			currentHat = null;
			Destroy (col.gameObject);
			GetComponent<AudioSource>().PlayOneShot (sound);
			changeFace (1);
			score++;
		} 
	}

	public void changeFace (int sent)
	{
		GetComponent<SpriteRenderer> ().sprite = sprites [sent];
		faceTimer = 0;
		differentFace = true;

	}
}
