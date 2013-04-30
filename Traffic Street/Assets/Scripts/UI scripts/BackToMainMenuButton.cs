using UnityEngine;
using System.Collections;

public class BackToMainMenuButton : MonoBehaviour {

	void OnClick(){
		if(Input.touchCount <=1){
			Application.LoadLevel("Main Menu");
		}
	}
	
	
}
