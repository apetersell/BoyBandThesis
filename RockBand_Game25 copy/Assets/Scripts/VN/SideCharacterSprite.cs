using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using DG.Tweening;

public class SideCharacterSprite : CharacterSpriteController
{
	public override void Awake (){}
	public override void changeOutfit (string outfit){}

	public Sprite [] expressions;
	[YarnCommand("expression")]
	public override void changeExpression (string sent)
	{
		if (sent == "Neutral") 
		{
			img.sprite = expressions [0];
		}
		if (sent == "Happy") 
		{
			img.sprite = expressions [1];
		}

		if (sent == "Angry") 
		{
			img.sprite = expressions [2];
		}

		if (sent == "Smile") 
		{
			img.sprite = expressions [3];
		}

		if (sent == "Surprise") 
		{
			img.sprite = expressions [4];
		}
	}
}