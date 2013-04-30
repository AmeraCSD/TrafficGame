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
	public static int score;
	public float gameTime;
	public float satisfyBar;
	public bool gameOver;
	public int trullyPassedEventsNum;
	
	private const float GAME_TIME = 150;			//should equal to 5 minutes
	private const int WARNING_BEFORE_EVENT_SECONDS = 3;
	
	private const int MIN_TIME_BETWEEN_EVENTS = 6;
	
	public List<Texture2D> vehiclesTextures;
	
	
	public GameObject vehiclePrefab;				//this object should be initialized in unity with the VehiclePrefab
	public GameObject ambulancePrefab;				//this object should be initialized in unity with the AmbulancePrefab
	public GameObject busPrefab;					//this object should be initialized in unity with the BusPrefab
	public GameObject caravanPrefab;
	public GameObject serviceCarPrefab;
	public GameObject thiefPrefab;

	private List<GamePath> Paths;
	private List<Street> Streets;
	private List<Vector3> Intersections;
	
	public GameObject intersectionPrefab;
	
	public static List<float> eventsWarningTimes;
	public static List<string> eventsWarningNames;
	
	public Queue existedVehicles;
	
	private SatisfyBar satisfyBarScript;
	
	private int initedCarsNumber;
	private bool cancelInvokeFirst15Vehicles;
	private bool canInstatiateTheRest;
	private int currentCarsNumber;
	private int secondsCounterFor30;
	private int secondsCounterForAnger;
	private bool instantiatThisTime;
	
	public static int vehicilesCounter;
	
	public bool showBox;
	
	
	public Texture2D accidentImage;
	
	/////////////////////////////////// GUI Variables ...
	
	
	public GameObject eventWarningLabelGo;
	public GameObject closeButtonGo;
	public GameObject gameTimeLabelGo;
	public GameObject gameTimeVarLabelGo;
	public GameObject scoreLabelGo;
	public GameObject scoreVarLabelGo;
	public GameObject satisfyBarLabelGo;
	
	public UILabel eventWarningLabel;
	public UIButton closeButton;
	private UILabel gameTimeLabel;
	private UILabel gameTimeVarLabel;
	private UILabel scoreLabel;
	public UILabel scoreVarLabel;
	private UILabel satisfyBarLabel;
	
	//mayor faces
	public GameObject mayorFacesSpriteGo;
	private UISlicedSprite mayorFacesSprite;
	
	
	public GameObject totalScoreLabelGo;
	public GameObject totalScoreVarLabelGo;
	
	private UILabel totalScoreLabel;
	private UILabel totalScoreVarLabel;
	
	public GameObject replayButtonGo;
	public UIButton replayButton;
	
	public GameObject eventsSpriteGo;
	private UISlicedSprite eventsSprite;
	
	public GameObject endGameSpriteGo;
	private UISlicedSprite endGameSprite;




	public GameObject winMenu;
	public GameObject loseMenu;
	
	public bool vibrationMade = false;
	
	
	private void InitGUIVariables(){
		eventWarningLabel = eventWarningLabelGo.GetComponent<UILabel>();
		closeButton = closeButtonGo.GetComponent<UIButton>();
		closeButtonGo.SetActive(false);
		
		gameTimeLabel = gameTimeLabelGo.GetComponent<UILabel>();
		gameTimeVarLabel = gameTimeVarLabelGo.GetComponent<UILabel>();
		scoreLabel = scoreLabelGo.GetComponent<UILabel>();
		scoreVarLabel = scoreVarLabelGo.GetComponent<UILabel>();
		satisfyBarLabel = satisfyBarLabelGo.GetComponent<UILabel>();
		
		mayorFacesSprite = mayorFacesSpriteGo.GetComponent<UISlicedSprite>();
		
		totalScoreLabel = totalScoreLabelGo.GetComponent<UILabel>();
		totalScoreVarLabel = totalScoreVarLabelGo.GetComponent<UILabel>();
		totalScoreVarLabelGo.SetActive(false);
		
		replayButton = replayButtonGo.GetComponent<UIButton>();
		replayButtonGo.SetActive(false);
		
		eventsSprite = eventsSpriteGo.GetComponent<UISlicedSprite>();
		eventsSpriteGo.SetActive(false);
		
		//endGameSprite = endGameSpriteGo.GetComponent<UISlicedSprite>();
		//endGameSpriteGo.SetActive(false);
	}
	
	
	int index =0 ;
	void Awake(){
		
		initVariables();
		InitGUIVariables();
	}
	
	
	
	private void initVariables(){
		satisfyBarScript = GameObject.FindGameObjectWithTag("satisfyBar").GetComponent<SatisfyBar>();		
	
		existedVehicles = new Queue();
		trullyPassedEventsNum = 0;
		showBox = false;
		instantiatThisTime = false;
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
		InstantiateIntersections();
		
		InvokeRepeating("generateFirst15Cars", Time.deltaTime, 0.3f);
		InvokeRepeating("CountTimeDown", 1.0f, 1.0f);
	}
	
	
	
	private void initObjects(){
		Streets = GameObject.FindGameObjectWithTag("master").GetComponent<StreetsGenerator>().getStreets();
		Paths = GameObject.FindGameObjectWithTag("master").GetComponent<StreetsGenerator>().getPaths();
		Intersections = GameObject.FindGameObjectWithTag("master").GetComponent<StreetsGenerator>().getIntersections();
		
		eventsWarningTimes = new List<float>();
		eventsWarningNames = new List<string>();
		
		Bus.InitInstances();
		Ambulance.InitInstances();
		Caravan.InitInstances();
		ServiceCar.InitInstances();
		//Thief.InitInstances();
		
	}
	
	private void InstantiateIntersections(){
		
		for(int i = 0; i<Intersections.Count ; i++){
			Instantiate(intersectionPrefab, Intersections[i] ,Quaternion.identity);
		}
		
	}
	
	private void generateFirst15Cars(){
		if(cancelInvokeFirst15Vehicles){
			canInstatiateTheRest =true;
			CancelInvoke("generateFirst15Cars");
			
		}
		int pos = Random.Range(0, Paths.Count);
		Texture2D tx = GetRandomTexture(0, 4);
		NormalVehicle.GenerateNormalVehicle(pos, vehiclePrefab, tx, Paths, cancelInvokeFirst15Vehicles, existedVehicles);
		vehicilesCounter++;
		AdjustEach15Vehicle();
	}
	
	private void SetEventsRandomTime(){
		
	//	Bus.SetBusRandomTime(MIN_TIME_BETWEEN_EVENTS);
	//	Ambulance.SetAmbulanceRandomTime(MIN_TIME_BETWEEN_EVENTS);
	//	Caravan.SetCaravanRandomTime(MIN_TIME_BETWEEN_EVENTS);
		ServiceCar.SetServiceCarRandomTime(MIN_TIME_BETWEEN_EVENTS);
		//Thief.SetThiefRandomTime(MIN_TIME_BETWEEN_EVENTS);
	}
	

	
	//This method is called in the Start() it counts down the game time each second and generates the vehicles in the random time calculated at the first of the game
	void CountTimeDown ()
	{
		if(--gameTime == 0)
		{
			CancelInvoke("CountTimeDown");
			
		}
	//	gameTimeVarLabel.text = gameTime+"";
		//gameTimeLabel = gameTimeLabelGo.GetComponent<UILabel>();
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
		instantiatThisTime = !instantiatThisTime;
		if(!CheckAllStreetsFullness()){
				//this piece of code is for the events
			int pos1 = Random.Range(0, Paths.Count);
			while(Paths[pos1].PathStreets[0].VehiclesNumber >= Paths[pos1].PathStreets[0].StreetCapacity){
					pos1 = Random.Range(0, Paths.Count);
			}
			/*
			if(Bus.InsideBusTimeSlotsList(gameTime)){
			
				Debug.Log("should be a bus");
				Bus.GenerateBus(pos1, busPrefab, Paths);
				vehicilesCounter ++;
				AdjustEach15Vehicle();
			}
			
			
			else if(Ambulance.InsideAmbulanceTimeSlotsList(gameTime)) {
				Debug.Log("should be ambulance");
				Ambulance.GenerateAmbulance(pos1, ambulancePrefab, Paths);
				vehicilesCounter ++;
				AdjustEach15Vehicle();
			}
			/*
			else if(Caravan.InsideCaravanTimeSlotsList(gameTime)) {
				Debug.Log("should be caravan");
				Caravan.GenerateCaravan(pos1, caravanPrefab, Paths);
				vehicilesCounter ++;
				AdjustEach15Vehicle();
			}
			
			else*/ if(ServiceCar.InsideServiceCarTimeSlotsList(gameTime)) {
				Debug.Log("should be a service car");
				Texture2D tx = GetRandomTexture(4, 7);
				ServiceCar.GenerateServiceCar(pos1, serviceCarPrefab, tx, Paths);
				vehicilesCounter ++;
				AdjustEach15Vehicle();
			}
			/*
			else if(Thief.InsideThiefTimeSlotsList(gameTime)) {
				if(CheckAllStreetsEmptiness()){
					Debug.Log("should be a thief");
					Thief.GenerateThief(pos1, thiefPrefab, Paths);
					vehicilesCounter ++;
					AdjustEach15Vehicle();
				}
			}
			*/
			
			else {
				Texture2D tx1 = GetRandomTexture(0, 4);
				NormalVehicle.GenerateNormalVehicle(pos1, vehiclePrefab, tx1, Paths, cancelInvokeFirst15Vehicles, existedVehicles);
				vehicilesCounter++;
				AdjustEach15Vehicle();
			}
			
			
			//if(instantiatThisTime){
				int pos2 = Random.Range(0, Paths.Count);
				if(!CheckAllStreetsFullness()){
					while(Paths[pos2].PathStreets[0].VehiclesNumber >= Paths[pos2].PathStreets[0].StreetCapacity){
						pos2 = Random.Range(0, Paths.Count);
					}
					if((Paths[pos1].GenerationPointPosition != Paths[pos2].GenerationPointPosition)){
						Texture2D tx = GetRandomTexture(0, 4);
						NormalVehicle.GenerateNormalVehicle(pos2, vehiclePrefab, tx, Paths, cancelInvokeFirst15Vehicles, existedVehicles);
						vehicilesCounter++;
						AdjustEach15Vehicle();
					}
				}
			//}
			
			
			
			/*
			int pos3 = Random.Range(0, Paths.Count);
			if(!CheckAllStreetsFullness()){
				while(Paths[pos3].PathStreets[0].VehiclesNumber >= Paths[pos3].PathStreets[0].StreetCapacity){
					pos3 = Random.Range(0, Paths.Count);
				}
				if((Paths[pos1].GenerationPointPosition != Paths[pos3].GenerationPointPosition)
					&&(Paths[pos2].GenerationPointPosition != Paths[pos3].GenerationPointPosition)){
							
					Texture2D tx = GetRandomTexture(0, 4);
					
					NormalVehicle.GenerateNormalVehicle(pos3, vehiclePrefab, tx, Paths, cancelInvokeFirst15Vehicles, existedVehicles);
					vehicilesCounter++;
					AdjustEach15Vehicle();
				}
			}
			*/
			
		}
	}
	
	private Texture2D GetRandomTexture(int rangeFrom, int rangeTo){
		if(vehiclesTextures.Count == 0){
			Debug.LogError("vehiclesTextures is not initialized");
		}
		int i = Random.Range(rangeFrom, rangeTo);
		return vehiclesTextures[i];

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
	
	private bool CheckAllStreetsEmptiness(){
		int counter = 0;
		for(int i= 0 ; i < Paths.Count ; i++){
			if(Paths[i].PathStreets[0].VehiclesNumber >= 1)
				counter ++ ;
		}
		if(counter == Paths.Count)
			return false;
		else
			return true;
	}
	
	// Update is called once per frame
	void Update () {

		DisplayGUIs();
		
	}
	
	
	
	void DisplayGUIs(){
		
		
		if(eventsWarningTimes.Contains(gameTime)){
			index = eventsWarningTimes.IndexOf(gameTime);
			eventsWarningTimes[index] = -1; //out of game time
			if(!CheckAllStreetsFullness()){
				showBox = true;
			}
			
		}
		
		if(showBox){
			if(eventsWarningNames[index] == "a"){
				closeButtonGo.SetActive(true);
				eventWarningLabel.text = "Ambulance is Coming";
				eventsSpriteGo.SetActive(true);
				eventsSprite.spriteName = "ambulance1";
			}
			/*
			if(eventsWarningNames[index] == "b"){
				closeButtonGo.SetActive(true);
				eventWarningLabel.text = "Bus is Coming";
				eventsSpriteGo.SetActive(true);
				eventsSprite.spriteName = "bus1";
			}
			*/
			if(eventsWarningNames[index] == "s"){
				closeButtonGo.SetActive(true);
				eventWarningLabel.text = "Service Car is Coming";
				eventsSpriteGo.SetActive(true);
				eventsSprite.spriteName = "ice";
			}
			/*
			if(eventsWarningNames[index] == "t"){
				closeButtonGo.SetActive(true);
				eventWarningLabel.text = "Thief is Coming";
				eventsSpriteGo.SetActive(true);
				eventsSprite.spriteName = "kolu-bike";
			}
			*/
			/*
			if(eventsWarningNames[index] == "c"){
				closeButtonGo.SetActive(true);
				eventWarningLabel.text = "Caravan is Coming";
				eventsSpriteGo.SetActive(true);
				eventsSprite.spriteName = "caravan1";
			}
			
			*/
			
			Time.timeScale = 0;
			
		}
		
		
		
		gameTimeVarLabel.text = gameTime+"";
		scoreVarLabel.text = score+"";
		
		
		
		if(satisfyBar <= 2){
			mayorFacesSprite.spriteName = "veryHappy";
		}
		else if(satisfyBar <= 4){
			mayorFacesSprite.spriteName = "happy";
		}
		
		else if(satisfyBar <= 6){
			mayorFacesSprite.spriteName = "not happy";
		}
		
		else if(satisfyBar <= 8){
			mayorFacesSprite.spriteName = "sad";
		}
		
		else if(satisfyBar <= 10){
			mayorFacesSprite.spriteName = "very sad";
		}
		
		
		
		
		if(score >= 200){
			eventWarningLabel.text =  "Congratulations !! ";
			
			totalScoreLabel.text = "Total Score:";
			totalScoreVarLabel.text = score+"";
			
			
			GameObject [] gos = GameObject.FindGameObjectsWithTag("vehicle");
			for(int i = 0; i < gos.Length; i++){
				gos[i].SetActive(false);
			}
			
			GameObject [] obs = GameObject.FindGameObjectsWithTag("Finish");
			for(int i = 0; i < obs.Length; i++){
				obs[i].SetActive(false);
			}
			if(GameObject.FindGameObjectWithTag("MainCamera")!=null)
				GameObject.FindGameObjectWithTag("MainCamera").SetActive(false);
			
			if(GameObject.FindGameObjectWithTag("menu") == null){
				Instantiate(winMenu, new Vector3(0,0,0) ,Quaternion.identity);
			}
			//mainMenu.SetActiveRecursively(true);
			ScoreCounting.score = score*10;
			EventsCounter.eventsCompleted = trullyPassedEventsNum;
			
			
			CancelInvoke("CountTimeDown");
			//replayButtonGo.SetActive(true);
			//closeButtonGo.SetActive(false);
			//totalScoreVarLabelGo.SetActive(true);
			
		}
		
		else if(satisfyBar >= 10){
		
			eventWarningLabel.text =  "Traffic Jam !! ";
			
			totalScoreLabel.text = "Total Score:";
			totalScoreVarLabel.text = score+"";
			
			
			GameObject [] gos = GameObject.FindGameObjectsWithTag("vehicle");
			for(int i = 0; i < gos.Length; i++){
				gos[i].SetActive(false);
			}
			
			GameObject [] obs = GameObject.FindGameObjectsWithTag("Finish");
			for(int i = 0; i < obs.Length; i++){
				obs[i].SetActive(false);
			}
			if(GameObject.FindGameObjectWithTag("MainCamera")!=null)
				GameObject.FindGameObjectWithTag("MainCamera").SetActive(false);
			
			if(GameObject.FindGameObjectWithTag("menu") == null){
				Instantiate(loseMenu, new Vector3(0,0,0) ,Quaternion.identity);
			}
			//mainMenu.SetActiveRecursively(true);
			ScoreCounting.score = score*10;
			//EventsCounter.eventsCompleted = trullyPassedEventsNum;
			
			CancelInvoke("CountTimeDown");
			
		}
		
		
		
		else if(gameOver){
			
			if(!vibrationMade && Globals.vibrationEnabled == true){
				Handheld.Vibrate();
				vibrationMade = true;
			}
			
			eventWarningLabel.text =  "Accident !! ";
			
			totalScoreLabel.text = "Total Score:";
			totalScoreVarLabel.text = score+"";
			
			
			GameObject [] gos = GameObject.FindGameObjectsWithTag("vehicle");
			for(int i = 0; i < gos.Length; i++){
				gos[i].SetActive(false);
			}
			
			GameObject [] obs = GameObject.FindGameObjectsWithTag("Finish");
			for(int i = 0; i < obs.Length; i++){
				obs[i].SetActive(false);
			}
			if(GameObject.FindGameObjectWithTag("MainCamera")!=null)
				GameObject.FindGameObjectWithTag("MainCamera").SetActive(false);
			
			if(GameObject.FindGameObjectWithTag("menu") == null){
				Instantiate(loseMenu, new Vector3(0,0,0) ,Quaternion.identity);
			}
			//mainMenu.SetActiveRecursively(true);
			ScoreCounting.score = score*10;
			//EventsCounter.eventsCompleted = trullyPassedEventsNum;
			
			CancelInvoke("CountTimeDown");
		}
		
		else if(gameTime == 0){
		
			eventWarningLabel.text =  "Game Over !! ";
			
			totalScoreLabel.text = "Total Score:";
			totalScoreVarLabel.text = score+"";
			
			
			GameObject [] gos = GameObject.FindGameObjectsWithTag("vehicle");
			for(int i = 0; i < gos.Length; i++){
				gos[i].SetActive(false);
			}
			
			GameObject [] obs = GameObject.FindGameObjectsWithTag("Finish");
			for(int i = 0; i < obs.Length; i++){
				obs[i].SetActive(false);
			}
			if(GameObject.FindGameObjectWithTag("MainCamera")!=null)
				GameObject.FindGameObjectWithTag("MainCamera").SetActive(false);
			
			if(GameObject.FindGameObjectWithTag("menu") == null){
				Instantiate(loseMenu, new Vector3(0,0,0) ,Quaternion.identity);
			}
			//mainMenu.SetActiveRecursively(true);
			ScoreCounting.score = score*10;
			//EventsCounter.eventsCompleted = trullyPassedEventsNum;
			
			CancelInvoke("CountTimeDown");
		}
		
		
	}
	
	
	
}
