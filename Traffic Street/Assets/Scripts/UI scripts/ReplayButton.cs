using UnityEngine;
using System.Collections;

public class ReplayButton : MonoBehaviour {
	
	GameMaster gameMasterScript;
	
	void Start(){
		gameMasterScript = GameObject.FindGameObjectWithTag("master").GetComponent<GameMaster>();
		//gameObject.active = false;
	}
	
	void OnClick()
	{
		Application.LoadLevel("Level 2");
		//gameObject.active = false;
		//gameMasterScript.eventWarningLabel.text = "";
		
	}

}
