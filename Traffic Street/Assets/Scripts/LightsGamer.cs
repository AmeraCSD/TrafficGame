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
	
	
	public const float MIN_VEHICLE_SPEED = 10.0f;		//this should be in the global class 
	
	//These variables are for changing the traffic lights (for the steady state)
	private Queue timersQueue;
	private Queue streetObjectsQueue;
	private float timer;
	private float checkedTimer;
	
	void Awake(){
	}
	
	
	// Use this for initialization
	void Start () {
		 
		initVariablesAtStart();

		Streets = GameObject.FindGameObjectWithTag("master").GetComponent<StreetsGenerator>().getStreets(); //To get the streets in the whole game here in one list

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
		for(int i=0; i<Streets.Count; i++ ){
			if(Streets[i].StreetLight.tLight != null){
				Streets[i].StreetLight.tLight.renderer.material.color = Color.red;
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		OnMousePressed();
		
		ChangeStateOnCheckedTimer();
		
	}
	
	//These two coming methods put the state of the light traffic on hold if the mouse is pressed at any time 
	//(called each frame)
	
	private void OnMousePressed(){
		if(Input.GetMouseButtonDown(1)){
			Ray ray = (GameObject.FindGameObjectWithTag("MainCamera")).camera.ScreenPointToRay(Input.mousePosition);
    		RaycastHit hit ;
    		if (Physics.Raycast(ray, out hit)){
				timer = Time.time ;
				PutOnHoldOnMouseHit(hit);
   			 }	
		}
	}
	
	//this method changes each level
	private void PutOnHoldOnMouseHit(RaycastHit hit){
		
		if(hit.collider.gameObject.tag == "lightDown"){
			PutStateOnHold(Streets[0]);
			PutStateOnHold(Streets[2]);
		}
		
		if(hit.collider.gameObject.tag == "lightLeft"){
			PutStateOnHold(Streets[4]);
			PutStateOnHold(Streets[5]);
		}
		
	}
	
	//this method called when we hold on a state .. it enqueues the holded light street and the current time + the time it has to change the state in 
	//(the calculations of the timer are based on street width and the minimum speed of the slowest vehicle)
	public void PutStateOnHold(Street str){
		if(!str.StreetLight.OnHold){
			str.StreetLight.OnHold = true;
			Debug.Log("Inside Put State On hold");
			str.StreetLight.tLight.renderer.material.color = Color.yellow;
			
			timer += str.MinimumDistanceToOpenTrafficLight / MIN_VEHICLE_SPEED ;
						
			//Debug.Log("The current time is --->  " + Time.time + "and the timer is ---> " + timer);
			if(timersQueue.Count == 0){
				checkedTimer = timer;
			}
			timersQueue.Enqueue(timer);
			streetObjectsQueue.Enqueue(str);
		}
	}
	
	//This method checks ,each frame, if the hold time is finished for any existed changed traffic light then changes the state (if any)
	//(called each frame)
	private void ChangeStateOnCheckedTimer(){
		if(Time.time >= checkedTimer){
			if(timersQueue.Count != 0 && streetObjectsQueue.Count != 0){
				timersQueue.Dequeue();
				Street s = streetObjectsQueue.Dequeue() as Street;
				ChangeState(s);
				s.StreetLight.OnHold = false;
				if(timersQueue.Count != 0)
					checkedTimer = (float) timersQueue.Peek();	//setting the next checkedTimer from the queue
			}
		}
	}
	
	//this method changes the state of the light from red to green and vice versa	
	public void ChangeState(Street str){
		if(str.StreetLight.Stopped){
			str.StreetLight.Stopped = false;
			str.StreetLight.tLight.renderer.material.color = Color.green;
		}
		else if(!(str.StreetLight.Stopped)){
			str.StreetLight.Stopped = true;
			str.StreetLight.tLight.renderer.material.color = Color.red;
		}
		else{
			Debug.LogWarning("you can't change the light while it is yellow");
		}
		
	}
	
}
