using UnityEngine;
using System.Collections;

public class CloseButton : MonoBehaviour {
	
	GameMaster gameMasterScript;
	
	void Start(){
		gameMasterScript = GameObject.FindGameObjectWithTag("master").GetComponent<GameMaster>();
		//gameObject.active = false;
	}
	/*
	void OnClick()
	{
		if(Input.touchCount <=1){
			gameObject.active = false;
			gameMasterScript.showBox = !gameMasterScript.showBox;
			gameMasterScript.eventWarningLabel.text = "";
			gameMasterScript.eventsSpriteGo.SetActive(false);
			Time.timeScale = 1;
		}
	}
	*/

}
