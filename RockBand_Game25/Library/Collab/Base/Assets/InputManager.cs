using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
, IPointerDownHandler // 2
, IPointerUpHandler
, IPointerEnterHandler
, IPointerExitHandler
	// ... And many more available!
{
	Image sprite;
	Color savedColor;
	public static Color currentColor = Color.white;
	public static bool isIncluding = false;

	void Awake()
	{
		savedColor = Color.white;
		sprite = GetComponent<Image>();
	}

	void Update()
	{
//		if (sprite)
//			sprite.color = Vector4.MoveTowards(sprite.color, target, Time.deltaTime * 10);
	}

	public void OnPointerDown(PointerEventData eventData) // 3
	{
		print("I was clicked");
		//currentColor = Color.yellow;
		//targetColor = currentColor;
		sprite.color = currentColor;
		savedColor = currentColor;
		isIncluding = true;
		//targetColor = currentColor;
	}

	public void OnPointerUp(PointerEventData eventData) // 3
	{
		print("I was clicked");
		isIncluding = false;
		sprite.color = currentColor;
		savedColor = currentColor;
	}

//	public void OnDrag(PointerEventData eventData)
//	{
//		print("I'm being dragged!");
//		target = Color.magenta;
//	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if(isIncluding){
			sprite.color = currentColor;
			savedColor = currentColor;
		}else{
			sprite.color = Color.grey;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if(!isIncluding){
			sprite.color = savedColor;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	



}
