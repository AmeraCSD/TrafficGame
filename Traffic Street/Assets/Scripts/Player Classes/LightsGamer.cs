using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/*
 This class is attached to the Player (Main Camera)
 it is resposible for changing the light colors from red to green and vice versa

*/
public class LightsGamer : MonoBehaviour {
	
	private List<Street>  Streets;
	
	public List<TrafficLight> lights;
	public List<LightsGroup> lightsGroups;
	
	private int max_lights_to_open;
	
	public bool rotateArrow;
	public GameObject rotOb;
	
	public const float MIN_VEHICLE_SPEED = 23.0f;		//this should be in the global class 
	
	//These variables are for changing the traffic lights (for the steady state)
	private Queue timersQueue;
	private Queue streetObjectsQueue;
	private float timer;
	private float checkedTimer;
	
	public List<GameObject> currentlyOpenedLights;
	

	void Awake(){
		rotateArrow = true;

	}
	
	
	// Use this for initialization
	void Start () {
		currentlyOpenedLights = new List<GameObject>();
		initVariablesAtStart();

		Streets = GameObject.FindGameObjectWithTag("master").GetComponent<GameMaster>().Streets; //To get the streets in the whole game here in one list
		lights = GameObject.FindGameObjectWithTag("master").GetComponent<GameMaster>().Lights;
		lightsGroups = GameObject.FindGameObjectWithTag("master").GetComponent<GameMaster>().LightsGroups;
		InitLightsColors();	
		
	}
	
	//this method initializes the variables (should be called in the Start() method)
	private void initVariablesAtStart(){
		timersQueue = new Queue();
		streetObjectsQueue = new Queue();
		timer=0;
		checkedTimer = 0;
	}
	
	//This method for setting the lights at first all with red light "all stopped" (should be called in the Start() method)_
	private void InitLightsColors(){
		for(int i=0; i<lightsGroups.Count ; i++ ){
			Debug.Log("i = " + i);
			for(int j =0; j<lightsGroups[i].GroupOfLights.Count;j++){
				if(j %2 == 0){	//is even
					lightsGroups[i].GroupOfLights[j].tLight.renderer.material.color = Color.green;
					lightsGroups[i].GroupOfLights[j].Stopped = false;
				}
				else{
					lightsGroups[i].GroupOfLights[j].tLight.renderer.material.color = Color.red;
					lightsGroups[i].GroupOfLights[j].Stopped = true;
				}
			}
		
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if(rotateArrow && rotOb!=null){
			rotOb.transform.Rotate(Vector3.up *600* Time.deltaTime, Space.World);
		}
		OnMousePressed();
		
		ChangeStateOnCheckedTimer();
		
	}
	
	//These two coming methods put the state of the light traffic on hold if the mouse is pressed at any time 
	//(called each frame)
	
	private void OnMousePressed(){
		if(Input.GetMouseButtonDown(0)){
			
			Ray ray = (GameObject.FindGameObjectWithTag("MainCamera")).camera.ScreenPointToRay(Input.mousePosition);
    		RaycastHit hit ;
    		if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.layer == 8){
//				Debug.Log(hit.collider.gameObject.tag);
				int index = IndexOfTag(hit.collider.gameObject.tag);
				if(index != -1 && !CheckIfAnyYellowInGroup(index)){
					timer = Time.time ;
				//	if(lightsGroups[index].GroupOfLights.Count == 2){
						rotOb = hit.collider.gameObject;
						rotateArrow = true;
					
				//	}
					PutOnHoldOnMouseHit(index);
				}
				
   			}	
		}
	}
	
	private int IndexOfTag(string tag){
		
		for(int i = 0; i<lightsGroups.Count; i++){
			if(tag.Contains((i+1).ToString())){
				return i;	
			}
		}
		
		return -1;
		
	}
	
	private bool CheckIfAnyYellowInGroup(int index){
		for(int i = 0; i<lightsGroups.Count; i++){
			for(int j =0 ; j < lightsGroups[i].GroupOfLights.Count ; j++){
				if(lightsGroups[i].GroupOfLights[j].tLight.renderer.material.color == Color.yellow)
					return true;
			}
		}
		return false;
	}
	private void PutOnHoldOnMouseHit(int index){
		

		LightsGroup lightGroup = lightsGroups[index];
		
		for(int i=0; i< lightGroup.GroupOfLights.Count; i++){
			for(int j = 0; j< lightGroup.GroupOfLights[i].AttachedStreets.Count; j++){
				PutStateOnHold(lightGroup.GroupOfLights[i].AttachedStreets[j]);
			}
			
		}
	
		
	}
	
	private int ContainsLight(GameObject go){
		for(int i=0; i<lights.Count; i++){
			if(lights[i].tLight.Equals(go)){
				return i;
			}
		}
		return -1;
	}
	
	//this method called when we hold on a state .. it enqueues the holded light street and the current time + the time it has to change the state in 
	//(the calculations of the timer are based on street width and the minimum speed of the slowest vehicle)
	public void PutStateOnHold(Street str){
		if(!str.StreetLight.OnHold){
			str.StreetLight.OnHold = true;
			//Debug.Log("Inside Put State On hold");
			
			str.StreetLight.tLight.renderer.material.color = Color.yellow;
			
		//	timer += str.MinimumDistanceToOpenTrafficLight / MIN_VEHICLE_SPEED ;
			timer += .2f;
			//timer +=1;			
			//Debug.Log("The current time is --->  " + Time.time + "and the timer is ---> " + timer);
			if(timersQueue.Count == 0){
				checkedTimer = timer;
			}
			timersQueue.Enqueue(timer);
			streetObjectsQueue.Enqueue(str);
		}
	}
	/////////////////////////////////////////////////////////////////////////////////////////////
	
	//This method checks ,each frame, if the hold time is finished for any existed changed traffic light then changes the state (if any)
	//(called each frame)
	private void ChangeStateOnCheckedTimer(){
		if(Time.time >= checkedTimer){
			if(timersQueue.Count != 0 && streetObjectsQueue.Count != 0){
				timersQueue.Dequeue();
				Street s = streetObjectsQueue.Dequeue() as Street;
				ChangeState(s);
				s.StreetLight.OnHold = false;
				
				rotateArrow = false;
				if(timersQueue.Count != 0)
					checkedTimer = (float) timersQueue.Peek();	//setting the next checkedTimer from the queue
			}
		}
	}
	
	//this method changes the state of the light from red to green and vice versa	
	public void ChangeState(Street str){
		if(str.StreetLight.Stopped){
			
			str.StreetLight.Stopped = false;
			//if(str.StreetLight.tLight.renderer.material.color != Color.green)
				str.StreetLight.tLight.renderer.material.color = Color.green;
		}
		else if(!(str.StreetLight.Stopped)){
			
			str.StreetLight.Stopped = true;
			//if(str.StreetLight.tLight.renderer.material.color != Color.red)
				str.StreetLight.tLight.renderer.material.color = Color.red;
		}
		else{
			Debug.LogWarning("you can't change the light while it is yellow");
		}
		
	}
	
}
