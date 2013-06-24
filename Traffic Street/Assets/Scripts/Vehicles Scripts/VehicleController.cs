using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class VehicleController : MonoBehaviour {
	
	public bool ImTheOneToMove;
	private int curScore;
	private GameObject []  corners;
 
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
	private bool rotateNow;
	
	
	public Vector3 rotateAroundPosition;
	
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
	private List<int> taxiStops;
	
	public float busStopTimer;
	public float serviceCarStopTimer;
	public float taxiStopTimer;
	
	public GameObject angerSpriteGo;
	private GameObject myAngerSprite;
	
	public bool busStop;
	
	public List <Vector3>  wayPoints;
	private int currentWayPoint;
	
	// Use this for initialization
	void Awake(){
		wayPoints = new List<Vector3>();
		
		
		
		rotateAroundPosition = Vector3.zero;
		angerMount = .5f;
		initInstancesAtFirst();
		
		angerSpriteGo = GameObject.FindGameObjectWithTag("comic");
		
		myAngerSprite = Instantiate(angerSpriteGo, transform.position, Quaternion.identity) as GameObject;
				
		myAngerSprite.SetActive(false);
		
		
	}
	
	public void initInstancesAtFirst(){
		gameMasterScript = GameObject.FindGameObjectWithTag("master").GetComponent<GameMaster>();
		boxColl = GetComponent<BoxCollider>();
		
		corners = gameMasterScript.Corners;
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
		taxiStopTimer =0;
		
		ImTheOneToMove = false;
		rotateNow = false;
		
	}
	
	void Start () {
		InitStreetAndVehicleAttributes();
		if(myVehicle.Horn != null)
			gameObject.GetComponent<AudioSource>().clip = myVehicle.Horn;
		
		if(myVehicle.Type == VehicleType.Bus){
			for(int i=0; i<Globals.busStoppers.Count; i++){
				wayPoints.Add(Globals.busStoppers[i].transform.position);
			}	
		}
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
		
	//	if(vehType == VehicleType.ServiceCar){
	//		serviceCarStops = ServiceCar.SetGetServiceCarRandomStops(gameMasterScript.gameTime -9, gameMasterScript.gameTime-15);
	//	}
		
		if(vehType == VehicleType.Taxi){
			taxiStops = Taxi.SetGetTaxiRandomStops(gameMasterScript.gameTime -5, gameMasterScript.gameTime-10);
		}
		
	}
	
	
	
	// Update is called once per frame
	
	void Update () {
		
		
		
		
		if(speed < 0){
			speed = 0.0f;
		}
		PerformEnqueue();
		SetStopOffset();
		CheckPosition_DeqIfPassed();
		
		if(_direction != _nextDirection)
			MathsCalculatios.HaveToAccelerate(transform, _direction, _endPosition, _street, this);
		
		if(!rotateNow && !busStop)
			Move();
		
		if(myVehicle.NextStreet!=null){
			if(_direction != _nextDirection){
				if(MathsCalculatios.IsLeavingTheStreet_Rotate(corners, transform, _direction, _endPosition, _street, _nextDirection, this)){
					rotateNow = true;
				}
			}
			else {
				if(MathsCalculatios.IsLeavingTheStreet(transform, _direction, _endPosition, _street)){
					TransfereToNextStreet();
				}
			}
		//	Debug.Log("yaaady el neelah el soodaaaaaaaaaaa");
			
			
		}
		
		///////////////////////rotationnnn
		if(rotateNow && _direction != _nextDirection){
			RotateVehicle();
		}
		
//		Debug.Log(-1*transform.forward + " compared with  " + Vector3.forward);
		
//		Debug.LogWarning(transform.forward);
		
		
		
		if(_direction != _nextDirection && MathsCalculatios.HasFinishedRotation(transform.forward, rotateNow,_direction, _nextDirection, this)){
			//Debug.Log("hall mn mazeeeeeeeed");
			rotateNow = false;
			haveToReduceMySpeed = false;
			TransfereToNextStreet();
			//rotateAroundPosition = Vector3.zero;
		}
	//	Debug.Log(rotateNow);
		//////////////////////////////////////////////////////////////
		
		
		
		
		CheckAndDeactivateAtEnd();
		
		
		if(!passed){
			if(vehType != VehicleType.Thief &&  vehType != VehicleType.Police)
				StopMovingOnRed();
		}
//		CheckServiceCarStops();
		CheckTaxiStops();
		CheckMyAnger();
		
		/*
		if(busStopTimer >= gameMasterScript.gameTime){
			speed = myVehicle.Speed;
		}
		*/
		
		
		if(!(_light.Stopped) && !haveToReduceMySpeed){
			speed = myVehicle.Speed;
			
		}
		
		/////Taxi's spot
		if(busStop){
			speed = 5;
			//haveToReduceMySpeed = true;
			int rate=3; 
			//Vector3 target = Vector3.zero;
			if(currentWayPoint < wayPoints.Count){
		 
		        Vector3 target= wayPoints[currentWayPoint];
		 		Vector3 Position = transform.position;
				
				if(currentWayPoint == 0){
					//speed = 35;
					//transform.LookAt(target);
					if(Position.x<target.x)
					{
						
						Position.x +=5*Time.deltaTime ;
						transform.position = Position;
					}	
					
				//	transform.Translate(transform.TransformDirection(-1*transform.forward) * speed * Time.deltaTime, Space.Self);
					
					
				}
		       	else{
		    
			    	//transform.LookAt(-1*target);
					
//					Debug.Log("currentWayPoint "+currentWayPoint);
					if(currentWayPoint == 1 ){
					 	transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-1*target - transform.position),rate*.5f*Time.deltaTime );
					}
					else {	
						if(currentWayPoint == 3 ){
							SetStopTimeForBus();
							speed =0;
							if(busStopTimer >= gameMasterScript.gameTime){
								speed =5;
								transform.rotation =Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-1*target - (-1*transform.position)),rate*.5f*Time.deltaTime );
							
							}
						}
						else{
						transform.rotation =Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-1*target - (-1*transform.position)),rate*.5f*Time.deltaTime );
						}
					}
					
					if(Position.x<target.x)
					{
						if(currentWayPoint == 3) {
							if(busStopTimer >= gameMasterScript.gameTime){
								Position.x +=rate*Time.deltaTime ;
								transform.position = Position;
							}
						}
						else{
							Position.x +=rate*Time.deltaTime ;
							transform.position = Position;
						}
					}
					if(currentWayPoint == 3){
						if(busStopTimer >= gameMasterScript.gameTime){
							if(Position.z<target.z){
								Position.z +=rate*Time.deltaTime ;
								
								transform.position = Position;
							}
						}
						
					}
					else{
						
						if(Position.z>target.z){
						/*	if(currentWayPoint == 3){  
								
									Position.z -=2*Time.deltaTime ;
									
									transform.position = Position;
								}
							}
							else{
						*/
								Position.z -=2*Time.deltaTime ;
								
								transform.position = Position;
									
							//}
						}
					}
				}
				if((target - transform.position).magnitude <1){
				//if(transform.position.x>target.x && ){
					currentWayPoint++;
					Debug.Log("bus is here");
					if(currentWayPoint == wayPoints.Count){
						busStop =false;
						transform.forward = -1*Vector3.right;
					}
				}
				
			//	Debug.Log("safwatttttttttt "+ -1*transform.right);
				//transform.Translate(transform.TransformDirection(-1*transform.forward) * 10 * Time.deltaTime, Space.World);
			}
		}
		
	}
	
	private void RotateVehicle(){
		float selfRotateSpeed = 40;
		float worldRotateSpeed = 120;
		haveToReduceMySpeed = true;
		if(_nextDirection == StreetDirection.Up && _direction == StreetDirection.Left){
			transform.Rotate(transform.up* selfRotateSpeed * Time.deltaTime, Space.Self);
			transform.RotateAround(rotateAroundPosition, Vector3.up, worldRotateSpeed * Time.deltaTime);
		}
		
		else if(_nextDirection == StreetDirection.Up && _direction == StreetDirection.Right){
			transform.Rotate(-1*transform.up* selfRotateSpeed * Time.deltaTime, Space.Self);
			transform.RotateAround(rotateAroundPosition,-1* Vector3.up, worldRotateSpeed  * Time.deltaTime);
		}
		
		else if(_nextDirection == StreetDirection.Down && _direction == StreetDirection.Left){
			transform.Rotate(-1*transform.up* selfRotateSpeed * Time.deltaTime, Space.Self);
			transform.RotateAround(rotateAroundPosition, -1*Vector3.up, worldRotateSpeed * Time.deltaTime);
		}
		
		else if(_nextDirection == StreetDirection.Down && _direction == StreetDirection.Right){
			transform.Rotate(transform.up* selfRotateSpeed * Time.deltaTime, Space.Self);
			transform.RotateAround(rotateAroundPosition,Vector3.up, worldRotateSpeed  * Time.deltaTime);
		}
		
		else if(_nextDirection == StreetDirection.Left && _direction == StreetDirection.Up){
			transform.Rotate(-1*transform.up* selfRotateSpeed * Time.deltaTime, Space.Self);
			transform.RotateAround(rotateAroundPosition, -1*Vector3.up, worldRotateSpeed * Time.deltaTime);
		}
		
		else if(_nextDirection == StreetDirection.Left && _direction == StreetDirection.Down){
			//Debug.Log("here weee gooooo");
			transform.Rotate(transform.up* selfRotateSpeed * Time.deltaTime, Space.Self);
			transform.RotateAround(rotateAroundPosition, Vector3.up, worldRotateSpeed  * Time.deltaTime);
		}
		
		else if(_nextDirection == StreetDirection.Right && _direction == StreetDirection.Up){
			transform.Rotate(-1*transform.up* selfRotateSpeed * Time.deltaTime, Space.Self);
			transform.RotateAround(rotateAroundPosition, -1*Vector3.up, worldRotateSpeed  * Time.deltaTime);
		}
		
		else if(_nextDirection == StreetDirection.Right && _direction == StreetDirection.Down){
			transform.Rotate(-1*transform.up* selfRotateSpeed *  Time.deltaTime, Space.Self);
			transform.RotateAround(rotateAroundPosition, -1*Vector3.up, worldRotateSpeed  * Time.deltaTime);
		}
		
	//	Debug.DrawLine (transform.position, rotateAroundPosition);
	//	Debug.Log("rotate around position : "+rotateAroundPosition);
	}
	
	
	
	private void PerformEnqueue(){
		if(!enqueued ){
			_myQueue.Enqueue(gameObject);	
			enqueued = true;
		}
	}
	
	private void SetStopOffset(){
		Offset = _size*4 + (GetMyOrderInQueue() * (_size + 9));
	}
	
	private void CheckPosition_DeqIfPassed(){
		if( _direction == StreetDirection.Right && transform.position.x >= _stopPosition    ||
			_direction == StreetDirection.Left && transform.position.x <= _stopPosition  ||
			_direction == StreetDirection.Down  && transform.position.z <= _stopPosition  ||
			_direction == StreetDirection.Up && transform.position.z >= _stopPosition ){
			
			passed = true ;
			if(!dequeued && (!(_light.Stopped) || StillInStopRange() ) ){				
				if(_myQueue.Count > 0){
					//////////////////////////////////////////
					//animation.Play("rotateLeftAnimation");
					
				//	Animator a = GetComponent<Animator>();
					
					/////////////////////////////////////
					
					
					
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
		rotateAroundPosition = Vector3.zero;
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
//				Debug.Log("next street is null " );
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
			stoppingTimerforAngerSet = false;
			//haveToReduceMySpeed = false;
		}
	}
	
	private void CheckAndDeactivateAtEnd(){
		if(myVehicle.NextStreet == null){
			
			if(MathsCalculatios.CheckMyEndPosition(transform, _direction, _endPosition)){
				if(vehType == VehicleType.Thief){
					gameMasterScript.eventWarningLabel.text = "Oh No! Thief has escaped !!!";
				}
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
	
	/*
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
	*/
	
	private void CheckTaxiStops(){
		if(vehType == VehicleType.Taxi){
			if(Taxi.InsideTaxiStops(taxiStops , gameMasterScript.gameTime)){
				if(taxiStopTimer ==0){
					taxiStopTimer = gameMasterScript.gameTime-4;
					speed -= 2;
					haveToReduceMySpeed = true;
				}
				
			}
			
			else if(gameMasterScript.gameTime <= taxiStopTimer){
				//move
				//Debug.Log("Moving the service car againnn" + gameMasterScript.gameTime );
				haveToReduceMySpeed = false;
				taxiStopTimer = 0;
			}
			
			if(taxiStopTimer != 0) {
				speed -= 10;
				haveToReduceMySpeed = true;
			}
		}
		 
			
	}
	
	private void CheckMyAnger(){
		if(speed == 0 && GetMyOrderInQueue()== 0 && _street.StreetLight.Stopped){
			SetLightTimer();
				if(! stoppingTimerforAngerSet){
					stoppingTimerforAnger = gameMasterScript.gameTime - Globals.ANGER_TIMER ;
					stoppingTimerforAngerSet = true;
				}
		}
		if(GetMyOrderInQueue()== 0 && _street.StreetLight.Stopped ){
			if(gameMasterScript.gameTime <= stoppingTimerforAnger){
				stoppingTimerforAngerSet = false;
				GameObject.FindGameObjectWithTag("satisfyBar").GetComponent<SatisfyBar>().AddjustSatisfaction(angerMount);
				gameMasterScript.satisfyBar += angerMount;
			//	satisfyAdjustedOnTime = true;
				gameMasterScript.secondsCounterForAnger = 0;
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
		if(busStopTimer ==0){
			busStopTimer = gameMasterScript.gameTime - 3;
		}
	}
	
	public void SetStopTimeForTaxi(){
		if(taxiStopTimer ==0){
			taxiStopTimer = gameMasterScript.gameTime - 3;
		}
	}
	
	
	
	private void Move(){
		
		if(_direction == StreetDirection.Left){
			if(myVehicle.CurrentStreetNumber == 0){
    			transform.localRotation = Quaternion.AngleAxis(90, Vector3.up);
			}
			transform.Translate(transform.TransformDirection(transform.forward) * speed * Time.deltaTime, Space.Self);
			
			Ray ray = new Ray(transform.position, Vector3.left);
			ReduceMeIfHit(ray);
		}
		else if(_direction == StreetDirection.Right){
			if(myVehicle.CurrentStreetNumber == 0){
    			transform.localRotation = Quaternion.AngleAxis(270, Vector3.up);
			}
			transform.Translate(transform.TransformDirection(transform.forward) * speed * Time.deltaTime, Space.Self);
			
			Ray ray = new Ray(transform.position, Vector3.right);
			ReduceMeIfHit(ray);
		}
		else if(_direction == StreetDirection.Down){
			if(myVehicle.CurrentStreetNumber == 0){
    			transform.localRotation = Quaternion.AngleAxis(0, Vector3.up);
			}
			transform.Translate(transform.TransformDirection(-1*transform.forward) * speed * Time.deltaTime, Space.Self);
			
			Ray ray = new Ray(transform.position, Vector3.back);
			ReduceMeIfHit(ray);
		}
		else if(_direction == StreetDirection.Up){
			if(myVehicle.CurrentStreetNumber == 0){
    			transform.localRotation = Quaternion.AngleAxis(180, Vector3.up);
			}
		
			transform.Translate(transform.TransformDirection(-1*transform.forward) * speed * Time.deltaTime, Space.Self);
			
			Ray ray = new Ray(transform.position, Vector3.forward);
			ReduceMeIfHit(ray);
		}
	}
	
	private void ReduceMeIfHit(Ray ray){
		RaycastHit hit ;
		if(vehType!= VehicleType.Thief && Physics.Raycast(ray, out hit, 8) ){
			Debug.DrawLine (ray.origin, hit.point);
			VehicleController hitVehicleController = hit.collider.gameObject.GetComponent<VehicleController>();
			if(hitVehicleController !=null && (hitVehicleController.speed < speed || haveToReduceMySpeed)  ){
				speed = hitVehicleController.speed;
				haveToReduceMySpeed = true;
			}
			else{
				if(hit.collider.tag == "human"){
					speed = 0;
					haveToReduceMySpeed = true;
				}
				else if(hit.collider.tag == "intersection"){
					if(hit.collider.gameObject.GetComponent<IntersectionArea>().vehiclesOnMe.Count <= 1){
						haveToReduceMySpeed = false;
						
					}
				}
				else
					haveToReduceMySpeed = false;
			}
		}
		else{ 
			if(vehType == VehicleType.ServiceCar || vehType == VehicleType.Bus || vehType == VehicleType.Taxi || vehType == VehicleType.Caravan)
					speed = myVehicle.Speed;
			if((vehType != VehicleType.Bus || vehType != VehicleType.ServiceCar || vehType != VehicleType.Taxi ||  vehType != VehicleType.Caravan)  &&(vehType != VehicleType.Caravan)&& (vehType != VehicleType.ServiceCar) && (vehType != VehicleType.Bus) && (vehType != VehicleType.Taxi))
				haveToReduceMySpeed = false;
		}

	}
	
	void OnTriggerEnter(Collider other) {
		
		triggeredObject = other.gameObject;
		
	//	if(triggeredObject.tag == "human"){
	//		speed = 0;
	//		haveToReduceMySpeed = true;
	//	}
		
//		Debug.Log("on trigger enterrrrr");
//		if(vehType == VehicleType.Thief || other.gameObject.GetComponent<VehicleController>().vehType == VehicleType.Thief){
		
		if(vehType == VehicleType.Thief && other.gameObject.tag == "vehicle"){
			if(vehType == VehicleType.Thief){
				gameObject.SetActive(false);
				other.gameObject.GetComponent<VehicleController>().myAngerSprite.SetActive(false);;
				//other.gameObject.SetActive(false);
				GameObject.FindGameObjectWithTag("satisfyBar").GetComponent<SatisfyBar>().AddjustSatisfaction(-1);
				gameMasterScript.satisfyBar -= 1;
				gameMasterScript.eventWarningLabel.text = "Well Done! Police has honered you";
			}
			
		}
		
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
		if(triggeredObject != null && triggeredObject.tag == "human"){
			haveToReduceMySpeed = false;
		}
		else{
			triggeredObject = null;
			if(other.transform.tag == "vehicle"){
				VehicleController vecCont = other.gameObject.GetComponent<VehicleController>();
				//Debug.Log("on trigger exit");
				vecCont.haveToReduceMySpeed = false;
				vecCont.speed = myVehicle.Speed;			//speed = myVehicle.Speed;
				ImTheOneToMove = false;
			}
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