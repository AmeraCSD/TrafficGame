using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class VehicleController : MonoBehaviour {
	
	public bool ImTheOneToMove;
	private int curScore;
 
	public Vehicle myVehicle;
	private float angerMount;
	//for the street
	private Street _street;
	private GamePath _path;
	private TrafficLight _light;
	private float _stopPosition;
	private Vector3 _endPosition;
	public Queue _myQueue;
	private int _queueSize;
	
	
	
	//for the vehicle
	
	public  StreetDirection _direction;
	private StreetDirection _nextDirection;
	
	public float speed;
	private float _size;
	public VehicleType vehType;
	
	private BoxCollider boxColl;	
	private GameObject triggeredObject;
	
	private bool passed;
	private bool dequeued;
	private bool enqueued;
	private bool gameOver;
	private bool satisfyAdjustedOnTime;
	
	public bool haveToReduceMySpeed;
	
	private float Offset;
	private int carsStillInsideNumber;
	
	private GameMaster gameMasterScript;
	private float stoppingTimerforAnger;
	private bool stoppingTimerforAngerSet;
	
	private List<int> serviceCarStops; 
	
	public float busStopTimer;
	private float serviceCarStopTimer;
	
	public GameObject angerSpriteGo;
	private GameObject myAngerSprite;
	
	// Use this for initialization
	void Awake(){
		angerMount = .25f;
		initInstancesAtFirst();
		
		angerSpriteGo = GameObject.FindGameObjectWithTag("comic");
		
		myAngerSprite = Instantiate(angerSpriteGo, transform.position, Quaternion.identity) as GameObject;
				
		myAngerSprite.SetActive(false);
		
		
	}
	
	public void initInstancesAtFirst(){
		gameMasterScript = GameObject.FindGameObjectWithTag("master").GetComponent<GameMaster>();
		boxColl = GetComponent<BoxCollider>();
		
		Offset = 0;
		dequeued = false;
		enqueued = false;
		passed = false;
		satisfyAdjustedOnTime = false;
		stoppingTimerforAnger = 0;
		stoppingTimerforAngerSet = false;
		haveToReduceMySpeed = false;
		busStopTimer = 0;
		serviceCarStopTimer = 0;
		
		ImTheOneToMove = false;
		
	}
	
	void Start () {
		InitStreetAndVehicleAttributes();
		gameObject.GetComponent<AudioSource>().clip = myVehicle.Horn;
	}
	
	public void InitStreetAndVehicleAttributes(){
		_path			= myVehicle.MyPath;		
		_street 		= myVehicle.CurrentStreet;
		_direction 		= myVehicle.CurrentDirection;
		speed 			= myVehicle.Speed;
		_size 			= myVehicle.Size;
		vehType			= myVehicle.Type;
		
		_light 			= _street.StreetLight;
		_stopPosition 	= _street.StopPosition;
		_endPosition 	= _street.EndPoint;
		_myQueue 		= _street.StrQueue;
		_queueSize 		= _myQueue.Count;
		
		_nextDirection = myVehicle.NextStreet.StreetLight.Type;
		
		if(vehType == VehicleType.ServiceCar){
			serviceCarStops = ServiceCar.SetGetServiceCarRandomStops(gameMasterScript.gameTime -9, gameMasterScript.gameTime-15);
		}
		
	}
	
	
	
	// Update is called once per frame
	void Update () {
		
		myAngerSprite.transform.localRotation = Quaternion.AngleAxis(90, Vector3.right);
		myAngerSprite.transform.parent = GameObject.FindGameObjectWithTag("panel").transform;
		myAngerSprite.transform.position = new Vector3(transform.position.x-10 , transform.position.y, transform.position.z+12);
		
		if(speed < 0){
			speed = 0.0f;
		}
		PerformEnqueue();
		SetStopOffset();
		CheckPosition_DeqIfPassed();
		
		Move();
		
		if(myVehicle.NextStreet!=null && MathsCalculatios.IsLeavingTheStreet(transform, _direction, _endPosition, _street)){
			TransfereToNextStreet();
		}
		CheckAndDeactivateAtEnd();
		if(!passed){
			//if(vehType != VehicleType.Thief)
				StopMovingOnRed();
		}
		CheckServiceCarStops();
		CheckMyAnger();
		
		
		if(busStopTimer >= gameMasterScript.gameTime){
			speed = myVehicle.Speed;
		}
		
		if(!(_light.Stopped) && !haveToReduceMySpeed){
			speed = myVehicle.Speed;
			
		}
		
	}
	
	
	private void PerformEnqueue(){
		if(!enqueued ){
			_myQueue.Enqueue(gameObject);	
			enqueued = true;
		}
	}
	
	private void SetStopOffset(){
		Offset =  (GetMyOrderInQueue()) * (_size + 9);
	}
	
	private void CheckPosition_DeqIfPassed(){
		if( _direction == StreetDirection.Right && transform.position.x >= _stopPosition    ||
			_direction == StreetDirection.Left && transform.position.x <= _stopPosition  ||
			_direction == StreetDirection.Down  && transform.position.z <= _stopPosition  ||
			_direction == StreetDirection.Up && transform.position.z >= _stopPosition ){
			
			passed = true ;
			if(!dequeued && (!(_light.Stopped) || StillInStopRange() ) ){				
				if(_myQueue.Count > 0){
					_myQueue.Dequeue();
					dequeued = true;
					_street.VehiclesNumber --;
					if(!satisfyAdjustedOnTime && vehType == VehicleType.Ambulance){
						GameObject.FindGameObjectWithTag("satisfyBar").GetComponent<SatisfyBar>().AddjustSatisfaction(-1);
						gameMasterScript.satisfyBar --;
						satisfyAdjustedOnTime = true;
					}					
				}
			}
			boxColl.isTrigger = true;
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
	
	private void TransfereToNextStreet(){
		ResetVehicleAttributes();
	}
	
	private void ResetVehicleAttributes(){
		if(myVehicle.NextStreet != null){
			myVehicle.CurrentStreet = myVehicle.NextStreet;
			myVehicle.CurrentDirection = myVehicle.CurrentStreet.StreetLight.Type;
			myVehicle.CurrentStreetNumber ++;
			
			if(myVehicle.CurrentStreetNumber != _path.PathStreets.Count ){
				myVehicle.NextStreet =_path.PathStreets[myVehicle.CurrentStreetNumber];
				_nextDirection = myVehicle.NextStreet.StreetLight.Type;
			}
			else{
				myVehicle.NextStreet = null;
				Debug.Log("next street is null " );
			}
			
			_street = myVehicle.CurrentStreet; 
			_direction 		= myVehicle.CurrentDirection;			
			
			_street.VehiclesNumber++;
			
			_light 			= _street.StreetLight;
			_stopPosition 	= _street.StopPosition;
			_endPosition 	= _street.EndPoint;
			_myQueue 		= _street.StrQueue;
			_queueSize 		= _street.StrQueue.Count;
			
			Offset = 0;
			dequeued = false;
			enqueued = false;
			passed = false;
			//haveToReduceMySpeed = false;
		}
	}
	
	private void CheckAndDeactivateAtEnd(){
		if(myVehicle.NextStreet == null){
			
			if(MathsCalculatios.CheckMyEndPosition(transform, _direction, _endPosition)){
				gameObject.SetActive(false);
				myAngerSprite.SetActive(false);
				if(vehType == VehicleType.Normal){
					gameMasterScript.existedVehicles.Enqueue(gameObject);
				}
				else{
					//if(vehType != VehicleType.Ambulance)
						gameMasterScript.trullyPassedEventsNum ++;
				}
				GameMaster.score += 1;
				
			}
		}
		
	}
	
	private void StopMovingOnRed(){
		if(_light.Stopped){
			if( (_direction == StreetDirection.Right && transform.position.x > _stopPosition - Offset ) ||
				(_direction == StreetDirection.Left && transform.position.x < _stopPosition + Offset) ||
				(_direction == StreetDirection.Down && transform.position.z < _stopPosition + Offset) ||
				(_direction == StreetDirection.Up && transform.position.z > _stopPosition - Offset)  ){
				//haveToReduceMySpeed = true;
				
				speed = 0;
				
				
				if(!satisfyAdjustedOnTime && vehType == VehicleType.Ambulance){
					GameObject.FindGameObjectWithTag("satisfyBar").GetComponent<SatisfyBar>().AddjustSatisfaction(2);
					gameMasterScript.satisfyBar += 2;
					Debug.Log("Ambulance stopped ... not good");
					satisfyAdjustedOnTime = true;
				}
			}
			else{
				if(!haveToReduceMySpeed)
					speed = myVehicle.Speed;
				if(triggeredObject != null && triggeredObject.tag == "intersection"){
					if(triggeredObject.GetComponent<IntersectionArea>().vehiclesOnMe.Count <= 1){
						haveToReduceMySpeed = false;
						speed = myVehicle.Speed;
					}
				}
				
			}	
		}
	}
	
	private void CheckServiceCarStops(){
		if(vehType == VehicleType.ServiceCar){
			if(ServiceCar.InsideServiceCarStops(serviceCarStops , gameMasterScript.gameTime)){
				if(serviceCarStopTimer ==0){
					serviceCarStopTimer = gameMasterScript.gameTime-2;
					//stop
					//Debug.Log("Stopping the service car" + gameMasterScript.gameTime );
					speed = 0;
					haveToReduceMySpeed = true;
				}
				
			}
			
			else if(gameMasterScript.gameTime <= serviceCarStopTimer){
				//move
				//Debug.Log("Moving the service car againnn" + gameMasterScript.gameTime );
				haveToReduceMySpeed = false;
				serviceCarStopTimer = 0;
			}
			if(serviceCarStopTimer != 0) {
				speed = 0;
				haveToReduceMySpeed = true;
			}
		}
		 
			
	}
	
	private void CheckMyAnger(){
		if(speed == 0 && GetMyOrderInQueue()== 0 && _street.StreetLight.Stopped){
			SetLightTimer();
				if(! stoppingTimerforAngerSet){
					stoppingTimerforAnger = gameMasterScript.gameTime - 15 ;	
					stoppingTimerforAngerSet = true;
				}
		}
		if(GetMyOrderInQueue()== 0 && _street.StreetLight.Stopped ){
			if(vehType != VehicleType.Ambulance && gameMasterScript.gameTime <= stoppingTimerforAnger){
				stoppingTimerforAngerSet = false;
				GameObject.FindGameObjectWithTag("satisfyBar").GetComponent<SatisfyBar>().AddjustSatisfaction(angerMount);
				gameMasterScript.satisfyBar += angerMount;
			//	satisfyAdjustedOnTime = true;
				stoppingTimerforAnger =0;
				angerMount *= 2;
				// comicccccccccccccccccccc and zamameeer
				myAngerSprite.SetActive(true);
				audio.PlayOneShot(myVehicle.Horn);
			}
		}
	}
	
	public void SetLightTimer(){
		if(_street.StreetLight.tLight!= null ){
			if(_street.StreetLight.tLight.tag == "lightLeft"){
				GameObject.FindGameObjectWithTag("timerLightLeft").GetComponent<LightTimerManager>().startTimer = true;
			}
			else if(_street.StreetLight.tLight.tag == "lightLeft1"){
				GameObject.FindGameObjectWithTag("timerLightLeft1").GetComponent<LightTimerManager>().startTimer = true;
			}
			else if(_street.StreetLight.tLight.tag == "lightRight"){
				GameObject.FindGameObjectWithTag("timerLightRight").GetComponent<LightTimerManager>().startTimer = true;
			}
			else if(_street.StreetLight.tLight.tag == "lightRight1"){
				GameObject.FindGameObjectWithTag("timerLightRight1").GetComponent<LightTimerManager>().startTimer = true;
			}
			else if(_street.StreetLight.tLight.tag == "lightUp"){
				GameObject.FindGameObjectWithTag("timerLightUp").GetComponent<LightTimerManager>().startTimer = true;
			}
			else if(_street.StreetLight.tLight.tag == "lightUp1"){
				GameObject.FindGameObjectWithTag("timerLightUp1").GetComponent<LightTimerManager>().startTimer = true;
			}
			else if(_street.StreetLight.tLight.tag == "lightDown"){
				GameObject.FindGameObjectWithTag("timerLightDown").GetComponent<LightTimerManager>().startTimer = true;
			}
			else if(_street.StreetLight.tLight.tag == "lightDown1"){
				GameObject.FindGameObjectWithTag("timerLightDown1").GetComponent<LightTimerManager>().startTimer = true;
			}
		}
	}
	
	private int GetMyOrderInQueue(){
		object [] array  = _myQueue.ToArray();
		return Array.IndexOf(array, gameObject);
	}
	
	
	public void SetStopTimeForBus(){
		busStopTimer = gameMasterScript.gameTime - 3;
	}
	
	
	
	private void Move(){
		
		if(_direction == StreetDirection.Left){
    		transform.localRotation = Quaternion.AngleAxis(180, Vector3.up);
			transform.Translate(transform.TransformDirection(Vector3.left) * speed * Time.deltaTime, Space.Self);
			
			Ray ray = new Ray(transform.position, Vector3.left);
			ReduceMeIfHit(ray);
		}
		else if(_direction == StreetDirection.Right){
    		transform.localRotation = Quaternion.AngleAxis(0, Vector3.up);
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
    		transform.localRotation = Quaternion.AngleAxis(270, Vector3.up);
			transform.Translate(transform.TransformDirection(Vector3.back) * speed * Time.deltaTime, Space.Self);
			
			Ray ray = new Ray(transform.position, Vector3.forward);
			ReduceMeIfHit(ray);
		}
	}
	
	private void ReduceMeIfHit(Ray ray){
		RaycastHit hit ;
		if(Physics.Raycast(ray, out hit, 8) ){
			Debug.DrawLine (ray.origin, hit.point);
			VehicleController hitVehicleController = hit.collider.gameObject.GetComponent<VehicleController>();
			if(hitVehicleController !=null && (hitVehicleController.speed < speed || haveToReduceMySpeed)  ){
				speed = hitVehicleController.speed;
				haveToReduceMySpeed = true;
			}
			else{
				if(hit.collider.tag == "intersection"){
					if(hit.collider.gameObject.GetComponent<IntersectionArea>().vehiclesOnMe.Count <= 1){
						haveToReduceMySpeed = false;
						
					}
				}
				else
					haveToReduceMySpeed = false;
			}
		}
		else{ 
			if(vehType == VehicleType.ServiceCar || vehType == VehicleType.Bus)
					speed = myVehicle.Speed;
			if((vehType != VehicleType.Bus || vehType != VehicleType.ServiceCar) &&(vehType != VehicleType.Caravan)&& (vehType != VehicleType.ServiceCar) && (vehType != VehicleType.Bus))
				haveToReduceMySpeed = false;
		}

	}
	
	void OnTriggerEnter(Collider other) {
		
		triggeredObject = other.gameObject;
		
		Debug.Log("on trigger enterrrrr");
	//	if(vehType == VehicleType.Thief || other.gameObject.GetComponent<VehicleController>().vehType == VehicleType.Thief){
			if(vehType == VehicleType.Thief){
				GameObject.Destroy(gameObject);
				GameObject.FindGameObjectWithTag("satisfyBar").GetComponent<SatisfyBar>().AddjustSatisfaction(-1);
				gameMasterScript.satisfyBar -= 1;
			}
			
	//	}
		
		if(other.tag == "vehicle"){
			if(other.gameObject.GetComponent<VehicleController>().ImTheOneToMove){
				other.gameObject.GetComponent<VehicleController>().haveToReduceMySpeed = true;
				other.gameObject.GetComponent<VehicleController>().speed = 0.0f;
			}
			else{
				ImTheOneToMove = true;
			}
			//gameMasterScript.gameOver = true;
		}
		else if(other.tag == "intersection"){
			if(other.gameObject.GetComponent<IntersectionArea>().vehiclesOnMe.Count >= 1){
				speed = 0;
				haveToReduceMySpeed = true;
			}
			else{
				haveToReduceMySpeed = false;
				speed = myVehicle.Speed;
			}
			if(_light.Stopped){
				haveToReduceMySpeed = false;
				speed = myVehicle.Speed;
			}
			
		}
		
		
		
   	}	
	
	void OnTriggerExit(Collider other) {
		triggeredObject = null;
		if(other.transform.tag == "vehicle"){
			Debug.Log("on trigger exit");
			other.gameObject.GetComponent<VehicleController>().haveToReduceMySpeed = false;
			other.gameObject.GetComponent<VehicleController>().speed = myVehicle.Speed;			//speed = myVehicle.Speed;
			ImTheOneToMove = false;
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