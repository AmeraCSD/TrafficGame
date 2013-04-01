using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Game master. This class is responsble for :
/// 1.Generating the random vehicles 
/// 2.The Score, Time Game ...
/// 3.Generating the Events
/// </summary>

public class GameMaster : MonoBehaviour {
	
	//HUDs variables 
	public int score;
	public float gameTime;
	public int satisfyBar;
	private const float GAME_TIME = 150;			//should equal to 5 minutes
	private const int MAX_STREET_VEHICLES_NUMBER = 4;
	
	
	private List<Path> Paths;
	private List<Street> Streets;
	private List<float> _timeSlots;
	private List<List <int>> _multiPositions;
	private int vehiclesNumber;
	// These constants for the random generation of vehicles
	//private const int VEHICLES_LEAST_NUMBER = 500;
	//private const int VEHICLES_MOST_NUMBER = 600;
	
	public GameObject vehiclePrefab;				//this object should be initialized in unity with the VehiclePrefab
	public GameObject ambulancePrefab;				//this object should be initialized in unity with the AmbulancePrefab
	
	public Queue existedVehicles;
	
	private SatisfyBar satisfyBarScript;
	public bool gameOver;
	private int initedCarsNumber;
	private bool cancelInvokeFirst15Vehicles;
	private bool canInstatiateTheRest;
	private int currentCarsNumber;
	private int secondsCounterFor30;
	private int secondsCounterForAnger;
	
	
	private int tempCounter;
	
	public Texture2D veryHappyIcon;
	public Texture2D happyIcon;
	public Texture2D notHappyIcon;
	public Texture2D sadIcon;
	public Texture2D verySadIcon;
	
	void Awake(){
		satisfyBarScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SatisfyBar>();
		existedVehicles = new Queue();
		//initializing the huds 
		tempCounter = 0;
		secondsCounterFor30 = 0;		//this variable to decrement the satify
		secondsCounterForAnger = 0;
		canInstatiateTheRest = false;
		cancelInvokeFirst15Vehicles = false;
		gameTime = GAME_TIME;
		initedCarsNumber = 0;
	//	gameOver = false;
		score = 0;
		satisfyBar = 2;
		satisfyBarScript.AddjustSatisfaction(2);
		
		
	}
	
	// Use this for initialization
	void Start () {
		//initializing the vehicles needs
		//_generationPoints = new List<Vector3>();
		Streets = GameObject.FindGameObjectWithTag("master").GetComponent<StreetsGenerator>().getStreets();
		Paths = GameObject.FindGameObjectWithTag("master").GetComponent<StreetsGenerator>().getPaths();
		
		//InitAllGenerationPoints();
		_timeSlots = new List<float>();
		//_multiPositions = new List<List<int>>();
		SetAmbulanceRandomTime();
		InvokeRepeating("generateFirst15Cars", Time.deltaTime, 0.1f);
		InvokeRepeating("CountTimeDown", 1.0f, 1.0f);
	}
	
	private void generateFirst15Cars(){
		if(cancelInvokeFirst15Vehicles){
			canInstatiateTheRest =true;
			CancelInvoke("generateFirst15Cars");
			
		}
		int pos = Random.Range(0, Paths.Count);
		GenerateRandomVehicle(pos);
	}
	
	private void SetAmbulanceRandomTime(){
		_timeSlots.Add(125);
		_timeSlots.Add(115);
		/*
		int timesNumber = 2;
		float timeRatio = 0.0f;
		for (int i = 0 ; i<timesNumber; i++){
			timeRatio = Random.Range(135, 145);
			if(!_timeSlots.Contains(timeRatio)){
				_timeSlots.Add(timeRatio);
				Debug.Log("time ... " + timeRatio);
				//_multiPositions.Add(GetOneTimePositions());
			}	
		}	
		*/	
	}
	
	private bool InsideTimeSlotsList(){
		bool found = false;
		int i=0;
		Debug.Log("timeSlots.count "+ _timeSlots.Count);
		while(!found && i < _timeSlots.Count){
			if(_timeSlots [i] == gameTime)
				found = true;
			i++;
		}
		return found;
	}
	
