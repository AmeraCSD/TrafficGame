using UnityEngine;
using System.Collections;

public class LightTimerManager : MonoBehaviour {
	
	public int timer;
	public GameObject analogousLight;
	public bool startTimer;
	
	
	// Use this for initialization
	void Start () {
		startTimer = false;
		timer = 15;
		InvokeRepeating("CountTimerDown", 1.0f, 1.0f);
	}
	
	private void CountTimerDown(){
		if(startTimer){
			if(analogousLight.renderer.material.color == Color.red){
				timer--;
			}
			else if(analogousLight.renderer.material.color != Color.red){
				timer = 15;
				startTimer = false;
			}
			if(timer < 0){
				timer = 15;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(timer == 15){
			gameObject.GetComponent<UILabel>().text = "";
		}
		else{
			gameObject.GetComponent<UILabel>().text = timer+"";
		}
		
	}
}
