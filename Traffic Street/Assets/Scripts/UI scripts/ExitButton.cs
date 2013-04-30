using UnityEngine;
using System.Collections;

public class ExitButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnClick(){
		if(Input.touchCount <=1){
			Debug.Log("should quit the application");
			Application.Quit();
		}
	}
	
}