	//This method is called in the Start() it counts down the game time each second and generates the vehicles in the random time calculated at the first of the game
	void CountTimeDown ()
	{
		//Debug.Log("Game Time= "+gameTime);
		if(--gameTime == 0)
		{
			CancelInvoke("CountTimeDown");
			//gameOver = true;
			
		}
		if( GameObject.FindGameObjectsWithTag("vehicle").Length < 15){
		//	secondsCounterForAnger = 0;
			secondsCounterFor30 ++;
			if(secondsCounterFor30 == 30){	
				if(satisfyBar >=0){
					satisfyBar --;
					secondsCounterFor30 = 0;
					satisfyBarScript.AddjustSatisfaction(-1);
				}
			}
		}
		else{
			secondsCounterFor30 = 0;
		//	secondsCounterForAnger ++;
		//	if(secondsCounterForAnger == 8){	
		//		satisfyBar += 2;
		//		satisfyBarScript.AddjustSatisfaction(2);
		//		secondsCounterForAnger = 0;
				
			//}
		}
		
		//here
		
		//int index = InsideTimeSlotsList();
		if( canInstatiateTheRest){
			
			if(!CheckAllStreetsFullness()){
				//this piece of code is for the ambulance
				int pos1 = Random.Range(0, Paths.Count);
				while(Paths[pos1].PathStreets[0].VehiclesNumber >= MAX_STREET_VEHICLES_NUMBER){
						pos1 = Random.Range(0, Paths.Count);
				}
				
				if(InsideTimeSlotsList()){
					Debug.Log("should be ambulance");
					GenerateAmbulance(pos1);
				}
				else {
					GenerateRandomVehicle(pos1);
				}
					
				int pos2 = Random.Range(0, Paths.Count);
				if(!CheckAllStreetsFullness()){
					while(Paths[pos2].PathStreets[0].VehiclesNumber >= MAX_STREET_VEHICLES_NUMBER){
						pos2 = Random.Range(0, Paths.Count);
					}
					if((Paths[pos1].GenerationPointPosition != Paths[pos2].GenerationPointPosition))
						GenerateRandomVehicle(pos2);
				}
				
				int pos3 = Random.Range(0, Paths.Count);
				if(!CheckAllStreetsFullness()){
					while(Paths[pos3].PathStreets[0].VehiclesNumber >= MAX_STREET_VEHICLES_NUMBER){
						pos3 = Random.Range(0, Paths.Count);
					}
					if((Paths[pos1].GenerationPointPosition != Paths[pos3].GenerationPointPosition)
						&&(Paths[pos2].GenerationPointPosition != Paths[pos3].GenerationPointPosition)){
	
						GenerateRandomVehicle(pos3);
					}
				}
			}
		}
	}
	
	
	private bool CanInstantiatThisTime(){
		int num = Random.Range(0,2);
		//Debug.LogWarning(num);
		if(num == 0)
			return false;
		else
			return true;
	}
	
