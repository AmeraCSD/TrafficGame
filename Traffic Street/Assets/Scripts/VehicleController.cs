using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class VehicleController : MonoBehaviour {
	
	private int curScore;
 
	public Vehicle myVehicle;
	//for the street
	private Street _street;
	private GamePath _path;
	private TrafficLight _light;
	private float _stopPosition;
	private Vector3 _endPosition;
	private Queue _myQueue;
	private int _queueSize;
	//for the vehicle
	
	private StreetDirection lastDirection;
	private StreetDirection _direction;
	private StreetDirection _nextDirection;
	public float speed;
	private float _size;
	public VehicleType vehType;
	
	//private CharacterController _charController ;
	private BoxCollider boxColl;	
	
	
	
	private bool insideOnTriggerEnter; 
	
	private bool passed;
	private bool dequeued;
	private bool enqueued;
	private bool gameOver;
	
	public bool thereIsAbus;
	public bool haveToReduceMySpeed;
	
	private bool satisfyAdjustedOnTime;
	
	private float Offset;
	private int carsStillInsideNumber;
	
	private GameMaster gameMasterScript;
	private float stoppingTimerforAnger;
	private bool stoppingTimerforAngerSet;
	
	public float busStopTimer;
	
	//public bool enableRayCast;
		
	
	// Use this for initialization
	void Awake(){
		 
		initInstancesAtFirst();
		
		
	}
	
	public void initInstancesAtFirst(){
		gameMasterScript = GameObject.FindGameObjectWithTag("master").GetComponent<GameMaster>();
		Offset = 0;
		dequeued = false;
		enqueued = false;
		insideOnTriggerEnter = false;
		passed = false;
		satisfyAdjustedOnTime = false;
		stoppingTimerforAnger = 0;
		stoppingTimerforAngerSet = false;
		thereIsAbus = false;
		haveToReduceMySpeed = false;
		busStopTimer = 0;
		//enableRayCast = false;
	}
	
	void Start () {
		InitStreetAndVehicleAttributes();
	//	_charController = GetComponent<CharacterController>();
		//Physics.IgnoreCollision(collider, boxColl);
		
	}
	
	public void InitStreetAndVehicleAttributes(){
		_path			= myVehicle.MyPath;
		_endPosition 	= _path.EndPosition;
		
		_street 		= myVehicle.CurrentStreet;
		_direction 		= myVehicle.CurrentDirection;
		speed 			= myVehicle.Speed;
		_size 			= myVehicle.Size;
		vehType		= myVehicle.Type;
		
		_light 			= _street.StreetLight;
		_stopPosition 	= _street.StopPosition;
		_myQueue 		= _street.StrQueue;
		_queueSize 		= _myQueue.Count;
		
		lastDirection = _direction;
		_nextDirection = myVehicle.NextStreet.StreetLight.Type;
		
	}
	
	private void ResetVehicleAttributes(){
		lastDirection = _direction;
		myVehicle.CurrentStreet = myVehicle.NextStreet;
		myVehicle.CurrentStreetNumber ++;
		if(myVehicle.CurrentStreetNumber+1 != _path.PathStreets.Count )
			myVehicle.NextStreet =_path.PathStreets[myVehicle.CurrentStreetNumber];
		else
			myVehicle.NextStreet = null;
		
		_street = myVehicle.CurrentStreet; 
		
		myVehicle.CurrentDirection = _street.StreetLight.Type;
		_direction 		= myVehicle.CurrentDirection;
		//Debug.Log(gameObject.name + " direction" + _direction);
		
		_street.VehiclesNumber++;
		_light 			= _street.StreetLight;
		_stopPosition 	= _street.StopPosition;
		_myQueue 		= _street.StrQueue;
		_queueSize 		= _street.StrQueue.Count;
		
		Offset = 0;
		dequeued = false;
		enqueued = false;
		insideOnTriggerEnter = false;
		passed = false;
		haveToReduceMySpeed = false;
		//thereIsAbus = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
		SetupColliderSize();
		PerformEnqueue();
		SetStopOffset();
				
		CheckPosition_DeqIfPassed();
		
		Move();
		CheckAndDeactivateAtEnd();
		if(!passed){
			SetStopOffset();
			StopMovingOnRed();
		}
		
		if(speed == 0 && GetMyOrderInQueue()== 0){
				//Debug.Log("hereeeeeeeeeeeeeeeeeeeeeeeee");
				if(! stoppingTimerforAngerSet){
					//Debug.Log("stoppingTimerforAnger .. " + stoppingTimerforAnger);
					stoppingTimerforAnger = gameMasterScript.gameTime - 6 ;
					stoppingTimerforAngerSet = true;
				}
		}
		if(GetMyOrderInQueue()== 0){
			if(vehType != VehicleType.Ambulance && gameMasterScript.gameTime <= stoppingTimerforAnger){
				stoppingTimerforAngerSet = false;
				GameObject.FindGameObjectWithTag("satisfyBar").GetComponent<SatisfyBar>().AddjustSatisfaction(1);
				gameMasterScript.satisfyBar += 1;
			//	satisfyAdjustedOnTime = true;
				stoppingTimerforAnger =0;
				
			}
		}
		//Debug.Log(stoppingTimerforAngerSet);
		
		if(busStopTimer >= gameMasterScript.gameTime){
			speed = myVehicle.Speed;
			//Debug.Log("haaaaaaaaaaaaaa");
		}
		
		if(!(_light.Stopped) && !haveToReduceMySpeed ){ /////////////////////////////////////////////////
			speed = myVehicle.Speed;
			
		}
		
	}
	
	
	private void CheckPosition_DeqIfPassed(){
		//Debug.Log(gameObject.name +" The queue Size ------------> " + _queueSize );

		if( _direction == StreetDirection.Right && transform.position.x >= _stopPosition    ||
			_direction == StreetDirection.Left && transform.position.x <= _stopPosition  ||
			_direction == StreetDirection.Down  && transform.position.z <= _stopPosition  ||
			_direction == StreetDirection.Up && transform.position.z >= _stopPosition ){
			
			
			passed = true ;
			
			if(!dequeued && (!(_light.Stopped)  || StillInStopRange()) ){
								
				if(_myQueue.Count > 0){
					
					_myQueue.Dequeue();
					dequeued = true;
					
					_street.VehiclesNumber --;
					if(!satisfyAdjustedOnTime && vehType == VehicleType.Ambulance){
						GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SatisfyBar>().AddjustSatisfaction(-1);
						gameMasterScript.satisfyBar --;
						satisfyAdjustedOnTime = true;
					}
					
				//	Debug.Log(gameObject.name +" is dequed" );
					
				}
				
			}
			if(myVehicle.NextStreet != null){
				TransfereToNextStreet();
				boxColl.isTrigger = true;
			}
			
		}
		
	}
	
	private bool StillInStopRange(){
		if(_light.Stopped){
			if( _direction == StreetDirection.Right && transform.position.x >= _stopPosition  +3  ||
			_direction == StreetDirection.Left && transform.position.x <= _stopPosition -5 ||
			_direction == StreetDirection.Down  && transform.position.z <= _stopPosition -5 ||
			_direction == StreetDirection.Up && transform.position.z >= _stopPosition +3 ){
			
				return true;
				
			}
			
			
		}
		return false;
	}
	
	
	private void PerformEnqueue(){
		if(!enqueued ){
			
			_myQueue.Enqueue(gameObject);
			
			enqueued = true;
			//Debug.Log(gameObject.name +" is enqueued"  +"  and The queue Size ------------> " + _queueSize  );
		}
	}
	
	private void SetStopOffset(){
		//Debug.Log(gameObject.name +"      "+ GetMyOrderInQueue());
		Offset =  (GetMyOrderInQueue()) * (_size + 10);
	}
	
	private int GetMyOrderInQueue(){
		object [] array  = _myQueue.ToArray();
		return Array.IndexOf(array, gameObject);
	}
	
	private void SetupColliderSize(){
		boxColl = GetComponent<BoxCollider>();
		if(vehType == VehicleType.Normal){
			
			if(getVehicleLargerAxis(gameObject) == "x"){
				//boxColl.size = new Vector3(.5f  , 1 , transform.localScale.z/2.0f  );
			}
			else{
				//boxColl.size = new Vector3(transform.localScale.x/2.0f   , 1 , .5f );
			}
		}
		
	}
	
	private void CheckAndDeactivateAtEnd(){
		if(MathsCalculatios.CheckMyEndPosition(transform, _direction, _nextDirection, _endPosition)){
			//Destroy(gameObject) ;
			gameObject.SetActive(false);
			if(vehType == VehicleType.Normal){
				gameMasterScript.existedVehicles.Enqueue(gameObject);
			}
			gameMasterScript.score += 1;
			
		}
		
	}
	
	
	
	
	
	private void TransfereToNextStreet(){
		if(_direction == StreetDirection.Left && transform.position.x < _street.EndPoint.x){
			ResetVehicleAttributes();
		}
		else if(_direction == StreetDirection.Right && transform.position.x > _street.EndPoint.x){
			ResetVehicleAttributes();
		}
		else if(_direction == StreetDirection.Down && transform.position.z < _street.EndPoint.z){
			ResetVehicleAttributes();
		}
		else if(_direction == StreetDirection.Up && transform.position.z >_street.EndPoint.z){
			ResetVehicleAttributes();
		}
	}
	
	public void SetStopTimeForBus(){
		busStopTimer = gameMasterScript.gameTime - 3;
	}
	
	private void StopMovingOnRed(){
		SetStopOffset();
	/*	if(vehType == VehicleType.Bus){
			
		}
		*/
		
		if(_light.tLight != null){
			if(_light.Stopped){
				if( (_direction == StreetDirection.Right && transform.position.x > _stopPosition - Offset ) ||
					(_direction == StreetDirection.Left && transform.position.x < _stopPosition + Offset) ||
					(_direction == StreetDirection.Down && transform.position.z < _stopPosition + Offset) ||
					(_direction == StreetDirection.Up && transform.position.z > _stopPosition - Offset)  ){
					
					speed = 0.0f;
					
					
					if(!satisfyAdjustedOnTime && vehType == VehicleType.Ambulance){
						GameObject.FindGameObjectWithTag("satisfyBar").GetComponent<SatisfyBar>().AddjustSatisfaction(2);
						gameMasterScript.satisfyBar += 2;
						Debug.Log("Ambulance stopped ... not good");
						satisfyAdjustedOnTime = true;
					}
				}
		
			}
				
		}
		
	}
	
	
	private void Move(){
		SetStopOffset();
		
		
		if(_direction == StreetDirection.Left){
    		transform.localRotation = Quaternion.AngleAxis(180, Vector3.up);
			transform.Translate(transform.TransformDirection(Vector3.left) * speed * Time.deltaTime, Space.Self);
			
			Ray ray = new Ray(transform.position, Vector3.left);
			ReduceMeIfHit(ray);
		}
		else if(_direction == StreetDirection.Right){
    		transform.localRotation = Quaternion.AngleAxis(180, Vector3.up);
			transform.Translate(transform.TransformDirection(Vector3.right) * speed * Time.deltaTime, Space.Self);
			
			Ray ray = new Ray(transform.position, Vector3.right);
			ReduceMeIfHit(ray);
		}
		else if(_direction == StreetDirection.Down){
    		transform.localRotation = Quaternion.AngleAxis(90, Vector3.up);
			transform.Translate(transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime, Space.Self);
			
			Ray ray = new Ray(transform.position, Vector3.back);
			ReduceMeIfHit(ray);
		}
		else if(_direction == StreetDirection.Up){
    		transform.localRotation = Quaternion.AngleAxis(90, Vector3.up);
			transform.Translate(transform.TransformDirection(Vector3.back) * speed * Time.deltaTime, Space.Self);
			
			Ray ray = new Ray(transform.position, Vector3.forward);
			ReduceMeIfHit(ray);
		}
	}
	
	private void ReduceMeIfHit(Ray ray){
		RaycastHit hit ;
		if(Physics.Raycast(ray, out hit, 10)){
			//Debug.Log("I hit something");
			Debug.DrawLine (ray.origin, hit.point);
			VehicleController hitVehicleController = hit.collider.gameObject.GetComponent<VehicleController>();
		
			
			if(hitVehicleController.speed < speed || haveToReduceMySpeed ){
			//	Debug.Log(gameObject.name +  " ... I have to reduce my speed");
				speed = hitVehicleController.speed;
				haveToReduceMySpeed = true;
			}
			else
				haveToReduceMySpeed = false;
			
			
		}
		else{ 
			if(vehType != VehicleType.Bus)
				haveToReduceMySpeed = false;
		}
		
		
		
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log("on trigger enterrrrr");
		other.gameObject.GetComponent<VehicleController>().haveToReduceMySpeed = true;
		other.gameObject.GetComponent<VehicleController>().speed = 0.0f;
		gameMasterScript.gameOver = true;
		
		
   	}	
	
	
	
	void OnTriggerExit(Collider other) {
		if(other.transform.tag == "vehicle"){
			Debug.Log("on trigger exit");
			other.gameObject.GetComponent<VehicleController>().haveToReduceMySpeed = false;
			other.gameObject.GetComponent<VehicleController>().speed = myVehicle.Speed;			//speed = myVehicle.Speed;
		}
		//Debug.Log ("speed " + speed);
	
   	}
	
	private string getVehicleLargerAxis(GameObject v){
		
		if(v.transform.localScale.x > v.transform.localScale.z)
			return "x";
		else
			return "z";
	}
}
