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
	
	private Level currentLevel;
	
	public List<GamePath> Paths;
	public List<Street> Streets;
	public List<Vector3> Intersections;
	public List<TrafficLight> Lights;
		
	//HUDs variables 
	public static int score;
	public float gameTime;
	public float satisfyBar;
	public bool gameOver;
	public int trullyPassedEventsNum;
	
	private const float GAME_TIME = 300;			//should equal to 5 minutes
	private const int WARNING_BEFORE_EVENT_SECONDS = 3;
	
	public List<Texture2D> vehiclesTextures;
	
	public GameObject vehiclePrefab;				//this object should be initialized in unity with the VehiclePrefab
	public GameObject ambulancePrefab;				//this object should be initialized in unity with the AmbulancePrefab
	public GameObject busPrefab;					//this object should be initialized in unity with the BusPrefab
	public GameObject caravanPrefab;		
	public GameObject serviceCarPrefab;		
	public GameObject thiefPrefab;			
	
	public GameObject intersectionPrefab;	
	
	public static List<float> eventsWarningTimes;	
	public static List<string> eventsWarningNames;	
	
	public Queue existedVehicles;					
	
	private SatisfyBar satisfyBarScript;
	
	private int initedCarsNumber;
	private bool cancelInvokeFirst15Vehicles;
	private bool canInstatiateTheRest;
	private int currentCarsNumber;
	public int secondsCounterForAnger;
	private bool instantiatThisTime;
	
	public static int vehicilesCounter;
	
	public bool showBox;
	
	
	public Texture2D accidentImage;
	
	/////////////////////////////////// GUI Variables ...
	
	
	public GameObject eventWarningLabelGo;