	//here we give the vehicle all of its properities
	private void GenerateRandomVehicle(int pos){	
		
		if(!CheckAllStreetsFullness()){
			if(!cancelInvokeFirst15Vehicles){
				while(Paths[pos].PathStreets[0].VehiclesNumber >= MAX_STREET_VEHICLES_NUMBER){
					pos = Random.Range(0, Paths.Count);
				}
			}
			if(vehiclePrefab != null){
				tempCounter ++;
				//*****************************optimization
				GameObject vehicle;
				if(existedVehicles.Count == 0){
					vehicle = Instantiate(vehiclePrefab, Paths[pos].GenerationPointPosition ,Quaternion.identity) as GameObject;
					Paths[pos].PathStreets[0].VehiclesNumber ++;
					//vehicle.name = "Street # "+Paths[pos].PathStreets[0].ID + " # " + Paths[pos].PathStreets[0].VehiclesNumber;
					vehicle.name = "Street # "+Paths[pos].PathStreets[0].ID + " Car number " + tempCounter;
					
					//	public Vehicle(VehicleType type,float speed,float size, Direction curDir, Street curStreet, Street nextStreet, Path path)
					vehicle.GetComponent<VehicleController>().myVehicle = new Vehicle(	VehicleType.Normal, 
																						23.0f, 
																						getVehicleLargeSize(vehicle), 
																						Paths[pos].PathStreets[0].StreetLight.Type, 
																						Paths[pos].PathStreets[0], 
																						Paths[pos].PathStreets[1], 
																						0,
																						Paths[pos]);
				}
				else{
					vehicle = existedVehicles.Dequeue() as GameObject;
				//	Debug.Log("dequeuing ..." + vehicle.name);
					//vehicle = vehiclePrefab;					
					vehicle.transform.position = Paths[pos].GenerationPointPosition;
					vehicle.transform.rotation = Quaternion.identity;
					vehicle.GetComponent<VehicleController>().initInstancesAtFirst();
					vehicle.SetActive(true);
					Paths[pos].PathStreets[0].VehiclesNumber ++;
					//vehicle.name = "Street # "+Paths[pos].PathStreets[0].ID + " # " + Paths[pos].PathStreets[0].VehiclesNumber;
					vehicle.name = "Street # "+Paths[pos].PathStreets[0].ID + " Car number " + tempCounter;
					
					//	public Vehicle(VehicleType type,float speed,float size, Direction curDir, Street curStreet, Street nextStreet, Path path)
					vehicle.GetComponent<VehicleController>().myVehicle = new Vehicle(	VehicleType.Normal, 
																						23.0f, 
																						getVehicleLargeSize(vehicle), 
																						Paths[pos].PathStreets[0].StreetLight.Type, 
																						Paths[pos].PathStreets[0], 
																						Paths[pos].PathStreets[1], 
																						0,
																						Paths[pos]);
					vehicle.GetComponent<VehicleController>().InitStreetAndVehicleAttributes();
					
				}
				
			}
			initedCarsNumber ++;
			if(initedCarsNumber == 15){
				cancelInvokeFirst15Vehicles = true;
				//satisfyBar += 2;
				initedCarsNumber =0;
			}
		}
	}
	
	private void GenerateAmbulance(int pos){	
		
		if(!CheckAllStreetsFullness()){
			if(!cancelInvokeFirst15Vehicles){
				while(Paths[pos].PathStreets[0].VehiclesNumber >= MAX_STREET_VEHICLES_NUMBER){
					pos = Random.Range(0, Paths.Count);
				}
			}
			if(ambulancePrefab != null){
				tempCounter ++;
				//*****************************optimization
				GameObject vehicle;
			//	if(existedVehicles.Count == 0){
					vehicle = Instantiate(ambulancePrefab, Paths[pos].GenerationPointPosition ,Quaternion.identity) as GameObject;
					Paths[pos].PathStreets[0].VehiclesNumber ++;
					//vehicle.name = "Street # "+Paths[pos].PathStreets[0].ID + " # " + Paths[pos].PathStreets[0].VehiclesNumber;
					vehicle.name = "Street # "+Paths[pos].PathStreets[0].ID + " Car number " + tempCounter;
					
					//	public Vehicle(VehicleType type,float speed,float size, Direction curDir, Street curStreet, Street nextStreet, Path path)
					vehicle.GetComponent<VehicleController>().myVehicle = new Vehicle(	VehicleType.Ambulance, 
																						25.0f, 
																						getVehicleLargeSize(vehicle), 
																						Paths[pos].PathStreets[0].StreetLight.Type, 
																						Paths[pos].PathStreets[0], 
																						Paths[pos].PathStreets[1], 
																						0,
																						Paths[pos]);
				
			}
			initedCarsNumber ++;
			if(initedCarsNumber == 15){
				cancelInvokeFirst15Vehicles = true;
				//satisfyBar += 2;
				initedCarsNumber =0;
			}
		}
	}
	
	private bool CheckAllStreetsFullness(){
		int counter = 0;
		for(int i= 0 ; i < Paths.Count ; i++){
			if(Paths[i].PathStreets[0].VehiclesNumber >= MAX_STREET_VEHICLES_NUMBER)
				counter ++ ;
		}
		if(counter == Paths.Count)
			return true;
		else
			return false;
	}
	
	private float getVehicleLargeSize(GameObject v){
		
		if(v.transform.localScale.x > v.transform.localScale.z)
			return v.transform.localScale.x;
		else
			return v.transform.localScale.z;
	}
	
	
	
	
	
