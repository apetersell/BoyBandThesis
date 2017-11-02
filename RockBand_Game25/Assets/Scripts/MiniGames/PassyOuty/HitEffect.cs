using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour 
{
	public bool active;
	SpriteRenderer sr;
	public float expandedScale;
	public float scaleTime;
	Vector3 originalScale;
	Color originalColor;
	Color fadedColor;

	// Use this for initialization
	void Start () {

		sr = GetComponent<SpriteRenderer> ();
		originalScale = gameObject.transform.localScale;
		originalColor = sr.color;
		fadedColor = originalColor;
		fadedColor.a = 0;

	}

	// Update is called once per frame
	void Update () 
	{
		if (active) 
		{
			StartCoroutine (scaleOverTime (scaleTime)); 
		}
		sr.sprite = transform.parent.gameObject.GetComponent<SpriteRenderer> ().sprite;
		sr.flipX = transform.parent.gameObject.GetComponent<SpriteRenderer> ().flipX;
		if (active == false) 
		{
			sr.color = fadedColor;
		}
	}

	IEnumerator scaleOverTime (float time)
	{ 
		Vector3 targetScale = new Vector3 (expandedScale, expandedScale, expandedScale);
		float currentTime = 0.0f;

		do
		{
			transform.localScale = Vector3.Lerp(originalScale, targetScale, currentTime / time);
			sr.color = Color.Lerp (originalColor, fadedColor, currentTime/time); 
			currentTime += Time.deltaTime;
			yield return null;
		} 
		while (currentTime <= time);

		active = false;
	}
}

