using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitEffectUI : MonoBehaviour {

	public bool active;
	RectTransform rt;
	Image img;
	public float expandedScale;
	public float scaleTime;
	Vector3 originalScale;
	Color originalColor;

	// Use this for initialization
	void Start () {

		rt = GetComponent<RectTransform> ();
		img = GetComponent<Image> ();
		originalScale = transform.parent.gameObject.transform.localScale;
		originalColor = transform.parent.gameObject.GetComponent<Image> ().color;

	}

	// Update is called once per frame
	void Update () 
	{
		if (active) {
			StartCoroutine (scaleOverTime (scaleTime)); 
		} 
		else 
		{
			rt.localScale = originalScale;
			img.color = originalColor;
		}
		img.sprite = transform.parent.gameObject.GetComponent<Image> ().sprite;
	}

	IEnumerator scaleOverTime (float time)
	{
		
		Color targetColor = img.color; 
		targetColor.a = 0; 
		Vector3 targetScale = new Vector3 (expandedScale, expandedScale, expandedScale);
		float currentTime = 0.0f;

		do
		{
			GetComponent<RectTransform>().localScale = Vector3.Lerp(originalScale, targetScale, currentTime / time); 
			img.color = Color.Lerp (originalColor, targetColor, currentTime/time);  
			currentTime += Time.deltaTime;
			yield return null;
		} 
		while (currentTime <= time); 

		active = false;
	}
}