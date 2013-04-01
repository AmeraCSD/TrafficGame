using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class VehicleController : MonoBehaviour {
	
	private int curScore;
 
	public Vehicle myVehicle;
	//for the street
	private Street _street;
	private Path _path;
	private TrafficLight _light;
	private float _stopPosition;
	private Vector3 _endPosition;
	private Queue _myQueue;
	private int _queueSize;
	//for the vehicle
	
	private Direction lastDirection;
	private Direction _direction;
	private Direction _nextDirection;
	private float _speed;
	private float _size;
	
	//private CharacterController _charController ;
	private BoxCollider boxColl;	
	
	private bool haveToStop;
	private bool insideOnTriggerEnter; 
	
	private bool passed;
	private bool dequeued;
	private bool enqueued;
	private bool gameOver;
	
	private bool satisfyAdjustedOnTime;
	
	private float Offset;
	private int carsStillInsideNumber;
	
	private GameMaster gameMasterScript;
		
	
	// Use this for initialization
	void Awake(){
		 
		initInstancesAtFirst();
		
		
	}
	
	public void initInstancesAtFirst(){
		gameMasterScript = GameObject.FindGameObjectWithTag("master").GetComponent<GameMaster>();
		Offset = 0;
		dequeued = false;
		enqueued = false;
		haveToStop = false;
		insideOnTriggerEnter = false;
		passed = false;
		satisfyAdjustedOnTime = false;
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
		_speed 			= myVehicle.Speed;
		_size 			= myVehicle.Size;
		
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
		haveToStop = false;
		insideOnTriggerEnter = false;
		passed = false;
	}
	
	// Update is called once per frame
	void Update () {

		SetupColliderSize();
		PerformEnqueue();
		SetStopOffset();
				
		CheckPosition_DeqIfPassed();
		
		Move();
		CheckAndDestroyAtEnd();
		if(!passed)
			StopMovingOnRed();
		
		
		
		if(!(_light.Stopped) && !haveToStop){
			_speed = myVehicle.Speed;
			
		}
		
	}
	
	
	private void CheckPosition_DeqIfPassed(){
		//Debug.Log(gameObject.name +" The queue Size ------------> " + _queueSize );

		if( _direction == Direction.Right && transform.position.x > _stopPosition  ||
			_direction == Direction.Left && transform.position.x < _stopPosition ||
			_direction == Direction.Down  && transform.position.z < _stopPosition  ||
			_direction == Direction.Up && transform.position.z > _stopPosition ){
			
			
			passed = true ;
			
			if(!dequeued && (!(_light.Stopped)) ){
				if(_myQueue.Count > 0){
					
					_myQueue.Dequeue();
					dequeued = true;
					
					_street.VehiclesNumber --;
					//Debug.Log(gameObject.name +" is dequed"  +"  and The queue Size ------------>>> " + _queueSize  );
					
				}
			}
			if(myVehicle.NextStreet != null){
				TransfereToNextStreet();
				boxColl.isTrigger = true;
			}
			
		}
		
	}
	
	
	private void PerformEnqueue(){
		if(!enqueued ){
			
			_myQueue.Enqueue(gameObject);
			
			enqueued = true;
			//Debug.Log(gameObject.name +" is enqueued"  +"  and The queue Size ------------> " + _queueSize  );
		}
	}
	
	private void SetStopOffset(){
		//Debug.Log("QUEUE SIZEEEEEEE of  " + gameObject.name +"      "+ _queueSize);
		Offset =  (GetMyOrderInQueue()) * (_size + 5);
	}
	
	private int GetMyOrderInQueue(){
		object [] array  = _myQueue.ToArray();
		return Array.IndexOf(array, gameObject);
	}
	
	private void SetupColliderSize(){
		boxColl = GetComponent<BoxCollider>();
		if(myVehicle.Type != VehicleType.Ambulance){
			
			if(getVehicleLargerAxis(gameObject) == "x"){
				boxColl.size = new Vector3(.5f  , 1 , transform.localScale.z/2.0f  );
			}
			else{
				boxColl.size = new Vector3(transform.localScale.x/2.0f   , 1 , .5f );
			}
		}
		
	}
	
	private void CheckAndDestroyAtEnd(){
		if(CheckMyEndPosition()){
			//Destroy(gameObject) ;
			gameObject.SetActive(false);
			if(myVehicle.Type != VehicleType.Ambulance){
				gameMasterScript.existedVehicles.Enqueue(gameObject);
			}
			gameMasterScript.score += 1;
		}
		
	}
	
	private bool CheckMyEndPosition(){
		//For Lefts
		if(_direction == Direction.Left && (_nextDirection == null || _nextDirection == Direction.Left)){
			if(transform.position.x < _endPosition.x)
				return true;
		}
		
		if(_direction == Direction.Left && _nextDirection == Direction.Down){
			if(transform.position.x < _endPosition.x && transform.position.z < _endPosition.z)
				return true;
		}
		
		if(_direction == Direction.Left && _nextDirection == Direction.Up){
			if(transform.position.x < _endPosition.x && transform.position.z > _endPosition.z)
				return true;
		}
		
		//For Rights
		if(_direction == Direction.Right && (_nextDirection == null || _nextDirection == Direction.Right)){
			if(transform.position.x > _endPosition.x)
				return true;
		}
		
		if(_direction == Direction.Right &&  _nextDirection == Direction.Down){
			if(transform.position.x > _endPosition.x && transform.position.z < _endPosition.z)
				return true;
		}
		
		if(_direction == Direction.Right && _nextDirection == Direction.Up){
			if(transform.position.x > _endPosition.x && transform.position.z > _endPosition.z)
				return true;
		}
		
		//For Ups
		if(_direction == Direction.Up && (_nextDirection == null || _nextDirection == Direction.Up)){
			if(transform.position.z > _endPosition.z)
				return true;
		}
		
		if(_direction == Direction.Up && _nextDirection == Direction.Right){
			if(transform.position.z > _endPosition.z && transform.position.x > _endPosition.x)
				return true;
		}
		
		if(_direction == Direction.Up && _nextDirection == Direction.Left){
			if(transform.position.z > _endPosition.z && transform.position.x < _endPosition.x)
				return true;
		}
		
		//For Downs
		if(_direction == Direction.Down && (_nextDirection == null || _nextDirection == Direction.Down)){
			if(transform.position.z < _endPosition.z)
				return true;
		}
		
		if(_direction == Direction.Down && _nextDirection == Direction.Right ){
			if(transform.position.z < _endPosition.z && transform.position.x > _endPosition.x)
				return true;
		}
		
		if(_direction == Direction.Down && _nextDirection == Direction.Left ){
			if(transform.position.z < _endPosition.z && transform.position.x < _endPosition.x)
				return true;
		}
		return false;
	}
	
	
	
	private void TransfereToNextStreet(){
		if(_direction == Direction.Left && transform.position.x < _street.EndPoint.x){
			ResetVehicleAttributes();
		}
		else if(_direction == Direction.Right && transform.position.x > _street.EndPoint.x){
			ResetVehicleAttributes();
		}
		else if(_direction == Direction.Down && transform.position.z < _street.EndPoint.z){
			ResetVehicleAttributes();
		}
		else if(_direction == Direction.Up && transform.position.z >_street.EndPoint.z){
			ResetVehicleAttributes();
		}
	}
	
	private void StopMovingOnRed(){
		if(_light.tLight != null){
			if(_light.Stopped){
				if( (_direction == Direction.Right && transform.position.x > _stopPosition - Offset ) ||
					(_direction == Direction.Left && transform.position.x < _stopPosition + Offset) ||
					(_direction == Direction.Down && transform.position.z < _stopPosition + Offset) ||
					(_direction == Direction.Up && transform.position.z > _stopPosition - Offset)  ){
					
					_speed = 0.0f;
					
					if(!satisfyAdjustedOnTime && myVehicle.Type == VehicleType.Ambulance){
						GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SatisfyBar>().AddjustSatisfaction(2);
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
		//Debug.Log(gameObject.name + " ------> " + _direction);
		if(_direction == Direction.Left){
		//	ReverseLastDirectionMove();
    		transform.localRotation = Quaternion.AngleAxis(180, Vector3.up);
			transform.Translate(transform.TransformDirection(Vector3.left) * _speed * Time.deltaTime, Space.Self);
		}
		else if(_direction == Direction.Right){
		//	ReverseLastDirectionMove();
			
    		transform.localRotation = Quaternion.AngleAxis(180, Vector3.up);
			transform.Translate(transform.TransformDirection(Vector3.right) * _speed * Time.deltaTime, Space.Self);
		}
		else if(_direction == Direction.Down){
    		transform.localRotation = Quaternion.AngleAxis(90, Vector3.up);
		//	ReverseLastDirectionMove();
			transform.Translate(transform.TransformDirection(Vector3.forward) * _speed * Time.deltaTime, Space.Self);
		}
		else if(_direction == Direction.Up){
    		transform.localRotation = Quaternion.AngleAxis(90, Vector3.up);
		//	ReverseLastDirectionMove();
			transform.Translate(transform.TransformDirection(Vector3.back) * _speed * Time.deltaTime, Space.Self);
			//haveToStop = false;
		}
	}
		
	private void ReverseLastDirectionMove(){
		
		if((lastDirection == Direction.Down || lastDirection == Direction.Up) && (_direction == Direction.Right || _direction == Direction.Left)){
			transform.Rotate(transform.TransformDirection(Vector3.up)  * (float)(Time.time/.9));
		}
		if((_direction == Direction.Down || _direction == Direction.Up) && (lastDirection == Direction.Right || lastDirection == Direction.Left)){
			transform.Rotate(transform.TransformDirection(Vector3.up)  * (float)(Time.time/.9));
		}	}
	
	void OnTriggerEnter(Collider other) {
		//if(other.transform.tag == "vehicle")
	//	{
		
			
	//	}
		if(other.GetComponent<VehicleController>().myVehicle.Type == VehicleType.Ambulance){
			haveToStop = true;
			_speed = 50.0f;
		}
		
		else if(myVehicle.Type != VehicleType.Ambulance){
			haveToStop = true;
			_speed = 0.0f;
			gameMasterScript.gameOver = true;
		}
		
		else{
			haveToStop = true;
			_speed = 30.0f;
			other.GetComponent<VehicleController>()._speed = other.GetComponent<VehicleController>().myVehicle.Speed;
		}
		Debug.Log ("speed " + _speed);
   	}	
	/*
	void OnCollisionExit(Collision other){
	}
	*/
	
	void OnTriggerExit(Collider other) {
		//if(other.transform.tag == "vehicle")
	//	{
		Debug.Log("on trigger exit");
			haveToStop = false;
			_speed = myVehicle.Speed;
	//	}
		Debug.Log ("speed " + _speed);
	//	gameMasterScript.gameOver = true;
   	}
	
	private string getVehicleLargerAxis(GameObject v){
		
		if(v.transform.localScale.x > v.transform.localScale.z)
			return "x";
		else
			return "z";
	}
}
