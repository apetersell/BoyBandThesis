using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

	Image img;
	GlobalManager gm; 
	public bool stressBar;
	public bool Dance;
	public bool Vocal;
	public bool PR;
	public bool Stress;
	public bool Relationship;
	public bool JP;
	public bool Lee;
	public bool Aig;
	public bool fanBar;

	// Use this for initialization
	void Start () 
	{
		img = GetComponent<Image> ();
		gm = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (stressBar) {
			if (Dance) {
				img.fillAmount = gm.DanceScore / StoryManager.statMilestone1;
			} else if (Vocal) {
				img.fillAmount = gm.VocalScore / StoryManager.statMilestone1;
			} else if (PR) {
				img.fillAmount = gm.PRScore / StoryManager.statMilestone1;
			}
		} else if (fanBar) {
			if (Aig) {
				img.fillAmount = gm.AigFans / StoryManager.fanMileStone1;
			} else if (JP) {
				img.fillAmount = (gm.AigFans + gm.JPFans) / StoryManager.fanMileStone1;
			} else if (Lee) {
				img.fillAmount = gm.totalFans / StoryManager.fanMileStone1;
			}
		}else {
			if (Dance) {
				img.fillAmount = gm.effectiveDance / StoryManager.statMilestone1;
			} else if (Vocal) {
				img.fillAmount = gm.effectiveVocal / StoryManager.statMilestone1;
			} else if (PR) {
				img.fillAmount = gm.effectivePR / StoryManager.statMilestone1;
			} else if (Relationship) {
				if (JP) {
					img.fillAmount = gm.jPeRelationship / 100;
				} else if (Lee) {
					img.fillAmount = gm.leeRelationship / 100;
				}
			} else if (Stress) {
				img.fillAmount = gm.Stress / StoryManager.statMilestone1;
			}
		}
	}
}
