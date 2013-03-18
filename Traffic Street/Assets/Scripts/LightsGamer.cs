using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/*
 This class is attached to the Player (Main Camera)
 it is resposible for changing the light colors from red to green and vice versa

*/
public class LightsGamer : MonoBehaviour {
	
	private List<Street>  Streets;
	private TrafficLight _down;
	private TrafficLight _up;
	private TrafficLight _left;
	private TrafficLight _right;
	
	void Awake(){
		//InitLightsObjects();
	}
	
	//This method is intializing the objects of the traffic lights
/*	private void InitLightsObjects(){
		_down = new TrafficLight( LightPositionType.Down, FindLightObject("lightDown"), true);
		_up = new TrafficLight( LightPositionType.Up, FindLightObject("lightUp"), true);
		_left = new TrafficLight( LightPositionType.Left, FindLightObject("lightLeft"), false);
		_right = new TrafficLight( LightPositionType.Right, FindLightObject("lightRight"), false);	
	}
	*/
	
	//This methode returns a game object with the tag of the light type
	private GameObject FindLightObject(string name){
		GameObject target = GameObject.FindGameObjectWithTag(name);
		return target;
	}
	
	// Use this for initialization
	void Start () {
		Streets = GameObject.FindGameObjectWithTag("master").GetComponent<StreetsGenerator>().getStreets();

		InitLightsColors();	


	}
	
	private void InitLightsColors(){
		for(int i=0; i<4; i++ ){
			Streets[i].LightPosition.tLight.renderer.material.color = Color.red;
		}
		
		/*_up.tLight.renderer.material.color = Color.red;
		_down.tLight.renderer.material.color = Color.red;
		_left.tLight.renderer.material.color = Color.green;
		_right.tLight.renderer.material.color = Color.green;
		*/
	}
	

	
	// Update is called once per frame
	void Update () {
		//OnArrowsPressed();
		OnMousePressed();
		
	}
	
	
	
	private void OnMousePressed(){
		if(Input.GetMouseButtonDown(1)){
			Ray ray = (GameObject.FindGameObjectWithTag("MainCamera")).camera.ScreenPointToRay(Input.mousePosition);
    		RaycastHit hit;
    		if (Physics.Raycast(ray, out hit)){
	      		// the object identified by hit.transform was clicked
	      		// do whatever you want
				//Streets[i].LightPosition.ChangeState();
				ChangeStatesOnMouseHit(hit);
   			 }
			
		}
	}
	
	private void ChangeStatesOnMouseHit(RaycastHit hit){
		if(hit.collider.gameObject.tag == "lightDown"){
			Streets[0].LightPosition.ChangeState();
		}
		if(hit.collider.gameObject.tag == "lightUp"){
			Streets[1].LightPosition.ChangeState();
		}
		if(hit.collider.gameObject.tag == "lightLeft"){
			Streets[2].LightPosition.ChangeState();
			//Debug.Log("Tikkkkaaaaa");
		}
		if(hit.collider.gameObject.tag == "lightRight"){
			Streets[3].LightPosition.ChangeState();
		}
	}
	
/*	private void OnArrowsPressed(){
		if(Input.GetKeyDown(KeyCode.Tab)){
			
			_up.ChangeState();
			_down.ChangeState();
			_left.ChangeState();
			_right.ChangeState();
		}
		
	}
	*/
}
