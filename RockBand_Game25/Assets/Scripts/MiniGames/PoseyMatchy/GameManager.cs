using System.Collections;
using System.Collections.Generic;
using Beat;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour {

	public bool cheatingMode;
	public Sprite [] poses;
	public Sprite good;
	public Sprite bad;
	public static ShuffleBag<Sprite> poseBag; 
	public Sprite currentPose;
	public float currentTimer;
	public float maxTimer;
	public float gameTimerMax;
	public float gameTimer;
	float valueOfMatch;
	public Image player;
	Image goodBad;
	public Image [] bandMates;
	public Image choreographer; 
	Image timeMeter;
	public float firstSpeedUp;
	public float secondSpeedUp;
	public float firstSpeed;
	public float secondSpeed;
	public float thirdSpeed;
	public float speedUpTextLife;
	public float endTime;
	bool theEnd = false;
	GameObject endText;
	GameObject scoreBoard;
	ScoreManager sm;
	GlobalManager globe;
	int bandPose;
   Beat.Clock clock;


	void Awake () 
	{
	   clock = Clock.Instance;
      clock.SetBPM(100);
      maxTimer = clock.MeasureLength();

		if (!IsValidBag())  
		{
			poseBag = new ShuffleBag<Sprite> (); 
			for (int i = 0; i < poses.Length; i++) 
			{
				poseBag.Add (poses [i]);
			}
		}
//		gameTimerMax = GameObject.Find ("MusicMan").GetComponent<AudioSource> ().clip.length;
//		gameTimer = GameObject.Find ("MusicMan").GetComponent<AudioSource> ().clip.length;
	}
		
	void Start () 
	{
		sm = GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ();
		globe = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
		player = GameObject.Find ("PlayerIcon").GetComponent<Image> ();
		choreographer = GameObject.Find ("Choreographer").GetComponent<Image> ();
		timeMeter = GameObject.Find ("TimeMeter").GetComponent<Image> ();
		goodBad = GameObject.Find ("GoodBad").GetComponent<Image> ();
		endText = GameObject.Find ("EndText");
		scoreBoard = GameObject.Find ("ScoreBoard");
	   
		for (int i = 0; i < bandMates.Length; i++) 
		{
			bandMates[i] = GameObject.Find("BandMate_" + i).GetComponent<Image>();
		}
	}

	void Update () 
	{
		if (currentPose == poses [0]) 
		{
			bandPose = 0;
		}
		if (currentPose == poses [1]) 
		{
			bandPose = 1;
		}
		if (currentPose == poses [2]) 
		{
			bandPose = 2;
		}
		if (currentPose == poses [3]) 
		{
			bandPose = 3;
		}
		for (int i = 0; i < bandMates.Length; i++) 
		{
			bandMates [i].sprite = bandMates [i].gameObject.GetComponent<BandMatePosey> ().myPoses [bandPose];
		}
		if (cheatingMode) 
		{
			player.sprite = choreographer.sprite;
		}
		gameTimer -= Time.deltaTime;
		if (currentPose != null) {
			choreographer.sprite = currentPose;
//			for (int i = 0; i < bandMates.Length; i++) {
//				bandMates [i].sprite = currentPose;
//			}
			if (player.sprite == choreographer.sprite) {
				goodBad.sprite = good;
			} else {
				goodBad.sprite = bad;
			}
		}
		if (theEnd == false) {
			playerInput ();
			handleTimer ();
		}
		SpeedUp ();
//		handleEnding ();
	}

	bool IsValidBag()
	{
		return poseBag != null; 
	}

	void GetNewPose()
	{
		currentPose = poseBag.Next ();
	}

	void playerInput ()
	{
		if (Input.GetKeyDown (KeyCode.RightArrow)) 
		{
			changePlayerPose (0);
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) 
		{
			changePlayerPose (1);
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) 
		{
			changePlayerPose (2);
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) 
		{
			changePlayerPose (3);
		}
	}

	void changePlayerPose(int sent)
	{
		player.sprite = poses [sent];
	}

	void handleTimer()
	{
		currentTimer = currentTimer - Time.deltaTime;
		timeMeter.fillAmount = currentTimer / maxTimer;
		if (currentTimer <= 0) 
		{
			if (player.sprite == choreographer.sprite) {
				sm.scorePoints(true);
				GameObject.Find ("PlayerIcon").transform.GetChild (0).GetComponent<HitEffectUI> ().active = true;
				choreographer.transform.GetChild (0).GetComponent<HitEffectUI> ().active = true;
				for (int i = 0; i < bandMates.Length; i++) 
				{
					bandMates [i].transform.GetChild (0).GetComponent<HitEffectUI> ().active = true;
				}
			} else {
				sm.scorePoints (false);
			}
			GetNewPose ();
			currentTimer = maxTimer;
		}
	}

	void SpeedUp ()
	{
		if (gameTimer > firstSpeedUp) {
			maxTimer = clock.HalfLength();
		} else if (gameTimer <= firstSpeedUp && gameTimer > secondSpeedUp) {
			maxTimer = clock.QuarterLength();
		} else
		{
		   maxTimer = clock.MeasureLength();
		}

		if (gameTimer <= firstSpeedUp + 1 && gameTimer > firstSpeedUp) 
		{
			activateSpeedText ();
		}

		if (gameTimer <= secondSpeedUp + 1 && gameTimer > secondSpeedUp) 
		{
			activateSpeedText ();
		}
	}

	void activateSpeedText()
	{
		GameObject.Find ("SpeedUp!").GetComponent<CoolText> ().wakeUp(speedUpTextLife);
	}

//	void handleEnding ()
//	{
//		if (gameTimer < endTime) {
//			theEnd = true;
//		} else {
//			theEnd = false;
//		}
//
//		if (theEnd) {
//			endText.SetActive (true);
//			scoreBoard.SetActive (false);
//			if (Input.GetKeyDown(KeyCode.R))
//			{
//				SceneManager.LoadScene ("PoseyMatchy");
//			}
//		} else {
//			endText.SetActive (false);
//			scoreBoard.SetActive (true);
//		}
//	}
}
