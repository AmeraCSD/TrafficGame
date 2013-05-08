using UnityEngine;
using System.Collections;

public class RepeatGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnClick(){
		Time.timeScale =1;
		Application.LoadLevel(Application.loadedLevelName);
	
	}
}
