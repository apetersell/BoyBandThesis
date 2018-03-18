using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhonyRestyManager : MonoBehaviour {

	public Transform thumb;
	public Vector3[] thumbPositions;
	public Text display;
	GlobalManager globe;
	public float stressDecreaseRate;
	public bool frustrated;
	public float maxTimeFrustrated;
	float frustrationTimer;
	public GameObject frustrationEffect;
	public GameObject texting;
	public GameObject hattyCatchy; 
	public bool testing;
	float testTimer;
	Animator anim;

	// Use this for initialization
	void Start () 
	{
		globe = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
		thumbPositions [0] = thumb.transform.localPosition;
		anim = GameObject.Find ("Aig-mini-rest").GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		anim.SetBool ("Frustrated", frustrated);
		chooseGame ();
		display.text = "Stress: " + Mathf.Round(globe.Stress).ToString();
		thumbPlacement ();
		if (!frustrated && !globe.isStopped) 
		{
			globe.Stress -= stressDecreaseRate * Time.deltaTime;
		}

		if (frustrated) 
		{
			frustrationTimer += Time.deltaTime;
		}

		if (frustrationTimer >= maxTimeFrustrated) 
		{
			frustrated = false;
			frustrationTimer = 0;
		}

		if (testing) 
		{
			testTimer += Time.deltaTime;
			if (testTimer >= 12) 
			{
				if (globe.JPPresent == true && globe.LeePresent == true) 
				{
					globe.JPPresent = false;
					globe.LeePresent = false;

				} else {
					globe.JPPresent = true;
					globe.LeePresent = true;
				}
				testTimer = 0;
			}
		}
	}

	void thumbPlacement ()
	{
		if (Input.GetKey (KeyCode.LeftArrow)) {
			thumb.transform.localPosition = thumbPositions [1];
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			thumb.transform.localPosition = thumbPositions [2];
		} else {
			thumb.transform.localPosition = thumbPositions [0];
		}
	}

	public void chooseGame ()
	{
		if (!texting.GetComponent<TextingManager> ().chainStarted) 
		{
			if (globe.LeePresent || globe.JPPresent) {
				texting.SetActive (true);
				hattyCatchy.SetActive (false);
			} else {
				hattyCatchy.SetActive (true);
				texting.SetActive (false);
			}
		}
	}
	public void frustrate (int sent)
	{
		GameObject effect = Instantiate (frustrationEffect) as GameObject;
		FrustrationEffect fe = effect.GetComponent<FrustrationEffect> ();
		Animator animeEffect = effect.transform.GetChild (0).GetComponent<Animator> ();
		anim.SetTrigger ("Miss");
		if (sent == 0) 
		{
			anim.SetBool ("Drop", true);
		}
		fe.spriteIndex = sent;
		frustrated = true;
		frustrationTimer = 0;
	}
}
