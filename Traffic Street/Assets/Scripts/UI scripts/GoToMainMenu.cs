using UnityEngine;
using System.Collections;

public class GoToMainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnClick(){
		
		if(Input.touchCount <=1){
			Time.timeScale = 1;
			Application.LoadLevel("Main Menu");
		}
	
	}
	
}
