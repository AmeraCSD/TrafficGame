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
	
	public List<TrafficLight> lights;
	
	private int max_lights_to_open;
	
	
	public const float MIN_VEHICLE_SPEED = 23.0f;		//this should be in the global class 
	
	//These variables are for changing the traffic lights (for the steady state)
	private Queue timersQueue;
	private Queue streetObjectsQueue;
	private float timer;
	private float checkedTimer;
	
	public List<GameObject> currentlyOpenedLights;
	
	public GameObject youCantOpenLightLabelGo;
	public GameObject youCantOpenLightLabelVarGo;
	//private UILabel youCantOpenLightLabel;
	
	void Awake(){
		youCantOpenLightLabelGo.SetActive(false);
		youCantOpenLightLabelVarGo.SetActive(false);

	//	youCantOpenLightLabel = youCantOpenLightLabelGo.GetComponent<UILabel>();
	}
	
	
	// Use this for initialization
	void Start () {
		currentlyOpenedLights = new List<GameObject>();
		initVariablesAtStart();

		Streets = GameObject.FindGameObjectWithTag("master").GetComponent<GameMaster>().Streets; //To get the streets in the whole game here in one list
		lights = GameObject.FindGameObjectWithTag("master").GetComponent<GameMaster>().Lights;
		max_lights_to_open = GameObject.FindGameObjectWithTag("master").GetComponent<GameMaster>().currentLevel.MaxLightsToOpen;
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
		if(Input.GetMouseButtonDown(0)){
			
			Ray ray = (GameObject.FindGameObjectWithTag("MainCamera")).camera.ScreenPointToRay(Input.mousePosition);
    		RaycastHit hit ;
    		if (Physics.Raycast(ray, out hit)){
				if(hit.collider.gameObject.renderer.material.color == Color.red){
					if(currentlyOpenedLights.Count < max_lights_to_open){
						currentlyOpenedLights.Add(hit.collider.gameObject);
						timer = Time.time ;
						PutOnHoldOnMouseHit(hit);
					}
					else{
						Debug.Log("you cannot do thisssss");
						youCantOpenLightLabelGo.SetActive(true);
						youCantOpenLightLabelVarGo.SetActive(true);
						youCantOpenLightLabelVarGo.GetComponent<UILabel>().text = max_lights_to_open+"";
					}
					
				}
				else if(hit.collider.gameObject.renderer.material.color == Color.green){
					youCantOpenLightLabelGo.SetActive(false);
					youCantOpenLightLabelVarGo.SetActive(false);
					if(currentlyOpenedLights.Contains(hit.collider.gameObject)){
						currentlyOpenedLights.Remove(hit.collider.gameObject);
					}
					timer = Time.time ;
					PutOnHoldOnMouseHit(hit);
				}
   			}	
		}
	}
	
	//this method changes each level
	private void PutOnHoldOnMouseHit(RaycastHit hit){
		
		int index = ContainsLight(hit.collider.gameObject);
//		Debug.Log("the index is "+ index);
		if(index != -1){
			for(int i=0; i<lights[index].AttachedStreets.Count; i++){
				PutStateOnHold(lights[index].AttachedStreets[i]);
			}
		}
		/*
		if(hit.collider.gameObject.tag == "lightRight"){
			PutStateOnHold(Streets[0]);
			PutStateOnHold(Streets[1]);
		}
		
		if(hit.collider.gameObject.tag == "lightRight1"){
			PutStateOnHold(Streets[24]);
			PutStateOnHold(Streets[25]);
		}
		
		if(hit.collider.gameObject.tag == "lightLeft"){
			PutStateOnHold(Streets[12]);
			PutStateOnHold(Streets[13]);
		}
		
		if(hit.collider.gameObject.tag == "lightLeft1"){
			PutStateOnHold(Streets[30]);
			PutStateOnHold(Streets[31]);
		}
		
		if(hit.collider.gameObject.tag == "lightUp1"){
			PutStateOnHold(Streets[4]);
			PutStateOnHold(Streets[5]);
		}
		
		if(hit.collider.gameObject.tag == "lightUp"){
			PutStateOnHold(Streets[10]);
			PutStateOnHold(Streets[11]);
		}
		
		if(hit.collider.gameObject.tag == "lightDown1"){
			PutStateOnHold(Streets[32]);
			PutStateOnHold(Streets[33]);
		}
		
		if(hit.collider.gameObject.tag == "lightDown"){
			PutStateOnHold(Streets[36]);
			PutStateOnHold(Streets[37]);
		}
		*/
		
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
			
			timer += str.MinimumDistanceToOpenTrafficLight / MIN_VEHICLE_SPEED ;
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
