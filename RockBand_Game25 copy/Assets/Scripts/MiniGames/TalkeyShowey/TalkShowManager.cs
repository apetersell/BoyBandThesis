using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkShowManager : MonoBehaviour {

	public Sprite[] calls;
	public Sprite[] responses;
	public Sprite[] hostSprites;
	public Sprite[] aigSprites;
	public Image host; 
	public Image aig; 
	public static ShuffleBag <int> numBag;
	public GameObject[] hostBubbles; 
	public GameObject[] aigBubbles; 
	public Image[] aigThoughts;
	float timer;
	int A; 
	int B; 
	int C; 
	int D; 
	int aigUp; 
	int aigDown; 
	int aigLeft; 
	int aigRight; 
	public bool [] steps;
	bool canRespond;
	bool end;
	float endTimer;
	public int responseIndex = 0;
	public float applauseTime;
	List<int> temp = new List<int>();
	public int[] current;
	ScoreManager sm;
	public GameObject[] timers;
	Image currentTimer;
	public float maxTimeToRespond; 
	float answerTimer; 
	public Sprite good;
	public Sprite bad;

	void Awake () 
	{
		if (!IsValidBag())  
		{
			numBag = new ShuffleBag<int> (); 
			for (int i = 0; i < calls.Length; i++) 
			{
				numBag.Add(i);
			}
		}
	}

	// Use this for initialization
	void Start () 
	{
		for (int i = 0; i < aigThoughts.Length; i++) 
		{
			aigThoughts [i].color = Color.clear;
		}
		sm = GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (end) 
		{
			endTimer += Time.deltaTime;
			if (endTimer >= applauseTime) 
			{
				reset ();
			}
		}
		if (canRespond) 
		{
			inputAnswer ();
		}

		timer += Time.deltaTime;
		if (timer > 0 && !steps [0]) 
		{
			A = numBag.Next ();
			temp.Add (A);
			current [0] = A;
			B = numBag.Next ();
			current [1] = B;
			temp.Add (B);
			C = numBag.Next ();
			current [2] = C;
			temp.Add (C);
			D = numBag.Next ();
			temp.Add (D);
			assignInputs ("Up");
			assignInputs ("Down");
			assignInputs ("Left");
			assignInputs ("Right");
			aigThoughts [0].sprite = responses [aigUp];
			aigThoughts [1].sprite = responses [aigDown];
			aigThoughts [2].sprite = responses [aigLeft];
			aigThoughts [3].sprite = responses [aigRight];
			for (int i = 0; i < aigThoughts.Length; i++) 
			{
				aigThoughts [i].color = Color.white;
			}
			hostBubbles [0].SetActive (true);
			hostBubbles [0].transform.GetChild (0).GetComponent<Image> ().sprite = calls[A];
			steps [0] = true;
		}
		if (timer > 1 && !steps [1]) 
		{
			hostBubbles [1].SetActive (true);
			hostBubbles [1].transform.GetChild (0).GetComponent<Image> ().sprite = calls[B];
			steps [1] = true;
		}
		if (timer > 2 && !steps [2]) 
		{
			hostBubbles [2].SetActive (true);
			hostBubbles [2].transform.GetChild (0).GetComponent<Image> ().sprite = calls[C];
			steps [2] = true;
			canRespond = true;
		}
	}
	bool IsValidBag()
	{
		return numBag != null; 
	}

	void assignInputs (string sent)
	{
		if (sent == "Up") 
		{
			int rando = Random.Range (0, temp.Count);
			aigUp = temp[rando]; 
			temp.Remove (temp[rando]);
		}

		if (sent == "Down") 
		{
			int rando = Random.Range (0, temp.Count);
			aigDown = temp[rando]; 
			temp.Remove (temp[rando]);
		}

		if (sent == "Left") 
		{
			int rando = Random.Range (0, temp.Count);
			aigLeft = temp[rando]; 
			temp.Remove (temp[rando]);
		}

		if (sent == "Right") 
		{
			aigRight = temp[0]; 
			temp.Clear ();
		}
	}

	void inputAnswer ()
	{
		if (responseIndex < 3) 
		{
			currentTimer = timers [responseIndex].GetComponent<Image> ();
			currentTimer.gameObject.SetActive (true);
			currentTimer.fillAmount = answerTimer / maxTimeToRespond;

			if (Input.GetKeyDown (KeyCode.UpArrow)) 
			{
				aigBubbles [responseIndex].SetActive (true);
				aigBubbles [responseIndex].transform.GetChild (0).GetComponent<Image> ().sprite = responses [aigUp];
				aig.sprite = aigSprites [1];
				if (aigUp == current [responseIndex]) 
				{
					sm.scorePoints (true);
					currentTimer.transform.GetChild (0).GetComponent<Image> ().color = Color.white;
					currentTimer.transform.GetChild (0).GetComponent<Image> ().sprite = good;
					answerTimer = 0;
					responseIndex++;
				} else {
					sm.scorePoints (false);
					currentTimer.transform.GetChild (0).GetComponent<Image> ().color = Color.white;
					currentTimer.transform.GetChild (0).GetComponent<Image> ().sprite = bad;
					holdForAudience ("Wrong");
				}
			}
			if (Input.GetKeyDown (KeyCode.DownArrow)) 
			{
				aigBubbles [responseIndex].SetActive (true);
				aigBubbles [responseIndex].transform.GetChild (0).GetComponent<Image> ().sprite = responses [aigDown];
				aig.sprite = aigSprites [1];
				if (aigDown == current [responseIndex]) {
					sm.scorePoints (true);
					currentTimer.transform.GetChild (0).GetComponent<Image> ().color = Color.white;
					currentTimer.transform.GetChild (0).GetComponent<Image> ().sprite = good;
					answerTimer = 0;
					responseIndex++;
				} else {
					sm.scorePoints (false);
					currentTimer.transform.GetChild (0).GetComponent<Image> ().color = Color.white;
					currentTimer.transform.GetChild (0).GetComponent<Image> ().sprite = bad;
					holdForAudience ("Wrong");
				}
			}
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				aigBubbles [responseIndex].SetActive (true);
				aigBubbles [responseIndex].transform.GetChild (0).GetComponent<Image> ().sprite = responses [aigLeft];
				aig.sprite = aigSprites [1];
				if (aigLeft == current [responseIndex]) {
					sm.scorePoints (true);
					currentTimer.transform.GetChild (0).GetComponent<Image> ().color = Color.white;
					currentTimer.transform.GetChild (0).GetComponent<Image> ().sprite = good;
					answerTimer = 0;
					responseIndex++;
				} else {
					sm.scorePoints (false);
					currentTimer.transform.GetChild (0).GetComponent<Image> ().color = Color.white;
					currentTimer.transform.GetChild (0).GetComponent<Image> ().sprite = bad;
					holdForAudience ("Wrong");
				}
			}
			if (Input.GetKeyDown (KeyCode.RightArrow)) 
			{
				aigBubbles [responseIndex].SetActive (true);
				aigBubbles [responseIndex].transform.GetChild (0).GetComponent<Image> ().sprite = responses [aigRight];
				aig.sprite = aigSprites [1];
				if (aigRight == current [responseIndex]) {
					sm.scorePoints (true);
					responseIndex++;
				} else {
					sm.scorePoints (false);
					holdForAudience ("Wrong");
				}
			}
		}

		if (responseIndex == 3) 
		{
			holdForAudience ("Applause");
		}

		answerTimer += Time.deltaTime;
		if (answerTimer >= maxTimeToRespond) 
		{
			answerTimer = 0;
			holdForAudience ("Wrong");
		}
	}

	void holdForAudience (string sent)
	{
		canRespond = false;
		end = true;
		if (sent == "Applause") 
		{
			host.sprite = hostSprites [1];
		} 
		else 
		{
			host.sprite = hostSprites [2];
		}
	}

	void reset ()
	{
		for (int i = 0; i < timers.Length; i++) 
		{
			timers [i].transform.GetChild (0).GetComponent<Image> ().color = Color.clear;
			timers [i].SetActive (false);
		}
		for (int i = 0; i < aigThoughts.Length; i++) 
		{
			aigThoughts [i].color = Color.clear;
		}
		answerTimer = 0;
		end = false;
		endTimer = 0;
		host.sprite = hostSprites [0];
		aig.sprite = aigSprites [0];
		responseIndex = 0;
		for (int i = 0; i < steps.Length; i++) 
		{
			steps [i] = false;
		}
		for (int i = 0; i < hostBubbles.Length; i++) 
		{
			hostBubbles [i].SetActive (false);
		}
		for (int i = 0; i < hostBubbles.Length; i++) 
		{
			aigBubbles [i].SetActive (false);
		}
		timer = 0;
	}
}
