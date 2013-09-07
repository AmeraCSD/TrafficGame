using UnityEngine;
using System.Collections;

public class LightTimerManager : MonoBehaviour {
	
	public int timer;
	public GameObject analogousLight;
	public bool startTimer;
	
	// Use this for initialization
	void Start () {
		startTimer = false;
		timer = Globals.angerMinTime;
		InvokeRepeating("CountTimerDown", 1.0f, 1.0f);
	}
	
	private void CountTimerDown(){
		if(startTimer){
			if(analogousLight.renderer.material.color == Color.red){
				timer--;
			}
			else if(analogousLight.renderer.material.color != Color.red){
				timer = Globals.angerMinTime;
				startTimer = false;
			}
			if(timer < 0){
				timer = Globals.angerMinTime;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(timer == Globals.angerMinTime){
			gameObject.GetComponent<UILabel>().text = "";
		}
		else{
			gameObject.GetComponent<UILabel>().text = timer+"";
		}
		
	}
}
