using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ScreenShift : MonoBehaviour, IPointerClickHandler
{
	public float onScreen;
	public float offScreen;
	public float speed;
	public Transform other;
	public Transform me;
	public ScreenShift otherSS;
	public bool active;
	public KeyCode myKey;
	// Use this for initialization
	void Start () 
	{
	}

	void Update ()
	{
		if (active) 
		{
			if (Input.GetKeyDown (myKey)) 
			{
				shiftScreen ();
			}
		}
	}
	
	public void OnPointerClick (PointerEventData eventData)
	{
		shiftScreen ();
	} 

	void shiftScreen ()
	{
		me.DOLocalMoveX (offScreen, speed, false);
		other.DOLocalMoveX (onScreen, speed, false);
		active = false;
		otherSS.active = true;
	}
}
