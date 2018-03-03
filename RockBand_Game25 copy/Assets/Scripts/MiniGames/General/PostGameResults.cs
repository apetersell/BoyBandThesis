using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PostGameResults : MonoBehaviour 
{
	public Image danceBar;
	public Image vocalBar;
	public Image prBar;
	public Image stressBar;
	public Image danceStressBar;
	public Image vocalStressBar;
	public Image prStressBar;
	public Text addedDance;
	public Text addedVocal;
	public Text addedPR;
	public Text addedStress;
	public Text effectiveDance;
	public Text effectiveVocal;
	public Text effectivePR;
	public Text effectiveStress;
	public GameObject results;
	public GameObject stats;
	public GameObject addedStats;
	public GameObject addedStressDisplay; 
	public Image screen;
	public Text dateDisplay;
	GlobalManager globe;
	public float danceFillAmount;
	public float vocalFillAmount;
	public float prFillAmount;
	public float stressFillAmount;
	public float danceStressFill;
	public float vocalStressFill;
	public float prStressFill;
	public float fadeInTime;
	bool timerStart;
	public float timer;
	public float statOn;
	public float fillStats;
	public float addedStatsOn;
	public float stressDisplay; 
	public float fillStress;
	public float showFinalScores; 
	bool filledStatsStart;
	bool stressDisplayStart;
	bool fillStressStart;
	bool showFinalScoresStart;
	float danceGained;
	float vocalGained;
	float prGained;
	float stressGained;
	float displayDance;
	float displayVocal;
	float displayPR;
	string addedStressString;
	public GameObject[] totalScores;
	public GameObject continueButton;
	public GameObject continueText;

	// Use this for initialization
	void Start () 
	{
		globe = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
	}
	
	// Update is called once per frame
	void Update () 
	{
		dateDisplay.text = globe.SOWstring;
		if (Input.GetKeyDown (KeyCode.RightArrow)) 
		{
			if (showFinalScoresStart) {
				loadVN ();
			}
		}
		stringDisplays ();
		if (timerStart) 
		{
			timer += Time.deltaTime;
			timedEvents ();
		}
		if (stats.activeSelf) 
		{
			danceBar.fillAmount = danceFillAmount / StoryManager.statMeterFull;
			vocalBar.fillAmount = vocalFillAmount / StoryManager.statMeterFull;
			prBar.fillAmount = prFillAmount / StoryManager.statMeterFull; 
			vocalBar.fillAmount = vocalFillAmount / StoryManager.statMeterFull; 
			stressBar.fillAmount = stressFillAmount / StoryManager.statMeterFull;
			danceStressBar.fillAmount = danceStressFill / StoryManager.statMeterFull;
			vocalStressBar.fillAmount = vocalStressFill / StoryManager.statMeterFull;
			prStressBar.fillAmount = prStressFill / StoryManager.statMeterFull;
		}
	}

	void stringDisplays()
	{
		if (danceGained > 0) {
			addedDance.text = "+" + Mathf.Round (danceGained).ToString ();
		} else {
			addedDance.text = "";
		}
		if (vocalGained > 0) {
			addedVocal.text = "+" + Mathf.Round (vocalGained).ToString ();
		} else {
			addedVocal.text = "";
		}
		if (prGained > 0) {
			addedPR.text = "+" + Mathf.Round (prGained).ToString ();
		} else {
			addedPR.text = "";
		}
		addedStress.text = addedStressString;
		if (stressGained > 0) {
			addedStressString = "+" + Mathf.Round (stressGained).ToString ();
		} else if (stressGained < 0) {
			addedStressString = Mathf.Round (stressGained).ToString ();
		} else {
			addedStressString = "";
		}
		effectiveDance.text = Mathf.Round (displayDance).ToString () + "<color=#F49AC1FF> (-" + Mathf.Round(stressFillAmount) + ")</color>";
		effectiveVocal.text = Mathf.Round (displayVocal).ToString () + "<color=#F49AC1FF> (-" + Mathf.Round(stressFillAmount) + ")</color>";
		effectivePR.text = Mathf.Round (displayPR).ToString () + "<color=#F49AC1FF> (-" + Mathf.Round(stressFillAmount) + ")</color>";
		effectiveStress.text = Mathf.Round (stressFillAmount).ToString ();
	}

	public void startPostGame()
	{
		screen.DOFade (.85f, fadeInTime).OnComplete(displayStats);
		danceFillAmount = globe.SOWEDance;
		displayDance = globe.SOWdance;
		vocalFillAmount = globe.SOWEvocal;
		displayVocal = globe.SOWvocal;
		prFillAmount = globe.SOWEpr;
		displayPR = globe.SOWpr;
		stressFillAmount = globe.SOWstress;
		danceStressFill = globe.SOWdance;
		vocalStressFill = globe.SOWvocal;
		prStressFill = globe.SOWpr;
		danceGained = globe.DanceScore - globe.SOWdance;
		vocalGained = globe.VocalScore - globe.SOWvocal;
		prGained = globe.PRScore - globe.SOWpr;
		stressGained = globe.Stress - globe.SOWstress;
	}

	void displayStats()
	{
		results.SetActive (true);
		stressFillAmount = globe.SOWstress;
		timerStart = true;
	}

	void timedEvents()
	{
		if (timer > statOn) 
		{
			stats.SetActive (true);
		}
		if (timer > addedStatsOn) 
		{
			if (!addedStats.activeSelf) 
			{
				for (int i = 0; i < addedStats.transform.childCount; i++) 
				{
					ScaleEffect se = addedStats.transform.GetChild (i).GetComponent<ScaleEffect>();
					se.scaleEffect ();
				}
			}
			addedStats.SetActive (true);
		}
		if (timer > fillStats && !filledStatsStart) 
		{
			DOTween.To(()=>danceGained, x=> danceGained = x, 0, 1);
			DOTween.To(()=>vocalGained, x=> vocalGained = x, 0, 1);
			DOTween.To(()=>prGained, x=> prGained = x, 0, 1);
			DOTween.To(()=>danceFillAmount, x=> danceFillAmount = x, globe.DanceScore - globe.SOWstress, 1);
			DOTween.To(()=>vocalFillAmount, x=> vocalFillAmount = x, globe.VocalScore - globe.SOWstress, 1);
			DOTween.To(()=>prFillAmount, x=> prFillAmount = x, globe.PRScore - globe.SOWstress, 1);
			DOTween.To(()=>displayDance, x=> displayDance = x, globe.DanceScore, 1);
			DOTween.To(()=>displayVocal, x=> displayVocal = x, globe.VocalScore, 1);
			DOTween.To(()=>displayPR, x=> displayPR = x, globe.PRScore, 1);
			DOTween.To(()=>danceStressFill, x=> danceStressFill = x, globe.DanceScore, 1);
			DOTween.To(()=>vocalStressFill, x=> vocalStressFill = x, globe.VocalScore, 1);
			DOTween.To(()=>prStressFill, x=> prStressFill = x, globe.PRScore, 1);
			filledStatsStart = true;
		}

		if (timer > stressDisplay) 
		{
			if (!addedStressDisplay.activeSelf) 
			{
				addedStressDisplay.gameObject.GetComponent<ScaleEffect> ().scaleEffect ();
			}
			addedStressDisplay.SetActive (true);

		}

		if (timer > fillStress && !fillStressStart) 
		{
			fillStressStart = true;
			displayStress ();
		}

		if (timer > showFinalScores && !showFinalScoresStart) 
		{
			for (int i = 0; i < totalScores.Length; i++) 
			{
				totalScores [i].SetActive (true);
				totalScores [i].GetComponent<ScaleEffect> ().scaleEffect ();
				if (i == 0) {
					totalScores [i].GetComponent<Text> ().text = Mathf.Round(globe.effectiveDance).ToString();
				} else if (i == 1) {
					totalScores [i].GetComponent<Text> ().text = Mathf.Round (globe.effectiveVocal).ToString();
				} else {
					totalScores [i].GetComponent<Text> ().text = Mathf.Round(globe.effectivePR).ToString();
				}
			}
			continueButton.SetActive (true);
			continueText.SetActive(true);
			showFinalScoresStart = true;
		}
	}
	
	public void loadVN() 
	{
		globe.loadVNScene ();
	}
	void displayStress()
	{
		DOTween.To(()=>stressFillAmount, x=> stressFillAmount = x, globe.Stress, 1);
		DOTween.To(()=>stressGained, x=> stressGained = x, 0, 1);
		DOTween.To(()=>danceFillAmount, x=> danceFillAmount = x, globe.effectiveDance, 1);
		DOTween.To(()=>vocalFillAmount, x=> vocalFillAmount = x, globe.effectiveVocal, 1);
		DOTween.To(()=>prFillAmount, x=> prFillAmount = x, globe.effectivePR, 1);
	}
}
