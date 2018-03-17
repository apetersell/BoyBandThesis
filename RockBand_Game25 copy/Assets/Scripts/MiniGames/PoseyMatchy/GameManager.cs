using System.Collections;
using System.Collections.Generic;
using Beat;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour {

	public bool cheatingMode;
	public int playerPose = 0;
	public int choreoPose = 1;
	public Sprite good;
	public Sprite bad;
	public static ShuffleBag<int> poseBag; 
	public float currentTimer;
	public float maxTimer;
	public float gameTimerMax;
	public float gameTimer;
	float valueOfMatch;
	public Animator player;
	Image goodBad;
	public Animator jPeAnim;
	public Animator leeAnim;
	public Animator choreographer; 
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
	bool up; 
	bool down; 
	bool left; 
	bool right; 
	bool upC; 
	bool downC; 
	bool leftC; 
	bool rightC; 


	void Awake () 
	{
	  clock = Clock.Instance;
      clock.SetBPM(100);
      maxTimer = clock.MeasureLength();

		if (!IsValidBag())  
		{
			poseBag = new ShuffleBag<int> (); 
			for (int i = 1; i < 5; i++) 
			{
				poseBag.Add (i);
			}
		}
//		gameTimerMax = GameObject.Find ("MusicMan").GetComponent<AudioSource> ().clip.length;
//		gameTimer = GameObject.Find ("MusicMan").GetComponent<AudioSource> ().clip.length;

		jPeAnim = GameObject.Find ("obj_JP").GetComponent<Animator> ();
		leeAnim = GameObject.Find ("obj_Lee").GetComponent<Animator> ();
	}
		
	void Start () 
	{
		sm = GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ();
		globe = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
		player = GameObject.Find ("Aig-mini-dance").GetComponent<Animator> ();
		choreographer = GameObject.Find ("Choreographer").GetComponent<Animator> ();
		timeMeter = GameObject.Find ("TimeMeter").GetComponent<Image> ();
		goodBad = GameObject.Find ("GoodBad").GetComponent<Image> ();
		endText = GameObject.Find ("EndText");
		scoreBoard = GameObject.Find ("ScoreBoard");
		GetNewPose ();
	}

	void Update () 
	{
		handleAnimations ();
		gameTimer -= Time.deltaTime;
		if (playerPose == choreoPose) 
		{
			goodBad.sprite = good;
		} 
		else 
		{
			goodBad.sprite = bad;
		}
		if (theEnd == false) 
		{
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
		choreoPose = poseBag.Next ();
		if (choreoPose == 1) 
		{
			upC = true;
			downC = false;
			rightC = false;
			leftC = false;
		}
		if (choreoPose == 2) 
		{
			upC = false;
			downC = true;
			rightC = false;
			leftC = false;
		}
		if (choreoPose == 3) 
		{
			upC = false;
			downC = false;
			rightC = true;
			leftC = false;
		}
		if (choreoPose == 4) 
		{
			upC = false;
			downC = false;
			rightC = false;
			leftC = true;
		}
		choreographer.SetTrigger ("doPose");
		jPeAnim.SetTrigger ("doPose");
		leeAnim.SetTrigger ("doPose");
	}

	void playerInput ()
	{
		if (Input.GetKeyDown (KeyCode.RightArrow)) 
		{
			changePlayerPose (3);
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) 
		{
			changePlayerPose (2);
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) 
		{
			changePlayerPose (4);
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) 
		{
			changePlayerPose (1);
		}
	}

	void changePlayerPose(int sent)
	{
		player.SetTrigger ("doPose");
		playerPose = sent;
		if (sent == 1) 
		{
			up = true;
			down = false;
			right = false;
			left = false;
		}
		if (sent == 2) 
		{
			up = false;
			down = true;
			right = false;
			left = false;
		}
		if (sent == 3) 
		{
			up = false;
			down = false;
			right = true;
			left = false;
		}
		if (sent == 4) 
		{
			up = false;
			down = false;
			right = false;
			left = true;
		}
	}

	void handleTimer()
	{
		currentTimer = currentTimer - Time.deltaTime;
		timeMeter.fillAmount = currentTimer / maxTimer;
		if (currentTimer <= 0) 
		{
			if (playerPose == choreoPose) 
			{
				player.SetTrigger ("Hit");
				sm.scorePoints(true);
			} 
			else 
			{
				player.SetTrigger ("Miss");
				sm.scorePoints (false);
			}
			GetNewPose ();
			currentTimer = maxTimer;
			up = false;
			down = false;
			right = false;
			left = false;
			playerPose = 0;
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

	void handleAnimations ()
	{
		player.SetBool ("Up", up);
		player.SetBool ("Down", down);
		player.SetBool ("Right", right);
		player.SetBool ("Left", left);

		choreographer.SetBool ("Up", upC);
		choreographer.SetBool ("Down", downC);
		choreographer.SetBool ("Right", rightC);
		choreographer.SetBool ("Left", leftC);

		jPeAnim.SetBool ("Up", upC);
		jPeAnim.SetBool ("Down", downC);
		jPeAnim.SetBool ("Right", rightC);
		jPeAnim.SetBool ("Left", leftC);

		leeAnim.SetBool ("Up", upC);
		leeAnim.SetBool ("Down", downC);
		leeAnim.SetBool ("Right", rightC);
		leeAnim.SetBool ("Left", leftC);
	
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
