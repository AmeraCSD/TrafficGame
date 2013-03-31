using UnityEngine;
using System.Collections;

public class SatisfyBar : MonoBehaviour {
	public int maxValue = 10;
	public int curValue = 0;
	
	public float barLength;
	
	// Use this for initialization
	void Start () {
		barLength = 0 ;
	}
	
	// Update is called once per frame
	void Update () {
	  AddjustSatisfaction(0);
	}
	
	void OnGUI(){
		GUI.Box(new Rect(10, 70, barLength, 20), curValue+ "/" + maxValue);
	}
	
	public void AddjustSatisfaction(int adj){
		curValue += adj;
		if(curValue < 0)
			curValue = 0;
		if(curValue >maxValue)
			curValue = maxValue;
		if(maxValue < 1)
			maxValue = 1;
		barLength = (Screen.width/4)* (curValue/(float)maxValue);
	}
}