	// Update is called once per frame
	void Update () {

		
		
	}
	
	void OnGUI(){
		if(gameTime == 128 || gameTime == 118){
			if(!CheckAllStreetsFullness())
				GUI.Label(new Rect(Screen.width/2 - 20 , Screen.height/2 , 100, 100), "Ambulance Coming" );
		}
		
		GUI.Label( new Rect(10, 10, 100, 35), "Time: "+ gameTime);
		GUI.Label(new Rect(10, 30, 100, 25), "Score : "+score);
		GUI.Label(new Rect(10, 50, 100, 25), "Satisfy Bar: ");
		//GUI.Box(new Rect(10, 100, 300, 300), veryHappyIcon);
		
		if(satisfyBar <= 2){
			GUI.Label(new Rect(10, 100, 80, 80), veryHappyIcon);
		}
		else if(satisfyBar <= 4){
			GUI.Label(new Rect(10, 100, 80, 80), happyIcon);
		}
		
		else if(satisfyBar <= 6){
			GUI.Label(new Rect(10, 100, 80, 80), notHappyIcon);
		}
		
		else if(satisfyBar <= 8){
			GUI.Label(new Rect(10, 100, 80, 80), sadIcon);
		}
		
		else if(satisfyBar <= 10){
			GUI.Label(new Rect(10, 100, 80, 80), verySadIcon);
		}
		
		if(score >= 200){
			GUI.Box(new Rect(Screen.width/4, Screen.height/4,  Screen.width/2 , Screen.height/2 ) , " "  );
			GUI.Label(new Rect(Screen.width/2 - 20 , Screen.height/2 , 100, 25), "Congratulations! ");
			string temp = score.ToString();
			GUI.Label(new Rect(Screen.width/2 - 20 , Screen.height/2+30-12 , 100, 25), "Score : "+ temp);
			
			
			GameObject [] gos = GameObject.FindGameObjectsWithTag("vehicle");
			for(int i = 0; i < gos.Length; i++){
				gos[i].SetActive(false);
			}
			
			CancelInvoke("CountTimeDown");
			if(GUI.Button(new Rect(Screen.width/2 - 40 , Screen.height/2+70-12 , 100, 25), "Replay ")){
				Application.LoadLevel("Level 1");
			}
		}
		else if(satisfyBar >= 10){
		
			GUI.Box(new Rect(Screen.width/4, Screen.height/4,  Screen.width/2 , Screen.height/2 ) , " "  );
			GUI.Label(new Rect(Screen.width/2 - 20 , Screen.height/2 , 100, 25), "Traffic Gam !!!");
			string temp = score.ToString();
			GUI.Label(new Rect(Screen.width/2 - 20 , Screen.height/2+30-12 , 100, 25), "Score : "+ temp);
			
			GameObject [] gos = GameObject.FindGameObjectsWithTag("vehicle");
			for(int i = 0; i < gos.Length; i++){
				gos[i].SetActive(false);
			}
			
			CancelInvoke("CountTimeDown");
			if(GUI.Button(new Rect(Screen.width/2 - 40 , Screen.height/2+70-12 , 100, 25), "Replay ")){
				Application.LoadLevel("Level 1");
			}
		
			
		}
		
		
		else if(gameOver || gameTime == 0){
		
			GUI.Box(new Rect(Screen.width/4, Screen.height/4,  Screen.width/2 , Screen.height/2 ) , " "  );
			GUI.Label(new Rect(Screen.width/2 - 20 , Screen.height/2 - 12, 100, 25), "LOSER !!");
			string temp = score.ToString();
			GUI.Label(new Rect(Screen.width/2 - 20 , Screen.height/2+30-12 , 100, 25), "Score : "+ temp);
			
			GameObject [] gos = GameObject.FindGameObjectsWithTag("vehicle");
			for(int i = 0; i < gos.Length; i++){
				gos[i].SetActive(false);
			}
			
			CancelInvoke("CountTimeDown");
			if(GUI.Button(new Rect(Screen.width/2 - 40 , Screen.height/2+70-12 , 100, 25), "Replay ")){
				Application.LoadLevel("Level 1");
			}
		//	gameObject.SetActive(false);
			
			//Application.Quit();
			
		}
	}
	
	
	
}
