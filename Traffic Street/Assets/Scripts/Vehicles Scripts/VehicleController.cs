using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class VehicleController : MonoBehaviour {
	
	public bool ImTheOneToMove;
	private GameObject []  corners;
 
	public  Vehicle myVehicle;
	
	
	private Street 			_street; 
	private GamePath 		_path;
	public TrafficLight 	_light;
	private float 			_stopPosition;
	private Vector3 		_endPosition;
	
	public 	Queue 			_myQueue;
	public  StreetDirection _direction;
	private StreetDirection _nextDirection;
	
	private bool rotateNow;
	
	public Vector3 rotateAroundPosition;
	
	//for the vehicle
	
	
	
	public 	float speed;
	private float _size;
	public 	VehicleType vehType;
	
	private BoxCollider boxColl;	
	public GameObject triggeredObject;
	public List <GameObject> triggeredIntersections;
	
	private float angerMount;
	private bool passed;
	private bool dequeued;
	private bool enqueued;
	private bool gameOver;
	private bool satisfyAdjustedOnTime;
	
	public bool haveToReduceMySpeed;
	
	private GameMaster gameMasterScript;
	private float stoppingTimerforAnger;
	private bool stoppingTimerforAngerSet;
	
	private List<int> serviceCarStops; 
	private List<int> taxiStops;
	
	public float busStopTimer;
	public float taxiStopTimer;
	
	public GameObject angerSpriteGo;
	public GameObject myAngerSprite;
	
	public bool busStop;
	
	public List <Vector3>  wayPoints;
	private int currentWayPoint;
	
	private bool playedAlert;
	
	public RaycastHit hit ;
	//accleration variables
	public float dist;
	
	public bool pauseRotation;	
	
	public float myRayCastRange ;
	// Use this for initialization
	void Awake(){
		//globals = Globals.GetInstance();
		gameMasterScript = GameObject.FindGameObjectWithTag("master").GetComponent<GameMaster>();
		boxColl = GetComponent<BoxCollider>();
		corners = gameMasterScript.Corners;
		
		wayPoints = new List<Vector3>();
		
		
		initInstancesAtFirst();
		
		angerSpriteGo = GameObject.FindGameObjectWithTag("comic");
		
		myAngerSprite = Instantiate(angerSpriteGo, transform.position, Quaternion.identity) as GameObject;
				
		myAngerSprite.SetActive(false);
		
		
	}
	
	public void initInstancesAtFirst(){
		triggeredIntersections = new List<GameObject>();
		rotateAroundPosition = Vector3.zero;
		angerMount = Globals.angerMinAmount;
		playedAlert = false;
		dequeued = false;
		enqueued = false;
		passed = false;
		satisfyAdjustedOnTime = false;
		stoppingTimerforAnger = 0;
		stoppingTimerforAngerSet = false;
		haveToReduceMySpeed = false;
		busStopTimer = 0;
		taxiStopTimer =0;
		
		ImTheOneToMove = false;
		rotateNow = false;
		dist = 0;
		pauseRotation = false;
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
		
		_nextDirection = myVehicle.NextStreet.StreetLight.Type;
	
		myRayCastRange = Globals.RAY_CAST_RANGE + _size;
		if(vehType == VehicleType.Taxi){
			taxiStops = Taxi.SetGetTaxiRandomStops(gameMasterScript.gameTime -5, gameMasterScript.gameTime-10);
		}
		
	}
	
	
	
	// Update is called once per frame
	
	private void SetAngerSpriteWithMe(){
		myAngerSprite.transform.localRotation = Quaternion.AngleAxis(90, Vector3.right);
		myAngerSprite.transform.parent = GameObject.FindGameObjectWithTag("panel").transform;
		myAngerSprite.transform.position = new Vector3(transform.position.x-10 , transform.position.y, transform.position.z+12);
	}
	
	private void ClampSpeed(){
		if(speed < 0){
			speed = 0.0f;
		}
	}
	
	private void CheckPosition_Deaccelerate(){
		//Debug.Log(_nextDirection);
		if(_direction != _nextDirection){
			
			MathsCalculatios.HaveToAccelerate(transform, _direction, _endPosition, _street, this);
		}
	}
	
	private void LeaveStreet_Rotate(){
		if(myVehicle.NextStreet!=null){
			if(_direction != _nextDirection){
				if(MathsCalculatios.IsLeavingTheStreet_Rotate(corners, transform, _direction, _endPosition, _street, _nextDirection, this)){
					//if(myVehicle.NextStreet.VehiclesNumber < myVehicle.NextStreet.StreetCapacity)
					rotateNow = true;
					speed = 0;	
				}
			}
			else {
				if(MathsCalculatios.IsLeavingTheStreet(transform, _direction, _endPosition, _street)){
					TransfereToNextStreet();
				}
			}
		}
		if(rotateNow && _direction != _nextDirection && !pauseRotation){
			RotateVehicle();
		}
		if(_direction != _nextDirection && MathsCalculatios.HasFinishedRotation(transform.forward, rotateNow,_direction, _nextDirection, this)){
			rotateNow = false;
			haveToReduceMySpeed = false;
			TransfereToNextStreet();
		}
	}
	
	private void StopTheBus(){
		if(busStop){
			haveToReduceMySpeed = true;
			speed = 5;
			int rate=3; 
			if(currentWayPoint < wayPoints.Count){
		 
		        Vector3 target= wayPoints[currentWayPoint];
		 		Vector3 Position = transform.position;
				
				if(currentWayPoint == 0){
					if(Position.x<target.x)
					{
						Position.x +=5*Time.deltaTime ;
						transform.position = Position;
					}	
				}
		       	else{
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
							Position.z -=2*Time.deltaTime ;
							transform.position = Position;
						}
					}
				}
				if((target - transform.position).magnitude <1){
					currentWayPoint++;
					Debug.Log("bus is here");
					if(currentWayPoint == wayPoints.Count){
						busStop =false;
						transform.forward = -1*Vector3.right;
					}
				}
			}
		}
	}
	
	void Update () {
		
		
		SetAngerSpriteWithMe();
		ClampSpeed();
		
		PerformEnqueue();
		CheckPosition_DeqIfPassed();
		
		CheckPosition_Deaccelerate();
		
		if(!rotateNow && !busStop)
			Move();
	
		LeaveStreet_Rotate();
		
		CheckAndDeactivateAtEnd();
		
		
		if(!passed && GetMyOrderInQueue()==0){
			if(vehType != VehicleType.Thief &&  vehType != VehicleType.Police)
				StopMovingOnRed();
		}
		
		
		CheckTaxiStops();
		CheckMyAnger();
		
		
		if(!(_light.Stopped) && !haveToReduceMySpeed){
			speed += MathsCalculatios.CalculateAcclerationByNewtonFormula(speed, myVehicle.Speed, Globals.ACCELERATE_FORWARD_RANGE );
		//	speed = myVehicle.Speed;
			
			dist = 0;
			
		}
		
		StopTheBus();
		
	}
	
	private void RotateVehicle(){
		float selfRotateSpeed = 40;
		float worldRotateSpeed = 120;
		
		if(vehType!= VehicleType.Thief && vehType!= VehicleType.Police)
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
			transform.Rotate(transform.up* selfRotateSpeed * Time.deltaTime, Space.Self);
			transform.RotateAround(rotateAroundPosition, Vector3.up, worldRotateSpeed  * Time.deltaTime);
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
			
			//Debug.Log(myVehicle.CurrentStreetNumber);
			
			if(myVehicle.CurrentStreetNumber+1 != _path.PathStreets.Count ){
				myVehicle.NextStreet =_path.PathStreets[myVehicle.CurrentStreetNumber+1];
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
			
			dequeued = false;
			enqueued = false;
			passed = false;
			stoppingTimerforAnger = 0;
			stoppingTimerforAngerSet = false;
			satisfyAdjustedOnTime = false;
			angerMount = 0.5f;
			dist = 0;
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
			
		//	int result = MathsCalculatios.CheckStoppingPosition(_direction, transform.position, _stopPosition );
			
			if(MathsCalculatios.CheckStoppingPosition(_direction, transform.position, _stopPosition)){
				if(dist==0){
					dist = MathsCalculatios.GetDistanceBetweenVehicleAndOtherPosition(transform.position, _stopPosition , _direction);
				}
			//	Debug.Log(dist);
				
				speed += MathsCalculatios.CalculateAcclerationByNewtonFormula ( myVehicle.Speed, 
																					0, 
																					dist);
				
				 
			
			}
			if(MathsCalculatios.EndAccelerationOnArrival(_direction, transform.position, _stopPosition)){
				//Debug.Log(gameObject.name + " " + speed);
				speed =0;
				haveToReduceMySpeed = false;
				dist = 0;
			}
			
			if(!satisfyAdjustedOnTime && vehType == VehicleType.Ambulance){
				GameObject.FindGameObjectWithTag("satisfyBar").GetComponent<SatisfyBar>().AddjustSatisfaction(2);
				gameMasterScript.satisfyBar += 2;
				satisfyAdjustedOnTime = true;
			}
			
				
		}
		else{
			/*
			if(triggeredObject != null && triggeredObject.tag == "intersection"){
				if(triggeredObject.GetComponent<IntersectionArea>().vehiclesOnMe.Count <= 1 ){
					//speed += MathsCalculatios.CalculateAcclerationByNewtonFormula(speed, myVehicle.Speed, 5 );
					haveToReduceMySpeed = false;
					speed = myVehicle.Speed;
				}
			}
				*/
		}	
		
	}
	
	
	private void CheckTaxiStops(){
		if(vehType == VehicleType.Taxi){
			if(Taxi.InsideTaxiStops(taxiStops , gameMasterScript.gameTime)){
				if(taxiStopTimer ==0){
					taxiStopTimer = gameMasterScript.gameTime-4;
					//speed -= 7;
					
				//	speed += MathsCalculatios.CalculateAcclerationByNewtonFormula(myVehicle.Speed, 0, Globals.ACCELERATE_FORWARD_RANGE );
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
			//	speed -= 10;
				speed += MathsCalculatios.CalculateAcclerationByNewtonFormula(myVehicle.Speed, 0, 15 );
				haveToReduceMySpeed = true;
			}
		}
		 
			
	}
	
	private void CheckMyAnger(){
		if(speed < 0.7f && GetMyOrderInQueue()== 0 && _street.StreetLight.Stopped){
				if(! stoppingTimerforAngerSet){
					stoppingTimerforAnger = gameMasterScript.gameTime - Globals.angerMinTime ;
					stoppingTimerforAngerSet = true;
					playedAlert = false;
				}
		}
		
		if(GetMyOrderInQueue()== 0 && _street.StreetLight.Stopped && vehType!=VehicleType.Taxi){
			if(gameMasterScript.gameTime <= stoppingTimerforAnger+ Globals.WARNING_MESSAGE_TIMER && !playedAlert){
				MessageBar.messagesQ.Enqueue(Globals.ANGERY_CAR_MESSAGE);
				//	gameMasterScript.eventWarningLabel.text = Globals.ANGERY_CAR_MESSAGE;
			//	MessageBar.notifyNow = true;
				playedAlert = true;
			}
			if(gameMasterScript.gameTime <= stoppingTimerforAnger){
				stoppingTimerforAngerSet = false;
				GameObject.FindGameObjectWithTag("satisfyBar").GetComponent<SatisfyBar>().AddjustSatisfaction(angerMount);
				gameMasterScript.satisfyBar += angerMount;
			//	satisfyAdjustedOnTime = true;
				gameMasterScript.secondsCounterForAnger = 0;
				stoppingTimerforAnger =0;
				angerMount *= Globals.nextAngerIncreaseRate;
				// comicccccccccccccccccccc and zamameeer
				myAngerSprite.SetActive(true);
				audio.PlayOneShot(myVehicle.Horn);
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
		//Debug.Log("speeddd "+speed);
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
		
		if(vehType!= VehicleType.Thief && Physics.Raycast(ray, out hit, myRayCastRange) ){
			Debug.DrawLine (ray.origin, hit.point);
			VehicleController hitVehicleController = hit.collider.gameObject.GetComponent<VehicleController>();
			if(hitVehicleController !=null && (hitVehicleController.speed < speed || haveToReduceMySpeed || hitVehicleController.haveToReduceMySpeed)  ){
				//speed = hitVehicleController.speed;
				if(dist==0){
					dist = MathsCalculatios.GetDistanceBetweenVehicleAndOtherPosition(transform.position, hit.collider.transform.position , _direction);
				}
				//Debug.Log(dist);
				if(speed > hitVehicleController.speed){
					
						
					speed += MathsCalculatios.CalculateAcclerationByNewtonFormula ( myVehicle.Speed, 
																					hitVehicleController.speed, 
																					myRayCastRange/3);
				}
				haveToReduceMySpeed = true;
				
			}
			else{
				/////////////////////////////************************** we will return here
				if(hit.collider.tag == "human"){
					speed = 0;
					haveToReduceMySpeed = true;
				}
				
				else if(hit.collider.tag == "intersection"){
					IntersectionArea intersection = hit.collider.gameObject.GetComponent<IntersectionArea>();
					
					if(intersection.vehiclesOnMe.Count !=0){
						
						if(intersection.vehiclesOnMe.Count == 1 &&  triggeredIntersections.Contains(hit.collider.gameObject) ){
							haveToReduceMySpeed = false;
							
						}
						else{
							//if(rotateNow){
						//	pauseRotation = true;
						//	}
							if(dist==0){
								dist = MathsCalculatios.GetDistanceBetweenVehicleAndOtherPosition(transform.position, hit.collider.transform.position , _direction);
							}
							speed += MathsCalculatios.CalculateAcclerationByNewtonFormula ( myVehicle.Speed, 
																							0, 
																							myRayCastRange);
							
							haveToReduceMySpeed = true;
						}
					}
					
					else{
						if(_light.Stopped && !MathsCalculatios.CompareTwoPositionsWRTDirections(_direction, transform.position, _stopPosition, 3)){
							speed += MathsCalculatios.CalculateAcclerationByNewtonFormula ( myVehicle.Speed, 
																							0, 
																							myRayCastRange-2);
							haveToReduceMySpeed = true;
						}
						else	
							haveToReduceMySpeed = false;
					}
					
				}
				else
					haveToReduceMySpeed = false;
			}
		}
		else{ 
		//	if(vehType == VehicleType.ServiceCar || vehType == VehicleType.Bus || vehType == VehicleType.Taxi || vehType == VehicleType.Caravan){
				//	speed = myVehicle.Speed;
		//		speed += MathsCalculatios.CalculateAcclerationByNewtonFormula(speed, myVehicle.Speed, Globals.ACCELERATE_FORWARD_RANGE );
		//	}
		//	if((vehType != VehicleType.Bus || vehType != VehicleType.ServiceCar || vehType != VehicleType.Taxi ||  vehType != VehicleType.Caravan)  &&(vehType != VehicleType.Caravan)&& (vehType != VehicleType.ServiceCar) && (vehType != VehicleType.Bus) && (vehType != VehicleType.Taxi))
				haveToReduceMySpeed = false;
		}

	}
	
	void OnTriggerEnter(Collider other) {
	//	dist =0;
		triggeredObject = other.gameObject;
		
		if(other.tag == "intersection" && !triggeredIntersections.Contains(other.gameObject)){
		//	Debug.Log(other.gameObject +" has been added");
			triggeredIntersections.Add(other.gameObject);
		}
		
		if(other.tag == "intersection"){
			if(_light.Stopped){
				haveToReduceMySpeed = false;
				speed = myVehicle.Speed;
			}
		}
		
		
	//	if(triggeredObject.tag == "human"){
	//		speed = 0;
	//		haveToReduceMySpeed = true;
	//	}
		
//		Debug.Log("on trigger enterrrrr");
//		if(vehType == VehicleType.Thief || other.gameObject.GetComponent<VehicleController>().vehType == VehicleType.Thief){
		
		if(vehType == VehicleType.Thief && other.gameObject.tag == "vehicle"){
			if(vehType == VehicleType.Thief){
				gameObject.SetActive(false);
				other.gameObject.GetComponent<VehicleController>().myAngerSprite.SetActive(false);
				//other.gameObject.SetActive(false);
				GameObject.FindGameObjectWithTag("satisfyBar").GetComponent<SatisfyBar>().AddjustSatisfaction(-1);
				gameMasterScript.satisfyBar -= 1;
				gameMasterScript.eventWarningLabel.text = "Well Done! Police is honering you";
				
			}
			
		}
		
		/*
		if(other.tag == "vehicle"){
			
			if(other.gameObject.GetComponent<VehicleController>().ImTheOneToMove){
				haveToReduceMySpeed = true;
				speed = 0.0f;
				
			//	if(other.gameObject.GetComponent<VehicleController>()._direction == _direction)
				//	speed =	other.gameObject.GetComponent<VehicleController>().speed;
			}
			else{
				ImTheOneToMove = true;
				haveToReduceMySpeed = false;
			//	other.gameObject.GetComponent<VehicleController>().pauseRotation = true;
			}
			
			//gameMasterScript.gameOver = true;
		}
		*/
		/*
		else if(other.tag == "intersection"){
			if(other.gameObject.GetComponent<IntersectionArea>().vehiclesOnMe.Count == 1 ){
				haveToReduceMySpeed = false;
				speed = myVehicle.Speed;
			}
			else {
				speed = 0;
				haveToReduceMySpeed = true;
			}
		}
		*/
		/*
		else if(other.tag == "intersection"){
			if(other.gameObject.GetComponent<IntersectionArea>().vehiclesOnMe.Count >= 1 ){
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
		*/
		
		
   	}	
	
	void OnTriggerExit(Collider other) {
		triggeredIntersections.Remove(other.gameObject);
//			Debug.Log("removing "+ other.gameObject );
		
		if(triggeredObject != null && triggeredObject.tag == "human"){
			haveToReduceMySpeed = false;
		}
		else{
			haveToReduceMySpeed = false;
			triggeredObject = null;
			if(other.transform.tag == "vehicle"){
				VehicleController vecCont = other.gameObject.GetComponent<VehicleController>();
				//Debug.Log("on trigger exit");
				vecCont.haveToReduceMySpeed = false;
				vecCont.speed = myVehicle.Speed;			//speed = myVehicle.Speed;
				ImTheOneToMove = false;
			//	pauseRotation = false;
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