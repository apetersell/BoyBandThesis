using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextingManager : MonoBehaviour {

	public struct QuestionText 
	{
		public string sender;
		public string content;
		public int correctAnswer;
		public int index;
	}

	public struct ReplyText
	{
		public string sender;
		public string rightContent;
		public string wrongContent; 
		public int index;
	}

	public string[] JPQuestions;
	public string[] leeQuestions;

	public int [] JPCorrectAnswers;
	public int [] leeCorrectAnswers;

	public string[] JPRightContent;
	public string []leeRightContent;

	public string [] JPWrongContent;
	public string [] leeWrongContent;

	public List<QuestionText> JPTexts = new List<QuestionText> ();
	public List<QuestionText> LeeTexts = new List<QuestionText>();
	public List<ReplyText> JPReplies = new List<ReplyText> ();
	public List<ReplyText> LeeReplies = new List<ReplyText>();

	public static ShuffleBag <QuestionText> JPBag;
	public static ShuffleBag <QuestionText> LeeBag;

	public List<TextProperties> activeTexts = new List<TextProperties>();

	public bool chainStarted;
	bool timeToRespond;
	bool acceptingInputs;
	bool responded;
	bool chainEnded;
	public GameObject yes;
	public GameObject no;
	public GameObject textObject; 
	public GameObject aigText;
	public Transform personalCanvas;

	public int lookingFor;
	public int tryingThis;
	public int currentTextIndex;
	public string onTheLine;
	public int randomCallerIndex;

	float eventsTimer;

	GlobalManager globe;
	PhonyRestyManager prm;
	public float friendshipMod;


	// Use this for initialization
	void Start () 
	{
		makeTexts ();
		globe = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
		prm = GameObject.Find ("Manager").GetComponent<PhonyRestyManager> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (acceptingInputs) 
		{
			inputs ();
		}
		yes.SetActive (acceptingInputs);
		no.SetActive (acceptingInputs);

		if (!chainStarted) 
		{
			randomCallerIndex = Random.Range (0, 2);
			QuestionText newQuestionText = new QuestionText();
			if (whosTexting () == "J-Pe") 
			{
				newQuestionText = JPBag.Next ();
			}
			if (whosTexting () == "Lee") 
			{
				newQuestionText = LeeBag.Next ();
			}
			onTheLine = whosTexting ();
			GameObject newText = Instantiate (textObject) as GameObject; 
			newText.transform.SetParent (personalCanvas);
			TextProperties tp = newText.GetComponent<TextProperties> ();
			tp.content = newQuestionText.content;
			tp.owner = newQuestionText.sender;
			lookingFor = newQuestionText.correctAnswer;
			currentTextIndex = newQuestionText.index;
			activeTexts.Add (tp);
			chainStarted = true;
		}

		if (chainStarted) 
		{
			eventsTimer += Time.deltaTime;
		}

		if (eventsTimer >= 1 && !timeToRespond) 
		{
			timeToRespond = true;
			acceptingInputs = true;
		}

		if (eventsTimer >= 1 && responded && !chainEnded) 
		{
			eventsTimer = 0;
			foreach (TextProperties tp in activeTexts) 
			{
				tp.pos++;
			}
			ReplyText rt = new ReplyText ();
			if (onTheLine == "J-Pe") {
				for (int i = 0; i < JPReplies.Count; i++) {
					if (JPReplies [i].index == currentTextIndex) {
						rt = JPReplies [i];
					}
				}
				GameObject newText = Instantiate (textObject) as GameObject; 
				newText.transform.SetParent (personalCanvas);
				TextProperties newTP = newText.GetComponent<TextProperties> (); 
				if (checkAnswer ()) {
					newTP.content = rt.rightContent;
					globe.jPeRelationship += friendshipMod;
				} else {
					newTP.content = rt.wrongContent;
					globe.jPeRelationship -= friendshipMod;
					prm.frustrate (1);

				}
				newTP.owner = rt.sender;
				activeTexts.Add (newTP);
			} else {
				for (int i = 0; i < LeeReplies.Count; i++) {
					if (LeeReplies [i].index == currentTextIndex) {
						rt = LeeReplies [i];
					}
				}
				GameObject newText = Instantiate (textObject) as GameObject; 
				newText.transform.SetParent (personalCanvas);
				TextProperties newTP = newText.GetComponent<TextProperties> (); 
				if (checkAnswer ()) {
					newTP.content = rt.rightContent;
					globe.leeRelationship += friendshipMod;
				} else {
					newTP.content = rt.wrongContent;
					globe.leeRelationship -= friendshipMod;
					prm.frustrate (2);
				}
				newTP.owner = rt.sender;
				activeTexts.Add (newTP);
			}
			chainEnded = true;
		}

		if (eventsTimer >= 1.25f && chainEnded)
		{
			reset ();
		}
	}

	bool IsValidBag()
	{
		return JPBag != null && LeeBag != null; 
	}

	void makeTexts()
	{
		for (int i = 0; i < JPQuestions.Length; i++) 
		{
			QuestionText jpQ;
			QuestionText leeQ;
			ReplyText jpR;
			ReplyText leeR;

			jpQ.sender = "J-Pe";
			jpQ.content = JPQuestions [i];
			jpQ.correctAnswer = JPCorrectAnswers [i];
			jpQ.index = i;

			leeQ.sender = "Lee";
			leeQ.content = leeQuestions [i];
			leeQ.correctAnswer = leeCorrectAnswers [i];
			leeQ.index = i;

			jpR.sender = "J-Pe";
			jpR.rightContent = JPRightContent [i];
			jpR.wrongContent = JPWrongContent [i];
			jpR.index = i;

			leeR.sender = "Lee";
			leeR.rightContent = leeRightContent [i];
			leeR.wrongContent = leeWrongContent [i];
			leeR.index = i;

			JPTexts.Add (jpQ);
			JPReplies.Add (jpR);
			LeeTexts.Add (leeQ);
			LeeReplies.Add (leeR);
		}

		if (!IsValidBag())   
		{
			JPBag = new ShuffleBag<QuestionText> (); 
			for (int j = 0; j <JPTexts.Count; j++) 
			{
				JPBag.Add(JPTexts[j]);
			}

			LeeBag = new ShuffleBag<QuestionText> (); 
			for (int l = 0; l <LeeTexts.Count; l++) 
			{
				LeeBag.Add(LeeTexts[l]);
			}
		}
	}

	public string whosTexting ()
	{
		string result = null;
		if (globe.JPPresent && !globe.LeePresent) {
			result = "J-Pe";
		} 
		if (!globe.JPPresent && globe.LeePresent) {
			result = "Lee";
		} 
		if (globe.JPPresent && globe.LeePresent) 
		{
			if (randomCallerIndex == 0) {
				result = "J-Pe";
			} else {
				result = "Lee";
			}
		}

		return result;
	}

	void inputs ()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			eventsTimer = 0;
			foreach (TextProperties tp in activeTexts) 
			{
				tp.pos++;
			}
			GameObject response = Instantiate (aigText) as GameObject;
			response.transform.SetParent (personalCanvas);
			TextProperties newTp = response.GetComponent<TextProperties> ();
			newTp.yesno = 0;
			newTp.changeAigChoice ();
			activeTexts.Add (newTp);
			tryingThis = 0;
			acceptingInputs = false;
			responded = true;
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) 
		{
			eventsTimer = 0;
			foreach (TextProperties tp in activeTexts) 
			{
				tp.pos++;
			}
			GameObject response = Instantiate (aigText) as GameObject;
			response.transform.SetParent (personalCanvas);
			TextProperties newTp = response.GetComponent<TextProperties> ();
			newTp.yesno = 1;
			newTp.changeAigChoice ();
			activeTexts.Add (newTp);
			tryingThis = 1;
			acceptingInputs = false;
			responded = true;
		}

	}

	bool checkAnswer ()
	{
		if (tryingThis == lookingFor) {
			return true;
		} else {
			return false;
		}
	}

	void reset()
	{
		for (int i = 0; i < activeTexts.Count; i++) 
		{
			Destroy (activeTexts [i].gameObject);
		}
		activeTexts.Clear ();
		eventsTimer = 0;
		chainStarted = false;
		timeToRespond = false;
		acceptingInputs = false;
		responded = false;
		chainEnded = false;
	}
}
