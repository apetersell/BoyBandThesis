using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LyricGenerator : MonoBehaviour {

	public string [] lyrics;
	public string [] answers;
	public int wordsPerRound;
	public List<GameObject> wordBank = new List<GameObject> ();
	ShuffleBag<int> indexes = new ShuffleBag<int>();
	List <int> wordsthisRound = new List <int>();
	ShuffleBag<int>currentRoundInts = new ShuffleBag<int> ();
	public int currentIndex;
	public int trying;
	public int selectedIndex;
	List<int> column1 = new List<int> ();
	List<int> column2 = new List<int> ();
	List<int> column3 = new List <int> ();
	List<int> currentColumn;
	Text currentLyric;
	int rowIndex;
	int columnIndex;
	ScoreManager sm;
	int leftThisRound;

	public Image img;
	public float maxTimer;
	float timer;

	// Use this for initialization
	void Start () 
	{
		sm = GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ();
		currentLyric = GetComponent<Text> ();
		for (int i = 0; i < lyrics.Length; i++) 
		{
			indexes.Add (i);
		}
		for (int i = 0; i < wordsPerRound; i++) 
		{
			GameObject word = GameObject.Find ("Word_" + i);
			wordBank.Add (word);
			word.GetComponent<wordBankOption> ().index = i;
			if (i < 3) {
				column1.Add (i);
			} else if (i >= 3 && i < 6) {
				column2.Add (i);
			} else {
				column3.Add (i);
			}
		}
		setUpNewRound ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentLyric.text = lyrics [currentIndex];
		musicStaff ();
		controls ();
		trying = wordsthisRound [selectedIndex];
	}

	void setUpNewRound ()
	{
		wordsthisRound.Clear ();
		currentRoundInts.Clear ();
		leftThisRound = wordsPerRound;
		for (int i = 0; i < wordsPerRound; i++) 
		{
			int newInt = indexes.Next ();
			wordsthisRound.Add (newInt);
			currentRoundInts.Add (newInt);
			wordBank [i].GetComponent<Text> ().text = answers [newInt];
		}
		for (int i = 0; i < wordBank.Count; i++) 
		{
			wordBank [i].GetComponent<wordBankOption> ().alreadyUsed = false;
		}
		setUpNewLyric ();
	}

	void setUpNewLyric ()
	{
		currentIndex = currentRoundInts.Next ();
	}

	void musicStaff ()
	{
		timer += Time.deltaTime;
		if (timer >= maxTimer) 
		{
			timer = 0;
			if (trying == currentIndex) {
				sm.scorePoints (true);
				wordBank [selectedIndex].GetComponent<wordBankOption> ().alreadyUsed = true;
			} else {
				sm.scorePoints (false);
			}
			leftThisRound--;
			if (leftThisRound > 0) 
			{
				setUpNewLyric ();
			} else {
				setUpNewRound ();
			}
		}
		img.fillAmount = timer / maxTimer;
	}

	void controls ()
	{
		if (columnIndex == 0) 
		{
			currentColumn = column1;
		}
		if (columnIndex == 1) 
		{
			currentColumn = column2;
		}

		if (columnIndex == 2) 
		{
			currentColumn = column3;
		}

		selectedIndex = currentColumn [rowIndex];

		if (Input.GetKeyDown (KeyCode.RightArrow)) 
		{
			columnIndex++;
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) 
		{
			columnIndex--;
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) 
		{
			rowIndex--;
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) 
		{
			rowIndex++;
		}

		if (columnIndex > 2) 
		{
			columnIndex = 0;
		}
		if (columnIndex < 0) 
		{
			columnIndex = 2;
		}
		if (rowIndex > 2) 
		{
			rowIndex = 0;
		}
		if (rowIndex < 0) 
		{
			rowIndex = 2;
		}
	}


}
