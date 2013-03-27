using UnityEngine;
using System.Collections;


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
	
	private GameMaster gameMasterScript;
		
	
	// Use this for initialization
	void Awake(){
		 gameMasterScript = GameObject.FindGameObjectWithTag("master").GetComponent<GameMaster>();
		
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
		_path			= myVehicle.MyPath;
		_endPosition 	= _path.EndPosition;
		
		_street 		= myVehicle.CurrentStreet;
		_direction 		= myVehicle.CurrentDirection;
		_speed 			= myVehicle.Speed;
		_size 			= myVehicle.Size;
		
		_light 			= _street.StreetLight;
		_stopPosition 	= _street.StopPosition;
		_myQueue 		= _street.StrQueue;
		_queueSize 		= _street.StrQueue.Count;
		
		lastDirection = _direction;
		
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
		SetStopOffset();
		PerformEnqueue();
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
		
		if( _direction == Direction.Right && (transform.position.x > _stopPosition ) ||
			_direction == Direction.Left && transform.position.x < _stopPosition   ||
			_direction == Direction.Down  && transform.position.z < _stopPosition ||
			_direction == Direction.Up && transform.position.z > _stopPosition ){
			
			
			passed = true ;
			
			if(!dequeued && !(_light.Stopped)){
				if(_myQueue.Count > 0){
					_myQueue.Dequeue();
					dequeued = true;
				//	boxColl.isTrigger = true;
					Debug.Log(gameObject.name +" is dequeued" );
					_street.VehiclesNumber --;
				}
			}
			if(myVehicle.NextStreet != null)
				TransfereToNextStreet();
			
		}
	}
	
	private void PerformEnqueue(){
		if(!enqueued ){
				_myQueue.Enqueue(gameObject);
				enqueued = true;
				Debug.Log(gameObject.name +" is enqueued" );
		}
	}
	
	private void SetStopOffset(){
	//	Debug.Log("QUEUE SIZEEEEEEE of  " + gameObject.name +"      "+ _queueSize);
		Offset =  (_queueSize) * (_size + 5);
	}
	
	private void SetupColliderSize(){
		boxColl = GetComponent<BoxCollider>();
		if(getVehicleLargerAxis(gameObject) == "x"){
			boxColl.size = new Vector3(.4f  , 1 , transform.localScale.z/1.5f  );
		}
		else{
			boxColl.size = new Vector3(transform.localScale.x/1.5f   , 1 , .4f );
		}
		
	}
	
	private void CheckAndDestroyAtEnd(){
		if(_direction == Direction.Left && transform.position.x < _endPosition.x && transform.position.z > _endPosition.z){
			Destroy(gameObject) ;
			gameMasterScript.score += 500;
		}
		else if(_direction == Direction.Right && transform.position.x > _endPosition.x && transform.position.z < _endPosition.z){
			Destroy(gameObject) ;
			gameMasterScript.score += 500;
		}
		else if(_direction == Direction.Down && transform.position.z < _endPosition.z ){
			Destroy(gameObject) ;
			gameMasterScript.score += 500;
		}
		else if(_direction == Direction.Up && transform.position.z > _endPosition.z ){
			Destroy(gameObject) ;
			gameMasterScript.score += 500;
		}
		
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
		//if(_light.tLight != null){
			if(_direction == Direction.Right && (_light.Stopped) && transform.position.x > _stopPosition - Offset ){
				_speed = 0.0f;
			}
			else if(_direction == Direction.Left && (_light.Stopped) && transform.position.x < _stopPosition + Offset ){
				_speed = 0.0f;
			}
			else if(_direction == Direction.Down && (_light.Stopped) && transform.position.z < _stopPosition + Offset ){
				_speed = 0.0f;
			}
			else if(_direction == Direction.Up && (_light.Stopped) && transform.position.z > _stopPosition - Offset ){
				_speed = 0.0f;
			}
		//}
	}
	
	
	private void Move(){
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
			haveToStop = true;
			_speed = 0.0f;
	//	}
		Debug.Log ("speed " + _speed);
		gameMasterScript.gameOver = true;
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
