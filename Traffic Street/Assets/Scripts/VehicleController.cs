using UnityEngine;
using System.Collections;


public class VehicleController : MonoBehaviour {
	
	private int curScore;
 
	public Vehicle myVehicle;
	//for the street
	private Street _street;
	private TrafficLight _light;
	private float _stopPosition;
	private float _streetEndPosition;
	private Queue _myQueue;
	private int _queueSize;
	//for the vehicle
	private Direction _direction;
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
	
	private float Offset;
		
	
	// Use this for initialization
	void Awake(){
		 
		
		Offset = 0;
		dequeued = false;
		enqueued = false;
		haveToStop = false;
		insideOnTriggerEnter = false;
		
	}
	
	void Start () {
		InitStreetAndVehicleAttributes();
	//	_charController = GetComponent<CharacterController>();
		//Physics.IgnoreCollision(collider, boxColl);
		passed = false;
	}
	
	private void InitStreetAndVehicleAttributes(){
		_street = myVehicle.CurrentStreet;
		_light = _street.StreetLight;
		_stopPosition = _street.StopPosition;
		_streetEndPosition = _street.StreetEndPosition;
		_myQueue = _street.StrQueue;
		_queueSize = _street.StrQueue.Count;
		
		_direction = myVehicle.CurrentDirection;
		_speed = myVehicle.Speed;
		_size = myVehicle.Size;
	}
	
	
	// Update is called once per frame
	void Update () {
		SetupColliderSize();
		SetStopOffset();
		PerformEnqueue();
		CheckPosition_DeqIfPassed();
		
		Move();
		
		CheckAndDestroyAtEnd();
		if(!passed)
			StopMovingOnRed();
		
		
		
		if((_light.tLight.renderer.material.color == Color.green) && !haveToStop){
			_speed = myVehicle.Speed;
			
		}
	
	}
	
	
	private void CheckPosition_DeqIfPassed(){
		
		if( _direction == Direction.Right && transform.position.x > _stopPosition ||
			_direction == Direction.Left && transform.position.x < _stopPosition ||
			_direction == Direction.Down  && transform.position.z < _stopPosition ||
			_direction == Direction.Up && transform.position.z > _stopPosition ){		
				
			passed = true ;
			if(!dequeued && (_light.tLight.renderer.material.color == Color.green)){
				if(_myQueue.Count > 0){
					_myQueue.Dequeue();
					dequeued = true;
				//	boxColl.isTrigger = true;
					Debug.Log(gameObject.name +" is dequeued" );
				}
			}
		}
	}
	
	private void PerformEnqueue(){
		if(!enqueued ){
				_myQueue.Enqueue(gameObject);
				enqueued = true;
		}
	}
	
	private void SetStopOffset(){
	//	Debug.Log("QUEUE SIZEEEEEEE of  " + gameObject.name +"      "+ _queueSize);
		Offset =  (_queueSize) * (_size + 3);
	}
	
	private void SetupColliderSize(){
		boxColl = GetComponent<BoxCollider>();
		if(getVehicleLargerAxis(gameObject) == "x"){
			boxColl.size = new Vector3(1  , 1 , transform.localScale.z  );
		}
		else{
			boxColl.size = new Vector3(transform.localScale.x  , 1 , 1 );
		}
		
	}
	
	private void CheckAndDestroyAtEnd(){
		if(_direction == Direction.Left && transform.position.x < _streetEndPosition){
			Destroy(gameObject) ;
			//curScore += 500;
		}
		else if(_direction == Direction.Right && transform.position.x > _streetEndPosition){
			Destroy(gameObject) ;
			//curScore += 500;
		}
		else if(_direction == Direction.Down && transform.position.z < _streetEndPosition){
			Destroy(gameObject) ;
			//curScore += 500;
		}
		else if(_direction == Direction.Up && transform.position.z > _streetEndPosition){
			Destroy(gameObject) ;
			//curScore += 500;
		}
	}

	
	private void StopMovingOnRed2(){
		_myQueue.Enqueue(gameObject);
		enqueued = true;
		_speed = 0.0f;
	//	Debug.Log(gameObject.name +" is enqueued" );
	}
	
	private void StopMovingOnRed(){
		if(_direction == Direction.Right && !(_light.tLight.renderer.material.color == Color.green) && transform.position.x > _stopPosition - Offset ){
			_speed = 0.0f;
		}
		else if(_direction == Direction.Left && !(_light.tLight.renderer.material.color == Color.green) && transform.position.x < _stopPosition + Offset ){
			_speed = 0.0f;
		}
		else if(_direction == Direction.Down && !(_light.tLight.renderer.material.color == Color.green) && transform.position.z < _stopPosition + Offset ){
			_speed = 0.0f;
		}
		else if(_direction == Direction.Up && !(_light.tLight.renderer.material.color == Color.green) && transform.position.z > _stopPosition - Offset ){
			_speed = 0.0f;
		}
	}

	private void Move(){
		if(_direction == Direction.Left){
			//_charController.Move(transform.TransformDirection(Vector3.left) * _speed * Time.deltaTime);
			transform.Translate(transform.TransformDirection(Vector3.left) * _speed * Time.deltaTime, Space.Self);
			//haveToStop = false;
		}
		else if(_direction == Direction.Right){
			//_charController.Move(transform.TransformDirection(Vector3.right) * _speed * Time.deltaTime);
			transform.Translate(transform.TransformDirection(Vector3.right) * _speed * Time.deltaTime, Space.Self);
			//haveToStop = false;
		}
		else if(_direction == Direction.Down){
    		transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
			//_charController.Move(transform.TransformDirection(Vector3.right) * _speed * Time.deltaTime);
			transform.Translate(transform.TransformDirection(Vector3.forward) * _speed * Time.deltaTime, Space.Self);
			//haveToStop = false;
		}
		else if(_direction == Direction.Up){
    		transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
			//_charController.Move(transform.TransformDirection(Vector3.left) * _speed * Time.deltaTime);
			transform.Translate(transform.TransformDirection(Vector3.back) * _speed * Time.deltaTime, Space.Self);
			//haveToStop = false;
		}
	}
		
	void OnTriggerEnter(Collider other) {
		//if(other.transform.tag == "vehicle")
	//	{
			Debug.Log("The collision  is entered------------------------------------------");
			Debug.Log(other);
			Debug.Log(gameObject.name);
			Debug.LogWarning ("Stopping vehicle after trigger");
			haveToStop = true;
			_speed = 0.0f;
	//	}
		Debug.Log ("speed " + _speed);
   	}	
	/*
	void OnCollisionExit(Collision other){
	}
	*/
	
	private string getVehicleLargerAxis(GameObject v){
		
		if(v.transform.localScale.x > v.transform.localScale.z)
			return "x";
		else
			return "z";
	}
}
