using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrustrationEffect : MonoBehaviour {

	public float speed;
	public float scaleTime;
	public float expandedScale;
	public float fadeTime;
	Vector3 originalScale;
	SpriteRenderer sr;

	void Start () 
	{
		StartCoroutine (scaleOverTime (scaleTime));
		originalScale = transform.localScale;
		sr = GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () 
	{

		transform.position = new Vector3 (
			transform.position.x,
			transform.position.y + speed,
			transform.position.z);
	}

	IEnumerator scaleOverTime (float time)
	{
		Vector3 targetScale = new Vector3 (expandedScale, expandedScale, expandedScale); 
		float currentTime = 0.0f;

		do
		{
			transform.localScale = Vector3.Lerp(originalScale, targetScale, currentTime / time); 
			//			tm.color = Color.Lerp (firstColor, secondColor, currentTime / time);
			currentTime += Time.deltaTime;
			yield return null;
		} 
		while (currentTime <= time); 

		StartCoroutine (fade (fadeTime)); 
	}

	IEnumerator fade (float time) 
	{

		float currentTime = 0.0f;
		do
		{ 
			sr.color = Color.Lerp (Color.white, Color.clear, currentTime/time);  
			currentTime += Time.deltaTime;
			yield return null;
		} 
		while (currentTime <= time); 

		Destroy(this.gameObject);
	}
}