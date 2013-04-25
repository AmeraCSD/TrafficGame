using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour {
	public GameObject pauseMenu;
	public static GameObject [] obs;
	public static GameObject [] obs2;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	
	}
	
	void OnClick(){
		
		if(GameObject.FindGameObjectWithTag("menu") == null){
			Instantiate(pauseMenu, new Vector3(0,0,0) ,Quaternion.identity);
		}
		Time.timeScale = 0;
		
		obs = GameObject.FindGameObjectsWithTag("Finish");
		obs2 = GameObject.FindGameObjectsWithTag("vehicle");
		
		for(int i = 0; i < obs.Length; i++)
			obs[i].SetActive(false);
		
		for(int i = 0; i < obs2.Length; i++)
			obs2[i].SetActive(false);
		
		
	}
}
