using UnityEngine;
using System.Collections;

public class NextLevelButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnClick(){
		
		if(Application.loadedLevelName == "Map 1"){
			Application.LoadLevel("Map 2");
		}
		
		
		
	}
}
