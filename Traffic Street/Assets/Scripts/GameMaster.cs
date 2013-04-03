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
	public bool gameOver;
	
	private const float GAME_TIME = 150;			//should equal to 5 minutes
	private const int WARNING_BEFORE_EVENT_SECONDS = 3;
	
	private const int MIN_TIME_BETWEEN_EVENTS =6;
	
	public GameObject vehiclePrefab;				//this object should be initialized in unity with the VehiclePrefab
	public GameObject ambulancePrefab;				//this object should be initialized in unity with the AmbulancePrefab
	public GameObject busPrefab;					//this object should be initialized in unity with the BusPrefab
	
	private List<Path> Paths;
	private List<Street> Streets;
	
	
	
	public Queue existedVehicles;
	
	private SatisfyBar satisfyBarScript;
	
	private int initedCarsNumber;
	private bool cancelInvokeFirst15Vehicles;
	private bool canInstatiateTheRest;
	private int currentCarsNumber;
	private int secondsCounterFor30;
	private int secondsCounterForAnger;
	
	
	public static int vehicilesCounter;
	
	//mayor faces
	public Texture2D veryHappyIcon;
	public Texture2D happyIcon;
	public Texture2D notHappyIcon;
	public Texture2D sadIcon;
	public Texture2D verySadIcon;
	
	void Awake(){
		
		initVariables();
	}
	
	private void initVariables(){
		satisfyBarScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SatisfyBar>();		
	
		existedVehicles = new Queue();
		vehicilesCounter = 0;
		secondsCounterFor30 = 0;		//this variable to decrement the satify
		secondsCounterForAnger = 0;
		canInstatiateTheRest = false;
		cancelInvokeFirst15Vehicles = false;
		gameTime = GAME_TIME;
		initedCarsNumber = 0;
		score = 0;
		satisfyBar = 2;
		satisfyBarScript.AddjustSatisfaction(2);
		
	}
	
	// Use this for initialization
	void Start () {
		
		initObjects();
		
		SetEventsRandomTime();
		
		
		InvokeRepeating("generateFirst15Cars", Time.deltaTime, 0.1f);
		InvokeRepeating("CountTimeDown", 1.0f, 1.0f);
	}
	
	private void initObjects(){
		Streets = GameObject.FindGameObjectWithTag("master").GetComponent<StreetsGenerator>().getStreets();
		Paths = GameObject.FindGameObjectWithTag("master").GetComponent<StreetsGenerator>().getPaths();
		
		Ambulance.InitInstances();
		Bus.InitInstances();
	}
	
	private void generateFirst15Cars(){
		if(cancelInvokeFirst15Vehicles){
			canInstatiateTheRest =true;
			CancelInvoke("generateFirst15Cars");
			
		}
		int pos = Random.Range(0, Paths.Count);
		NormalVehicle.GenerateNormalVehicle(pos, vehiclePrefab, Paths, cancelInvokeFirst15Vehicles, existedVehicles);
		vehicilesCounter++;
		AdjustEach15Vehicle();
	}
	
	private void SetEventsRandomTime(){
		Ambulance.SetAmbulanceRandomTime(MIN_TIME_BETWEEN_EVENTS);
		Bus.SetBusRandomTime(MIN_TIME_BETWEEN_EVENTS);
	}
	

	
	//This method is called in the Start() it counts down the game time each second and generates the vehicles in the random time calculated at the first of the game
	void CountTimeDown ()
	{
		if(--gameTime == 0)
		{
			CancelInvoke("CountTimeDown");
			
		}
		if( GameObject.FindGameObjectsWithTag("vehicle").Length < 15){
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
		
		}
		
		
		if( canInstatiateTheRest){
			
			InstantiatVehiclesArroundTime();
		}
	}
	
	
	
	private void InstantiatVehiclesArroundTime(){
		if(!CheckAllStreetsFullness()){
				//this piece of code is for the events
			int pos1 = Random.Range(0, Paths.Count);
			while(Paths[pos1].PathStreets[0].VehiclesNumber >= Paths[pos1].PathStreets[0].StreetCapacity){
					pos1 = Random.Range(0, Paths.Count);
			}
			
			if(Ambulance.InsideAmbulanceTimeSlotsList(gameTime)){
				Debug.Log("should be ambulance");
				Ambulance.GenerateAmbulance(pos1, ambulancePrefab, Paths);
				vehicilesCounter ++;
				AdjustEach15Vehicle();
			}
			else if(Bus.InsideBusTimeSlotsList(gameTime)) {
				Debug.Log("should be a bus");
				Bus.GenerateBus(pos1, busPrefab, Paths);
				vehicilesCounter ++;
				AdjustEach15Vehicle();
			}
			else {
				NormalVehicle.GenerateNormalVehicle(pos1, vehiclePrefab, Paths, cancelInvokeFirst15Vehicles, existedVehicles);
				vehicilesCounter++;
				AdjustEach15Vehicle();
			}
				
			int pos2 = Random.Range(0, Paths.Count);
			if(!CheckAllStreetsFullness()){
				while(Paths[pos2].PathStreets[0].VehiclesNumber >= Paths[pos2].PathStreets[0].StreetCapacity){
					pos2 = Random.Range(0, Paths.Count);
				}
				if((Paths[pos1].GenerationPointPosition != Paths[pos2].GenerationPointPosition)){
					NormalVehicle.GenerateNormalVehicle(pos2, vehiclePrefab, Paths, cancelInvokeFirst15Vehicles, existedVehicles);
					vehicilesCounter++;
					AdjustEach15Vehicle();
				}
			}
			
			int pos3 = Random.Range(0, Paths.Count);
			if(!CheckAllStreetsFullness()){
				while(Paths[pos3].PathStreets[0].VehiclesNumber >= Paths[pos3].PathStreets[0].StreetCapacity){
					pos3 = Random.Range(0, Paths.Count);
				}
				if((Paths[pos1].GenerationPointPosition != Paths[pos3].GenerationPointPosition)
					&&(Paths[pos2].GenerationPointPosition != Paths[pos3].GenerationPointPosition)){
							
					NormalVehicle.GenerateNormalVehicle(pos3, vehiclePrefab, Paths, cancelInvokeFirst15Vehicles, existedVehicles);
					vehicilesCounter++;
					AdjustEach15Vehicle();
				}
			}
		}
	}
	
	private void AdjustEach15Vehicle(){
		initedCarsNumber ++;
		if(initedCarsNumber == 15){
			cancelInvokeFirst15Vehicles = true;
			initedCarsNumber =0;
		}
	}
	
	
	private bool CheckAllStreetsFullness(){
		int counter = 0;
		for(int i= 0 ; i < Paths.Count ; i++){
			if(Paths[i].PathStreets[0].VehiclesNumber >= Paths[i].PathStreets[0].StreetCapacity)
				counter ++ ;
		}
		if(counter == Paths.Count)
			return true;
		else
			return false;
	}
	
	
	
	
	
	
	
	// Update is called once per frame
	void Update () {

		
		
	}
	
	void OnGUI(){
		if(Ambulance.ambulanceTimeSlots.Contains((int)(gameTime - WARNING_BEFORE_EVENT_SECONDS)) ){
			if(!CheckAllStreetsFullness())
				GUI.Label(new Rect(Screen.width/2 - 20 , Screen.height/2 , 100, 100), "Ambulance Coming" );
		}
		
		if(Bus.busTimeSlots.Contains((int)(gameTime - WARNING_BEFORE_EVENT_SECONDS)) ){
			if(!CheckAllStreetsFullness())
				GUI.Label(new Rect(Screen.width/2 - 20 , Screen.height/2 , 100, 100), "a Bus is Coming" );
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
		
		/*
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
		*/
		
		
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
