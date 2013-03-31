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
	private const int VEHICLES_LEAST_NUMBER = 500;
	private const int VEHICLES_MOST_NUMBER = 600;
	
	public GameObject vehiclePrefab;				//this object should be initialized in unity with the VehiclePrefab
	public Queue existedVehicles;
	
	public bool gameOver;
	private int initedCarsNumber;
	private bool cancelInvokeFirst15Vehicles;
	private bool canInstatiateTheRest;
	private int currentCarsNumber;
	private int secondsCounterFor30;
	private int secondsCounterForAnger;
	
	
	private int tempCounter;
	
	void Awake(){
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
		
		
	}
	
	// Use this for initialization
	void Start () {
		//initializing the vehicles needs
		//_generationPoints = new List<Vector3>();
		
		Streets = GameObject.FindGameObjectWithTag("master").GetComponent<StreetsGenerator>().getStreets();
		Paths = GameObject.FindGameObjectWithTag("master").GetComponent<StreetsGenerator>().getPaths();
		
		//InitAllGenerationPoints();
		//_timeSlots = new List<float>();
		//_multiPositions = new List<List<int>>();
//		CalculateRandomTimeSlots();
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
	
	
		
	
	//This method calculates random time intervals that the vehicles should be generated in
/*	private void CalculateRandomTimeSlots(){
		vehiclesNumber = Random.Range(VEHICLES_LEAST_NUMBER, VEHICLES_MOST_NUMBER);
		float timeRatio = 0.0f;
		for (int i = 0 ; i<vehiclesNumber; i++){
			timeRatio = Random.Range(0.0f, gameTime);
			if(!_timeSlots.Contains(timeRatio)){
				_timeSlots.Add(timeRatio);
				Debug.Log("time ... " + timeRatio);
				//_multiPositions.Add(GetOneTimePositions());
			}
				
		}
		
		
	}
*/
	
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
			secondsCounterForAnger = 0;
			secondsCounterFor30 ++;
			if(secondsCounterFor30 == 30){	
				if(satisfyBar >=0){
					satisfyBar --;
					secondsCounterFor30 = 0;
				}
			}
		}
		else{
			secondsCounterFor30 = 0;
			secondsCounterForAnger ++;
			if(secondsCounterForAnger == 5){	
				satisfyBar += 2;
				secondsCounterForAnger = 0;
				
			}
		}
		
		//here
		
		//int index = InsideTimeSlotsList();
		if( canInstatiateTheRest){
			
			if(!CheckAllStreetsFullness()){
				int pos1 = Random.Range(0, Paths.Count);
				
				while(Paths[pos1].PathStreets[0].VehiclesNumber >= MAX_STREET_VEHICLES_NUMBER){
					pos1 = Random.Range(0, Paths.Count);
				}
				
				GenerateRandomVehicle(pos1);
				
				int pos2 = Random.Range(0, Paths.Count);
				if(!CheckAllStreetsFullness()){
					while(Paths[pos2].PathStreets[0].VehiclesNumber >= MAX_STREET_VEHICLES_NUMBER){
						pos2 = Random.Range(0, Paths.Count);
					}
					if((Paths[pos1].GenerationPointPosition != Paths[pos2].GenerationPointPosition))
						GenerateRandomVehicle(pos2);
				}
				
				//Debug.Log("pos1 , pos2  ... " + pos1 +" , "+ pos2);
			}
			
		}
	}
/*	
	private List<int> GetOneTimePositions(){
		int pos = Random.Range(0, Paths.Count);
		List<int> positions = new List<int>();
			
		int i = 0;
		//Debug.Log("the pos is ---->  "+ pos);
			
		while (positions.Count< Paths.Count/2){
			if(!positions.Contains(pos+i)){
				if(!hasSameGeneration(positions, pos+i)){
					positions.Add(pos+i);
					Debug.Log("Adding element ... " + (pos+i));
				}
			}
			if(pos+i == Paths.Count-1){
				i -= pos+i;
				
			}
			else
				i++;
		}
		Debug.Log("positions number ... " + positions.Count);
		return positions;
	}
	*/
	/*
	private bool hasSameGeneration(List<int> positions, int pos){
		if(positions.Count == 0){
			return false;	
		}
		for(int i=0; i<positions.Count ; i++){
			Debug.LogWarning(positions[i] + " versus not againest " + pos);
			if(Paths[positions[i]].GenerationPointPosition == Paths[pos].GenerationPointPosition){
				Debug.Log("its true");
				return true;
			}
		}
		return false;
	}
	*/	
	
	
	private bool CanInstantiatThisTime(){
		int num = Random.Range(0,2);
		//Debug.LogWarning(num);
		if(num == 0)
			return false;
		else
			return true;
	}
	
/*	
	private int InsideTimeSlotsList(){
		bool found = false;
		int i=0;
		while(!found && i < _timeSlots.Count){
			if(gameTime >= _timeSlots [i])
				found = true;
			else
				i++;
		}
		if(found)
			return i;
		else
			return -1;
	}
	*/
	//here we give the vehicle all of its properities
	private void GenerateRandomVehicle(int pos){	
		//int pos = Random.Range(0, _generationPoints.Count);
		
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
					Debug.Log("dequeuing ..." + vehicle.name);
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
		GUI.Label( new Rect(10, 10, 100, 35), "Time: "+ gameTime);
		GUI.Label(new Rect(10, 30, 100, 25), "Score : "+score);
		GUI.Label(new Rect(10, 50, 100, 25), "Satisfy Bar "+satisfyBar);
		
		if(score >= 200){
			GUI.Box(new Rect(Screen.width/4, Screen.height/4,  Screen.width/2 , Screen.height/2 ) , " "  );
			GUI.Label(new Rect(Screen.width/2 - 15 , Screen.height/2 - 12, 100, 25), "Congratulations! ");
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
