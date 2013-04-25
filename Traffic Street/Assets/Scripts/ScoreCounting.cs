using UnityEngine;
using System.Collections;

public class ScoreCounting : MonoBehaviour {
	
	public static float score = GameMaster.score;
	public float rating = score;
	float counter;
	
	// Use this for initialization
	IEnumerator Start () {
		for(float i=0; i<score; i = i+(rating/200) ){
				yield return new WaitForSeconds((float)3/score);
				gameObject.GetComponent<UILabel>().text = (int)i+"";
				Debug.Log("ggggggggggg");
			}
		gameObject.GetComponent<UILabel>().text = score+"";
		
		/*
		yield return new WaitForSeconds(.7f);
		
		float bonus = score/20;
		score += bonus;
		Debug.Log(score);
		rating = score;
		for(float i=score-bonus; i<score; i = i+1 ){
				yield return new WaitForSeconds(.005f);
				gameObject.GetComponent<UILabel>().text = (int)i+"";
				Debug.Log("ggggggggggg");
			}
		
		gameObject.GetComponent<UILabel>().text = score+"";
		*/
	}
	
	
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
