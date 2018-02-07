using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleEffect : MonoBehaviour {

	public float scaleSize;
	public float scaleSpeed;
	float originalScale;

	void Awake ()
	{
		originalScale = transform.localScale.x; 
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void scaleEffect()
	{
		transform.DOScale (scaleSize, scaleSpeed).OnComplete (scaleDown);
	}

	public void scaleDown()
	{
		transform.DOScale (originalScale, scaleSpeed);
	}
}
