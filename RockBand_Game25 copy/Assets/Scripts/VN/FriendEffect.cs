using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendEffect : MonoBehaviour 
{
	AudioSource auds;
	public Sprite happyFace; 
	public Sprite angryFace; 
	public bool happy;
	public float speed;
	public AudioClip happySound;
	public AudioClip angrySound; 
	public float scaleTime;
	public float expandedScale;
	public float fadeTime;
	Vector3 originalScale;
	Image img;


	// Use this for initialization
	void Start () 
	{
		auds = GetComponent<AudioSource> ();
		StartCoroutine (scaleOverTime (scaleTime));
		originalScale = GetComponent<RectTransform> ().localScale;
		img = GetComponent<Image> ();
		if (happy) {
			img.sprite = happyFace;
			auds.PlayOneShot (happySound);
		} else {
			img.sprite = angryFace;
			auds.PlayOneShot (angrySound);
		}
	

	}
	
	// Update is called once per frame
	void Update () 
	{
		GetComponent<RectTransform> ().localPosition = new Vector3 (
			GetComponent<RectTransform> ().localPosition.x,
			GetComponent<RectTransform> ().localPosition.y + speed,
			GetComponent<RectTransform> ().localPosition.z);
	}

	IEnumerator scaleOverTime (float time)
	{
		Vector3 targetScale = new Vector3 (expandedScale, expandedScale, expandedScale); 
		float currentTime = 0.0f;

		do
		{
			GetComponent<RectTransform>().localScale = Vector3.Lerp(originalScale, targetScale, currentTime / time); 
			currentTime += Time.deltaTime;
			yield return null;
		} 
		while (currentTime <= time); 

		StartCoroutine (fade (fadeTime)); 
	}

	IEnumerator fade (float time) 
	{

		Color targetColor = img.color; 
		targetColor.a = 0; 
		float currentTime = 0.0f;

		do
		{ 
			img.color = Color.Lerp (Color.white, targetColor, currentTime/time);  
			currentTime += Time.deltaTime;
			yield return null;
		} 
		while (currentTime <= time); 

		Destroy(this.gameObject);
	}
}
