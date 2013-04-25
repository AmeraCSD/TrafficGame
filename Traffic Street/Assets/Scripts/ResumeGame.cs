using UnityEngine;
using System.Collections;

public class ResumeGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnClick(){
		Time.timeScale = 1;
		
		GameObject [] gos = PauseButton.obs;  
		for(int i = 0; i < gos.Length; i++){
				gos[i].SetActive(true);
		}
		
		GameObject [] gos2 = PauseButton.obs2;  
		for(int i = 0; i < gos2.Length; i++){
				gos2[i].SetActive(true);
		}
		
		Destroy(GameObject.FindGameObjectWithTag("menu")) ;
		
	}
}
