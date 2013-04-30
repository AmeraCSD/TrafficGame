using UnityEngine;
using System.Collections;

public class EnableVibrationCheckBox : MonoBehaviour {
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Globals.vibrationEnabled == true){
			gameObject.GetComponent<UICheckbox>().isChecked = true;
		}
		else{
			gameObject.GetComponent<UICheckbox>().isChecked = false;
		}
	
	}
	
	void OnActivate(){
		if(gameObject.GetComponent<UICheckbox>().isChecked){
			Debug.Log("vibration on");
			Globals.vibrationEnabled = true;
		}
		else{
			Debug.Log("vibration off");	
			Globals.vibrationEnabled = false;
		}
	}
}
