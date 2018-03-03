using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextProperties : MonoBehaviour {

	public bool aig;
	public int yesno;
	public Text text;
	public Image photo;
	public int pos;
	public string owner;
	public string content;
	public Sprite[] photos;
	public Vector3 [] positions;
	public string[] yesResponses;
	public string[] noResponses;
	public int aigChoiceIndex;

	// Use this for initialization
	void Start () 
	{
		text = gameObject.transform.GetChild (0).GetComponent<Text> ();
		photo = gameObject.transform.GetChild (1).GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!aig) {
			text.text = content;
			if (owner == "J-Pe") {
				photo.sprite = photos [0];
			}
			if (owner == "Lee") {
				photo.sprite = photos [1];
			}
		} else {
			text.text = aigSays ();
		}

		transform.localPosition = positions [pos];
		transform.localScale = new Vector3 (1, 1, 1);
	}

	public void changeAigChoice ()
	{
		int rando = Random.Range (0, yesResponses.Length);
		aigChoiceIndex = rando;
	}
		
	public string aigSays()
	{
		string result = null;
		if (yesno == 0) 
		{
			result = yesResponses [aigChoiceIndex];
		}
		if (yesno == 1) 
		{
			result = noResponses [aigChoiceIndex];
		}

		return result;
	}
}
