using UnityEngine;
using System.Collections;

public class BackToMainMenuButton : MonoBehaviour {

	void OnClick(){
		Application.LoadLevel("Main Menu");	
	}
	
	
}
