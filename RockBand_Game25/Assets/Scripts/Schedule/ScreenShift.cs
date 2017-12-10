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
	// Use this for initialization
	void Start () 
	{
	}
	
	public void OnPointerClick (PointerEventData eventData)
	{
		me.DOLocalMoveX (offScreen, speed, false);
		other.DOLocalMoveX (onScreen, speed, false);
	} 
}