//	public GameObject closeButtonGo;
	public GameObject gameTimeLabelGo;
	public GameObject gameTimeVarLabelGo;
	public GameObject scoreLabelGo;
	public GameObject scoreVarLabelGo;
	public GameObject satisfyBarLabelGo;
	
	public UILabel eventWarningLabel;
	//public UIButton closeButton;
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
	
	//public GameObject eventsSpriteGo;
	//private UISlicedSprite eventsSprite;
	
	public GameObject endGameSpriteGo;
	private UISlicedSprite endGameSprite;




	public GameObject winMenu;
	public GameObject loseMenu;
	
	public bool vibrationMade = false;
	
	private int vehiclesShouldGenerated;
	private int currentGroupNumber;
	private int currentRateIndex;
		
	private void InitGUIVariables(){
		eventWarningLabel = eventWarningLabelGo.GetComponent<UILabel>();
//		closeButton = closeButtonGo.GetComponent<UIButton>();
//		closeButtonGo.SetActive(false);
		
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
	}
	
	
	int index =0 ;
	void Awake(){
		currentGroupNumber = 0;
		
		InitTheCurrentLevel();
		initObjects();
		initVariables();
		InitGUIVariables();
		
	}
	
	
	private void InitTheCurrentLevel(){
		MapsData data = new MapsData();
		//levels = new List<Level>();
		
		if(Application.loadedLevelName == "Map 1"){
			Map map = new Map(data.GetMap1Streets(), data.GetMap1Paths(), data.GetMap1Intersections(), data.GetMap1Lights());
			/*
			List<VehicleType> events = new List<VehicleType>();
			events.Add(VehicleType.Ambulance);
			events.Add(VehicleType.Bus);
			List<int> eventsNumbers = new List<int>();
			eventsNumbers.Add(1);
			eventsNumbers.Add(1);
			List<Range> ranges = new List<Range>();
			ranges.Add(new Range(120, 130));
			ranges.Add(new Range(130, 140));
			EventRanges eventRangesObject = new EventRanges(ranges);
			*/
		//	currentLevel = new Level(1, map, 27.0f, 4, 200, 150, null, null, null, null);
		}
		
		else if(Application.loadedLevelName == "Map 2"){
			Map map = new Map(data.GetMap2Streets(), data.GetMap2Paths(), data.GetMap2Intersections(), data.GetMap2Lights());
			/*
			List<VehicleType> events = new List<VehicleType>();
			events.Add(VehicleType.Bus);
			events.Add(VehicleType.Ambulance);
			events.Add(VehicleType.ServiceCar);
			events.Add(VehicleType.Caravan);
			List<int> eventsNumbers = new List<int>();
			eventsNumbers.Add(1);
			eventsNumbers.Add(1);
			eventsNumbers.Add(1);3030300
			eventsNumbers.Add(1);
			List<EventTimes> eventTimes = new List<EventTimes>();
			*/
			List<int> groups = new List<int>();
			//25
			groups.Add(1);
			groups.Add(2);
			groups.Add(2);
			groups.Add(4);
			groups.Add(6);
			groups.Add(10);
			
			
			//75
			groups.Add(10);
			groups.Add(15);
			groups.Add(25);
			groups.Add(25);
			
			//100
			groups.Add(50);
			groups.Add(50);
			
			List<int []> init_rates_intervals = new List<int[]>();
			
			init_rates_intervals.Add(new int[2]{1,280});
			init_rates_intervals.Add(new int[2]{2,260});
			init_rates_intervals.Add(new int[2]{3,240});
			init_rates_intervals.Add(new int[2]{4,220});
			init_rates_intervals.Add(new int[2]{5,200});
			
			init_rates_intervals.Add(new int[2]{8,0});
			
			/*
			init_rates_intervals.Add(new int[2]{20,80});
			init_rates_intervals.Add(new int[2]{25,60});
			init_rates_intervals.Add(new int[2]{30,40});
			init_rates_intervals.Add(new int[2]{35,20});
			init_rates_intervals.Add(new int[2]{40,0});
			*/
			/*
			init_rates_intervals[1,0] = 2;
			init_rates_intervals[1,1] = 50;
			init_rates_intervals[2,0] = 3;
			init_rates_intervals[2,1] = 0;
			*/
			
			currentLevel = new Level(2, map, 20.0f, groups, init_rates_intervals, 200, 150, null, null, null, null);
			
			vehiclesShouldGenerated = groups[0];
			currentRateIndex = 0;
		}
		
	}
	
	private void initVariables(){
		satisfyBarScript = GameObject.FindGameObjectWithTag("satisfyBar").GetComponent<SatisfyBar>();		
	
		existedVehicles = new Queue();
		trullyPassedEventsNum = 0;
		showBox = false;
		instantiatThisTime = false;
		vehicilesCounter = 0;
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
		
		
		InstantiateIntersections();
		
		//InvokeRepeating("generateFirst15Cars", Time.deltaTime, 0.3f);
		InvokeRepeating("CountTimeDown", 1.0f, 1.0f);
	}
	
	private void initObjects(){
		Streets = currentLevel.LevelMap.Streets;
		Paths = currentLevel.LevelMap.GamePaths;
		Intersections = currentLevel.LevelMap.Intersections;
		Lights = currentLevel.LevelMap.Lights;
		
		eventsWarningTimes = new List<float>();
		eventsWarningNames = new List<string>();
		
		if(currentLevel.LevelEvents != null){
			InitEventsNeeds();
		}
		
		
	}
	
	private void InitEventsNeeds(){
		if(currentLevel.LevelEvents.Contains(VehicleType.Ambulance)){
			Ambulance.InitInstances();
			Ambulance.SetEventTime(currentLevel.EventsTimesList.TimesList);
		}
		if(currentLevel.LevelEvents.Contains(VehicleType.Bus)){
			Bus.InitInstances();
			Bus.SetEventTime(currentLevel.EventsTimesList.TimesList);
		}
		if(currentLevel.LevelEvents.Contains(VehicleType.Caravan)){
			Caravan.InitInstances();
			Caravan.SetEventTime(currentLevel.EventsTimesList.TimesList);
		}
		if(currentLevel.LevelEvents.Contains(VehicleType.ServiceCar)){
			ServiceCar.InitInstances();
			ServiceCar.SetEventTime(currentLevel.EventsTimesList.TimesList);
		}
		if(currentLevel.LevelEvents.Contains(VehicleType.Thief)){
			Thief.InitInstances();
			Thief.SetEventTime(currentLevel.EventsTimesList.TimesList);
		}
		
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
	
	
	//This method is called in the Start() it counts down the game time each second and generates the vehicles in the random time calculated at the first of the game
	void CountTimeDown ()
	{
		if(--gameTime == 0)
		{
			CancelInvoke("CountTimeDown");
			
		}
	//	gameTimeVarLabel.text = gameTime+"";
		//gameTimeLabel = gameTimeLabelGo.GetComponent<UILabel>();
		//if( GameObject.FindGameObjectsWithTag("vehicle").Length < 10){ //***********************should beeee 15
			secondsCounterForAnger ++;
			if(secondsCounterForAnger == 90){	
				if(satisfyBar >=0){
					satisfyBar --;
					secondsCounterForAnger = 0;
					satisfyBarScript.AddjustSatisfaction(-1);
				}
			}
	//	}
	//	else{
	//		secondsCounterForAnger = 0;
		
	//	}
		
		
	//	if( canInstatiateTheRest){
		//for(int i=0; i<currentLevel.InstatiationRateAndIntervals.GetUpperBound(0); i++){
		Debug.LogWarning("amiraaaaaaaaaaa"+currentLevel.InstatiationRateAndIntervals.Count);
		if((currentRateIndex < currentLevel.InstatiationRateAndIntervals.Count) && (gameTime > currentLevel.InstatiationRateAndIntervals[currentRateIndex][1] )){
			if(vehiclesShouldGenerated >0){
				InstantiatVehiclesArroundTime(currentLevel.InstatiationRateAndIntervals[currentRateIndex][0]);
				
			}
		}
		else if(gameTime <= currentLevel.InstatiationRateAndIntervals[currentRateIndex][1]){
			currentRateIndex ++;
		}
		
	//	if(currentCarsNumber ==0){
			
			
	//	}
		
		if(GameObject.FindGameObjectsWithTag("vehicle").Length==0){
			currentGroupNumber ++;
			if(currentGroupNumber<currentLevel.VehicleGroups.Count){
				vehiclesShouldGenerated = currentLevel.VehicleGroups[currentGroupNumber];
			}
		}
		
	//	}
	//	}
	}
	
	private void InstantiatVehiclesArroundTime(int rate){
		List<int> randomsList = new List<int>();
	//	instantiatThisTime = !instantiatThisTime;
		
			/*		
			else {
				Texture2D tx1 = GetRandomTexture(0, 4);
				NormalVehicle.GenerateNormalVehicle(pos, vehiclePrefab, tx1, Paths, cancelInvokeFirst15Vehicles, existedVehicles);
				vehicilesCounter++;
				AdjustEach15Vehicle();
			}
			*/
		//randomsList = CheckAndGenerateTheEventElseVehicle(randomsList);
		
		for(int i = 0; i<rate; i++){
			List<int> temp = CheckAndGenerateVehicle(randomsList);
			randomsList = temp;
		}
			
	}
	
	private List<int> CheckAndGenerateTheEventElseVehicle(List<int> randomsList){
		
		if(!CheckAllStreetsFullness()){
				//this piece of code is for the events
			/*
			int pos = Random.Range(0, Paths.Count);
			while(Paths[pos].PathStreets[0].VehiclesNumber >= Paths[pos].PathStreets[0].StreetCapacity){
					pos = Random.Range(0, Paths.Count);
			}
			
			*/
			if(currentLevel.LevelEvents.Contains(VehicleType.Bus)&& Bus.InsideTimeSlotsList(gameTime)){
				vibrationMade = false;
				showBox = false;
				eventWarningLabelGo.SetActive(false);
				//eventWarningLabel.text = "";
				Debug.Log("should be a bus");
				Bus.GenerateVehicle(busPrefab, currentLevel.EventsPaths[0]); //*********************temppppppppppppppppppp SHOULD BE CHANGED
				vehicilesCounter ++;
				AdjustEach15Vehicle();
				//randomsList.Add();
			}
			
			
			else if(currentLevel.LevelEvents.Contains(VehicleType.Ambulance)&& Ambulance.InsideTimeSlotsList(gameTime)) {
				vibrationMade = false;
				showBox = false;
				eventWarningLabelGo.SetActive(false);
				eventWarningLabel.text = "";
				Debug.Log("should be ambulance");
				Ambulance.GenerateVehicle(ambulancePrefab, currentLevel.EventsPaths[0]);
				vehicilesCounter ++;
				AdjustEach15Vehicle();
				//randomsList.Add(pos);
			}
			
			else if(currentLevel.LevelEvents.Contains(VehicleType.Caravan)&& Caravan.InsideTimeSlotsList(gameTime)) {
				vibrationMade = false;
				showBox = false;
				eventWarningLabelGo.SetActive(false);
				eventWarningLabel.text = "";
				Debug.Log("should be caravan");
				Caravan.GenerateVehicle(caravanPrefab, currentLevel.EventsPaths[0]);
				vehicilesCounter ++;
				AdjustEach15Vehicle();
				//randomsList.Add(pos);
			}
			
			else if(currentLevel.LevelEvents.Contains(VehicleType.ServiceCar)&& ServiceCar.InsideTimeSlotsList(gameTime)) {
				vibrationMade = false;
				showBox = false;
				eventWarningLabelGo.SetActive(false);
				eventWarningLabel.text = "";
				Debug.Log("should be a service car");
				Texture2D tx = GetRandomTexture(4, 7);
				ServiceCar.GenerateVehicle(serviceCarPrefab, currentLevel.EventsPaths[0]);
				vehicilesCounter ++;
				AdjustEach15Vehicle();
				//randomsList.Add(pos);
			}
			
			
			else if(currentLevel.LevelEvents.Contains(VehicleType.Thief)&& Thief.InsideTimeSlotsList(gameTime)) {
				if(CheckAllStreetsEmptiness()){
					vibrationMade = false;
				showBox = false;
				eventWarningLabelGo.SetActive(false);
				eventWarningLabel.text = "";
					Debug.Log("should be a thief");
					Thief.GenerateVehicle(thiefPrefab, currentLevel.EventsPaths[0]);
					vehicilesCounter ++;
					AdjustEach15Vehicle();
					//randomsList.Add(pos);
				}
			}
		
		
		}
		return randomsList;
	}
	
	private List<int> CheckAndGenerateVehicle(List<int> randoms){
		int pos = Random.Range(0, Paths.Count);
		
		if(!CheckAllStreetsFullness()){
			while(Paths[pos].PathStreets[0].VehiclesNumber >= Paths[pos].PathStreets[0].StreetCapacity){
				pos = Random.Range(0, Paths.Count);
			}
		
			
			if(!isRepeatedPosition(randoms, pos)){
				Texture2D tx = GetRandomTexture(0, 4);
				NormalVehicle.GenerateNormalVehicle(pos, vehiclePrefab, tx, Paths, cancelInvokeFirst15Vehicles, existedVehicles);
				vehiclesShouldGenerated--;
				vehicilesCounter++;
				AdjustEach15Vehicle();
				randoms.Add(pos);
			}
		}
//		Debug.Log("randoms toolha kedaa " + randoms.Count);
		return randoms;
	}
	
	private bool isRepeatedPosition(List<int> randoms, int pos){
		bool found = false;
		
		int i =0;
		while(!found && i<randoms.Count){
			if(Paths[randoms[i]].GenerationPointPosition == Paths[pos].GenerationPointPosition){
				found = true;
			}
			i++;
		}
		return found;
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
		if(initedCarsNumber == 10){	//**************************************** should be 15
			cancelInvokeFirst15Vehicles = true;
			initedCarsNumber =0;
		}
	}
	
	
	private bool CheckAllStreetsFullness(){
		int counter = 0;
		for(int i= 0 ; i < Paths.Count ; i++){
			if(Paths[i].PathStreets[0].VehiclesNumber >= Paths[i].PathStreets[0].StreetCapacity-1){
				
				counter ++ ;
			}
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
				
//		Debug.Log("el group ahooooooo   "+currentLevel.VehicleGroups[currentGroupNumber]);	
	//	Debug.Log("el rate nowww  " + currentLevel.InstatiationRateAndIntervals[currentRateIndex][0]);
		
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
				//closeButtonGo.SetActive(true);
				
				eventWarningLabelGo.SetActive(true);
			//	vibrationMade = false;
				if(!vibrationMade && Globals.vibrationEnabled == true){
					Handheld.Vibrate();
					vibrationMade = true;
				}
				eventWarningLabel.text = "Ambulance is Coming from the West";
				//eventsSpriteGo.SetActive(true);
				//eventsSprite.spriteName = "ambulance1";
			}
			
			if(eventsWarningNames[index] == "b"){
			//	vibrationMade = false;
				if(!vibrationMade && Globals.vibrationEnabled == true){
					Handheld.Vibrate();
					vibrationMade = true;
				}
				//closeButtonGo.SetActive(true);
				eventWarningLabelGo.SetActive(true);
				eventWarningLabel.text = "Bus is Coming";
				//eventsSpriteGo.SetActive(true);
				//eventsSprite.spriteName = "bus1";
			}
			
			
			if(eventsWarningNames[index] == "s"){
				//	vibrationMade = false;
				if(!vibrationMade && Globals.vibrationEnabled == true){
					Handheld.Vibrate();
					vibrationMade = true;
				}
				//closeButtonGo.SetActive(true);
				eventWarningLabelGo.SetActive(true);
				eventWarningLabel.text = "Service Car is Coming";
				//eventsSpriteGo.SetActive(true);
				//eventsSprite.spriteName = "ice";
			}
			
			if(eventsWarningNames[index] == "t"){
				//	vibrationMade = false;
				if(!vibrationMade && Globals.vibrationEnabled == true){
					Handheld.Vibrate();
					vibrationMade = true;
				}
				//closeButtonGo.SetActive(true);
				eventWarningLabelGo.SetActive(true);
				eventWarningLabel.text = "Thief is Coming";
				//eventsSpriteGo.SetActive(true);
				//eventsSprite.spriteName = "kolu-bike";
			}
			
			
			if(eventsWarningNames[index] == "c"){
				//	vibrationMade = false;
				if(!vibrationMade && Globals.vibrationEnabled == true){
					Handheld.Vibrate();
					vibrationMade = true;
				}
				//closeButtonGo.SetActive(true);
				eventWarningLabelGo.SetActive(true);
				eventWarningLabel.text = "Caravan is Coming";
				//eventsSpriteGo.SetActive(true);
				//eventsSprite.spriteName = "caravan1";
			}
			
			
			
			//Time.timeScale = 0;
			
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
		
		//	eventWarningLabel.text =  "Traffic Jam !! ";
			
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
		
		
	/*	
		else if(gameOver){
			
			if(!vibrationMade && Globals.vibrationEnabled == true){
				Handheld.Vibrate();
				vibrationMade = true;
			}
			
			
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
			ScoreCounting.score = score*10;
			//EventsCounter.eventsCompleted = trullyPassedEventsNum;
			
			CancelInvoke("CountTimeDown");
		}
		*/
		else if(gameTime == 0){
		
			
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
