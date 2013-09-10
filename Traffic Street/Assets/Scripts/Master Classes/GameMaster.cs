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
	

	//Level variables
	public Level currentLevel;
	
	public List<GamePath> Paths;
	public List<Street> Streets;
	public List<Vector3> Intersections;
	public List<TrafficLight> Lights;
	public List<LightsGroup> LightsGroups;
	public GameObject [] Corners;
		
	//HUDs variables 
	public static int score;
	public float gameTime;
	public float satisfyBar;
	public bool gameOver;
	public int trullyPassedEventsNum;
	
	
	
	public List<Material> vehiclesTextures;
	
	public GameObject vehiclePrefab;				//this object should be initialized in unity with the VehiclePrefab
	public GameObject ambulancePrefab;				//this object should be initialized in unity with the AmbulancePrefab
	public GameObject busPrefab;					//this object should be initialized in unity with the BusPrefab
	public GameObject caravanPrefab;		
	public GameObject serviceCarPrefab;		
	public GameObject thiefPrefab;
	public GameObject policePrefab;
	public GameObject taxiPrefab;
	public GameObject accidentCarPrefab;
	
	public GameObject intersectionPrefab;	
	
	public static List<float> eventsWarningTimes;	
	public static List<string> eventsWarningNames;	
	
	public Queue existedVehicles;					
	private SatisfyBar satisfyBarScript;
	public int secondsCounterForAnger;
	public static int vehicilesCounter;
	
	
	
	/////////////////////////////////// GUI Variables ...
	
	public bool showBox;
	public GameObject eventWarningLabelGo;
	public GameObject gameTimeLabelGo;
	public GameObject gameTimeVarLabelGo;
	public GameObject scoreLabelGo;
	public GameObject scoreVarLabelGo;
	public GameObject satisfyBarLabelGo;
	
	public UILabel eventWarningLabel;
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

	private bool instantiationFlag;


	public GameObject winMenu;
	public GameObject loseMenu;
	
	public bool vibrationMade = false;
	
	private int vehiclesShouldGenerated;
	private int currentGroupNumber;
	private int currentRateIndex;
	
	public AudioClip ambulanceSound;
	
	public GameObject accidentSmoke;
	public AudioClip accidentSound;
	
	public List<int> eventsCounter;
	
	private void InitGUIVariables(){
		eventWarningLabel 	= eventWarningLabelGo.GetComponent<UILabel>();
		gameTimeLabel 		= gameTimeLabelGo.GetComponent<UILabel>();
		gameTimeVarLabel 	= gameTimeVarLabelGo.GetComponent<UILabel>();
		scoreLabel 			= scoreLabelGo.GetComponent<UILabel>();
		scoreVarLabel 		= scoreVarLabelGo.GetComponent<UILabel>();
		satisfyBarLabel 	= satisfyBarLabelGo.GetComponent<UILabel>();
		mayorFacesSprite 	= mayorFacesSpriteGo.GetComponent<UISlicedSprite>();
		totalScoreLabel		= totalScoreLabelGo.GetComponent<UILabel>();
		totalScoreVarLabel 	= totalScoreVarLabelGo.GetComponent<UILabel>();
		replayButton 		= replayButtonGo.GetComponent<UIButton>();
		
		totalScoreVarLabelGo.SetActive(false);
		replayButtonGo.SetActive(false);
	}
	
	
	int index =0 ;
	void Awake(){
		currentGroupNumber = 0;
		instantiationFlag = true;
		eventsCounter = new List<int>();
		Corners = GameObject.FindGameObjectsWithTag("corner");
		InitTheCurrentLevel();
		initObjects();
		initVariables();
		InitGUIVariables();
		
		accidentSmoke.SetActive(false);
	}
	
	
	private void InitTheCurrentLevel(){
		MapsData data = new MapsData();
		
		
		
		if(Application.loadedLevelName == "Map 1"){
			Map map = new Map(data.GetMap1Streets(), data.GetMap1Paths(), data.GetMap1Intersections(), data.GetMap1Lights(), data.GetMap1LightsGroups());
			
			/*public Level(	int anId, 
			 				Map aMap, 
							float theNormalVehicleSpeed, 
							List<int> theVehicleGroups,
							List<int []> theInstantiationRateAndIntervals, 
							int theMinScore,
							float theGameTime, 
							List<VehicleType> theLevelEvents, 
							List<int> theEventsNumbers, 
							List<EventTimes> theEventTimes, 
							List<List <GamePath>>theEventsPaths)
				*/
			
			currentLevel = new Level(	1, 
										map, 
										Map1_Data.NormalVehicleSpeed, 
										Map1_Data.VehiclesGroups(), 
										Map1_Data.InistantiationRatesIntervals(), 
										Map1_Data.MinScore,  
										Map1_Data.GameTime,  
										null,  
										null,  
										null,  
										null);
			vehiclesShouldGenerated = currentLevel.VehicleGroups[0];
			currentRateIndex = 0;
			gameTime = currentLevel.GameTime;
		}
		else if(Application.loadedLevelName == "Map 2"){
			Map map = new Map(data.GetMap2Streets(), data.GetMap2Paths(), data.GetMap2Intersections(), data.GetMap2Lights(), data.GetMap2LightsGroups());
						
			currentLevel = new Level(	2, 
										map, 
										Map2_Data.NormalVehicleSpeed, 
										Map2_Data.VehiclesGroups(), 
										Map2_Data.InistantiationRatesIntervals(), 
										Map2_Data.MinScore,  
										Map2_Data.GameTime, 
										null,  
										null,  
										null,  
										null);
			
			vehiclesShouldGenerated = currentLevel.VehicleGroups[0];
			currentRateIndex = 0;
			gameTime = currentLevel.GameTime;
		}
		
		
	}
	
	private void initVariables(){
		
		//Globals.NORMAL_AVG_VEHICLE_SPEED = currentLevel.NormalVehicleSpeed;
		
		satisfyBarScript = GameObject.FindGameObjectWithTag("satisfyBar").GetComponent<SatisfyBar>();		
	
		existedVehicles = new Queue();
		trullyPassedEventsNum = 0;
		showBox = false;
		vehicilesCounter = 0;
		secondsCounterForAnger = 0;
		score = 0;
		satisfyBar = 2;
		satisfyBarScript.AddjustSatisfaction(2);
		
		
	}
	
	// Use this for initialization
	void Start () {
		
		InstantiateIntersections();
		InvokeRepeating("CountTimeDown", 1.0f, 1.0f);
	}
	
	private void initObjects(){
		Streets = currentLevel.LevelMap.Streets;
		Paths = currentLevel.LevelMap.GamePaths;
		Intersections = currentLevel.LevelMap.Intersections;
		Lights = currentLevel.LevelMap.Lights;
		LightsGroups = currentLevel.LevelMap.LightsGroups;
		
		eventsWarningTimes = new List<float>();
		eventsWarningNames = new List<string>();
		
		InitEventsLists();
		InitEventsNeeds();
		
		
	}
	
	private void InitEventsLists(){
		if(currentLevel.ID == 1){
			currentLevel.LevelEvents	 = Map1_Data.Events();
			currentLevel.EventsNumber	 = Map1_Data.EventsNumbers();
			currentLevel.EventsTimesList = Map1_Data.EventsTimes();
			currentLevel.EventsPaths	 = Map1_Data.EventsPaths(Paths);
		}
		else if(currentLevel.ID == 2){
			currentLevel.LevelEvents 	 = Map2_Data.Events();
			currentLevel.EventsNumber	 = Map2_Data.EventsNumbers();
			currentLevel.EventsTimesList = Map2_Data.EventsTimes();
			currentLevel.EventsPaths 	 = Map2_Data.EventsPaths(Paths);
		}		
	}
	
	private void InitEventsNeeds(){
	//	for(int i = 0; i<7 ; i++){
			
	//	}
		if(currentLevel.LevelEvents.Contains(VehicleType.Ambulance)){
			Ambulance.InitInstances();
			Ambulance.SetEventTime(currentLevel.EventsTimesList[currentLevel.LevelEvents.IndexOf(VehicleType.Ambulance)].TimesList);
			eventsCounter.Add(0);
		}
		if(currentLevel.LevelEvents.Contains(VehicleType.Bus)){
			Bus.InitInstances();
			Bus.SetEventTime(currentLevel.EventsTimesList[currentLevel.LevelEvents.IndexOf(VehicleType.Bus)].TimesList);
			eventsCounter.Add(0);
			//Debug.Log(eventsCounter.Count);
		}
		if(currentLevel.LevelEvents.Contains(VehicleType.Caravan)){
			Caravan.InitInstances();
			Caravan.SetEventTime(currentLevel.EventsTimesList[currentLevel.LevelEvents.IndexOf(VehicleType.Caravan)].TimesList);
			eventsCounter.Add(0);
		}
		if(currentLevel.LevelEvents.Contains(VehicleType.ServiceCar)){
			ServiceCar.InitInstances();
			ServiceCar.SetEventTime(currentLevel.EventsTimesList[currentLevel.LevelEvents.IndexOf(VehicleType.ServiceCar)].TimesList);
			eventsCounter.Add(0);
		}
		if(currentLevel.LevelEvents.Contains(VehicleType.Thief)){
			Thief.InitInstances();
			Thief.SetEventTime(currentLevel.EventsTimesList[currentLevel.LevelEvents.IndexOf(VehicleType.Thief)].TimesList);
			eventsCounter.Add(0);
		}
		if(currentLevel.LevelEvents.Contains(VehicleType.Police)){
			Police.InitInstances();
			Police.SetEventTime(currentLevel.EventsTimesList[currentLevel.LevelEvents.IndexOf(VehicleType.Police)].TimesList);
			eventsCounter.Add(0);
		}
		if(currentLevel.LevelEvents.Contains(VehicleType.Taxi)){
			Taxi.InitInstances();
			Taxi.SetEventTime(currentLevel.EventsTimesList[currentLevel.LevelEvents.IndexOf(VehicleType.Taxi)].TimesList);
			eventsCounter.Add(0);
		}
	}
	
	private void InstantiateIntersections(){
		
		for(int i = 0; i<Intersections.Count ; i++){
			Instantiate(intersectionPrefab, Intersections[i] ,Quaternion.identity);
		}
		
	}
	//******************************************************** End Of Start Methodes *********************************
	
	//This method is called in the Start() it counts down the game time each second and generates the vehicles in the random time calculated at the first of the game
	void CountTimeDown ()
	{
		instantiationFlag = true;
		if(--gameTime == 0)
		{
			CancelInvoke("CountTimeDown");
			
		}
		
		ChangeSpeedOverTime();
		
		CountAngerAndChangeSatisfaction();
	
		//CheckAndGenerateTheEventElseVehicle();
		
		ManageInstantiationAndRates();
		
		TrasfereToTheNextGroupOverTime();
	//	if(gameTime<currentLevel.GameTime-3){
			ChangeStreetCapacityForThief();
	//	}
	}
	
	private void ChangeSpeedOverTime(){
		if(gameTime%50 == 0){
//			Debug.Log ("the speed has been incremented ---> "+ Globals.NORMAL_AVG_VEHICLE_SPEED);
			Globals.starting_normal_avg_speed += Globals.speed_increase_rate;
		}
	}
	
	private void CountAngerAndChangeSatisfaction(){
		secondsCounterForAnger ++;
		if(secondsCounterForAnger == Globals.SATISFACTION_VEHICLES_DECREMENTER){	
			if(satisfyBar >=0){
				satisfyBar --;
				secondsCounterForAnger = 0;
				satisfyBarScript.AddjustSatisfaction(-1);
			}
		}
	}
	
	private void ManageInstantiationAndRates(){
		if((currentRateIndex < currentLevel.InstatiationRateAndIntervals.Count) && (gameTime > currentLevel.InstatiationRateAndIntervals[currentRateIndex][1] )){
			
			if(vehiclesShouldGenerated >0 && instantiationFlag){
				InstantiatVehiclesArroundTime(currentLevel.InstatiationRateAndIntervals[currentRateIndex][0]);
				
			}
		}
		else if(gameTime <= currentLevel.InstatiationRateAndIntervals[currentRateIndex][1]){
			currentRateIndex ++;
		}
	}
	
	private void InstantiatVehiclesArroundTime(int rate){
		List<int> randomsList = new List<int>();
			
		for(int i = 0; i<rate; i++){
			List<int> temp = CheckAndGenerateVehicle(randomsList);
			randomsList = temp;
		}
			
	}
		
	private void TrasfereToTheNextGroupOverTime(){
		GameObject [] vehiclesFound = GameObject.FindGameObjectsWithTag("vehicle");
		int tempLength = vehiclesFound.Length;
		
		int counter =0;
		for(int i= 0; i< vehiclesFound.Length; i++){
			VehicleController tempScript = vehiclesFound[i].GetComponent<VehicleController>();
			if(tempScript!= null){
				if(tempScript.myVehicle.Type != VehicleType.Normal){
					tempLength--;
				}
				else if(tempScript.myVehicle.NextStreet == null){
					counter ++;
				}
			}
			
		}
		if(counter == tempLength ){
			currentGroupNumber ++;
			if(currentGroupNumber<currentLevel.VehicleGroups.Count){
				vehiclesShouldGenerated = currentLevel.VehicleGroups[currentGroupNumber];
			}
		}
	}
	
	private void OnGeneratingEvents(){
		vibrationMade = false;
		showBox = false;
		vehicilesCounter ++;
		instantiationFlag = false;
		
	}
	
	private void CheckAndGenerateTheEventElseVehicle(){
		
		if(!CheckAllStreetsFullness()){
			int index ;
			if(currentLevel.LevelEvents.Contains(VehicleType.Ambulance)&& Ambulance.InsideTimeSlotsList(gameTime)) {
				OnGeneratingEvents();
				index = currentLevel.LevelEvents.IndexOf(VehicleType.Ambulance);
				
				Ambulance.GenerateVehicle(ambulancePrefab, currentLevel.EventsPaths[index][eventsCounter[index]]);
				eventsCounter[index]++;
			}
			
			else if(currentLevel.LevelEvents.Contains(VehicleType.Bus)&& Bus.InsideTimeSlotsList(gameTime)){
				
				OnGeneratingEvents();
				index = currentLevel.LevelEvents.IndexOf(VehicleType.Bus);
				Bus.GenerateVehicle(busPrefab, currentLevel.EventsPaths[index][eventsCounter[index]]); //*********************temppppppppppppppppppp SHOULD BE CHANGED
				eventsCounter[index]++;
			}
			
			else if(currentLevel.LevelEvents.Contains(VehicleType.Caravan)&& Caravan.InsideTimeSlotsList(gameTime)) {
				OnGeneratingEvents();
				index = currentLevel.LevelEvents.IndexOf(VehicleType.Caravan);
				Caravan.GenerateVehicle(caravanPrefab, currentLevel.EventsPaths[index][eventsCounter[index]]);
				eventsCounter[index]++;
			}
			
			else if(currentLevel.LevelEvents.Contains(VehicleType.ServiceCar)&& ServiceCar.InsideTimeSlotsList(gameTime)) {
				OnGeneratingEvents();
				index = currentLevel.LevelEvents.IndexOf(VehicleType.ServiceCar);
				ServiceCar.GenerateVehicle(serviceCarPrefab, currentLevel.EventsPaths[index][eventsCounter[index]]);
				eventsCounter[index]++;
			}
			
			
			else if(currentLevel.LevelEvents.Contains(VehicleType.Thief)&& Thief.InsideTimeSlotsList(gameTime)) {
				if(CheckAllStreetsEmptiness()){
					OnGeneratingEvents();		
					index = currentLevel.LevelEvents.IndexOf(VehicleType.Thief);
					Thief.GenerateVehicle(thiefPrefab, currentLevel.EventsPaths[index][eventsCounter[index]]);
					eventsCounter[index]++;
				}
			}
			
			else if(currentLevel.LevelEvents.Contains(VehicleType.Police)&& Police.InsideTimeSlotsList(gameTime)) {
				if(CheckAllStreetsEmptiness()){
					OnGeneratingEvents();
					index = currentLevel.LevelEvents.IndexOf(VehicleType.Police);
					Police.GenerateVehicle(policePrefab, currentLevel.EventsPaths[index][eventsCounter[index]]);
					eventsCounter[index]++;
				}
			}
		
			else if(currentLevel.LevelEvents.Contains(VehicleType.Taxi)&& Taxi.InsideTimeSlotsList(gameTime)) {
				OnGeneratingEvents();
				index = currentLevel.LevelEvents.IndexOf(VehicleType.Taxi);
				Taxi.GenerateVehicle(taxiPrefab, currentLevel.EventsPaths[index][eventsCounter[index]]);
				eventsCounter[index]++;
			}
			
		}
	}
	
	private List<int> CheckAndGenerateVehicle(List<int> randoms){
		int pos = Random.Range(0, Paths.Count);
		
		if(!CheckAllStreetsFullness()){
			while(Paths[pos].PathStreets[0].VehiclesNumber >= Paths[pos].PathStreets[0].StreetCapacity){
				pos = Random.Range(0, Paths.Count);
			}
			if(!isRepeatedPosition(randoms, pos)){
				Material tx = GetRandomTexture(0, 4);
				NormalVehicle.GenerateNormalVehicle(pos, vehiclePrefab, tx, Paths, existedVehicles, Globals.starting_normal_avg_speed);
				vehiclesShouldGenerated--;
				vehicilesCounter++;
				randoms.Add(pos);
			}
		}
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
	
	
	private Material GetRandomTexture(int rangeFrom, int rangeTo){
		if(vehiclesTextures.Count == 0){
			Debug.LogError("vehiclesTextures is not initialized");
		}
		int i = Random.Range(rangeFrom, rangeTo);
		return vehiclesTextures[i];

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
	
		DisplayGUIs();
		////////////////////////////////************************* the thief event...
		
	}
	
	private void ChangeStreetCapacityForThief(){
		int index = currentLevel.LevelEvents.IndexOf(VehicleType.Thief);
		//int index = 5;
//		Debug.Log("this is the index "+ currentLevel.EventsTimesList.Count + "  PPPPPP  "+ eventsCounter[index]);
		if(index != -1){
			if(currentLevel.EventsTimesList[index].TimesList.Count > eventsCounter[index]){
				if(gameTime == currentLevel.EventsTimesList[index].TimesList[eventsCounter[index]] + Globals.EMPTY_STREET_BEFORE_THIEF_TIMER){
					currentLevel.EventsPaths[index][eventsCounter[index]].PathStreets[0].StreetCapacity = 0;
				}
				
				if(gameTime == currentLevel.EventsTimesList[index].TimesList[eventsCounter[index]] - Globals.EMPTY_STREET_AFTER_THIEF_TIMER){
					if(currentLevel.ID == 1){
						currentLevel.EventsPaths[index][eventsCounter[index]].PathStreets[0].StreetCapacity = Map1_Data.originalThiefStreetCapacity;
					}
					else if(currentLevel.ID == 2){
						currentLevel.EventsPaths[index][eventsCounter[index]].PathStreets[0].StreetCapacity = Map2_Data.originalThiefStreetCapacity;
					}
					//////////////////////////////////////////////////////Rest of the levels
				}
			}
		}
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
			if(eventsWarningNames[index] == "ambulance"){
				
				if(!vibrationMade && Globals.vibrationEnabled == true){
					Handheld.Vibrate();
					vibrationMade = true;
				}
				if(!MessageBar.messagesQ.Contains(Globals.AMBULANCE_WARNING_MSG))
					MessageBar.messagesQ.Enqueue(Globals.AMBULANCE_WARNING_MSG);
			//	eventWarningLabel.text = "Ambulance is Coming from the east";
				//audio.PlayOneShot(ambulanceSound);
				
			}
			
			if(eventsWarningNames[index] == "bus"){
			
				if(!vibrationMade && Globals.vibrationEnabled == true){
					Handheld.Vibrate();
					vibrationMade = true;
				}
				
				if(!MessageBar.messagesQ.Contains(Globals.BUS_WARNING_MSG))
					MessageBar.messagesQ.Enqueue(Globals.BUS_WARNING_MSG);
				//eventWarningLabel.text = "Bus is Coming from the west";
				
			}
			
			
			if(eventsWarningNames[index] == "serviceCar"){
				if(!vibrationMade && Globals.vibrationEnabled == true){
					Handheld.Vibrate();
					vibrationMade = true;
				}
				if(!MessageBar.messagesQ.Contains(Globals.SERVICE_CAR_WARNING_MSG))
					MessageBar.messagesQ.Enqueue(Globals.SERVICE_CAR_WARNING_MSG);
			//	eventWarningLabel.text = "Service Car is Coming from the west";
				
			}
			
			if(eventsWarningNames[index] == "thief"){
				//	vibrationMade = false;
				if(!vibrationMade && Globals.vibrationEnabled == true){
					Handheld.Vibrate();
					vibrationMade = true;
				}
				if(!MessageBar.messagesQ.Contains(Globals.THIEF_WARNING_MSG))
					MessageBar.messagesQ.Enqueue(Globals.THIEF_WARNING_MSG);
				//eventWarningLabel.text = "A Thief is coming from the west";
				
			}
			
			if(eventsWarningNames[index] == "police"){
				if(!vibrationMade && Globals.vibrationEnabled == true){
					Handheld.Vibrate();
					vibrationMade = true;
				}
				if(!MessageBar.messagesQ.Contains(Globals.POLICE_WARNING_MSG))
					MessageBar.messagesQ.Enqueue(Globals.POLICE_WARNING_MSG);
			//	eventWarningLabel.text = "help the police to catch him";
				
			}
			
			if(eventsWarningNames[index] == "caravan"){
				if(!vibrationMade && Globals.vibrationEnabled == true){
					Handheld.Vibrate();
					vibrationMade = true;
				}
				if(!MessageBar.messagesQ.Contains(Globals.CARAVAN_WARNING_MSG))
					MessageBar.messagesQ.Enqueue(Globals.CARAVAN_WARNING_MSG);
			//	eventWarningLabel.text = "Caravan is Coming";
			}
			
			if(eventsWarningNames[index] == "taxi"){
				if(!vibrationMade && Globals.vibrationEnabled == true){
					Handheld.Vibrate();
					vibrationMade = true;
				}
				
				if(!MessageBar.messagesQ.Contains(Globals.TAXI_WARNING_MSG))
					MessageBar.messagesQ.Enqueue(Globals.TAXI_WARNING_MSG);
				//eventWarningLabel.text = "Taxi is coming from the north";
			}
			
			if(eventsWarningNames[index] == "accident"){
				//closeButtonGo.SetActive(true);
				
				eventWarningLabelGo.SetActive(true);
			//	vibrationMade = false;
				if(!vibrationMade && Globals.vibrationEnabled == true){
					Handheld.Vibrate();
					vibrationMade = true;
				}
				eventWarningLabel.text = "Accident .. Ya sater ya rab";
				//eventsSpriteGo.SetActive(true);
				//eventsSprite.spriteName = "ambulance1";
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
		
		
		if(score >= currentLevel.MinScore){
			eventWarningLabel.text =  "Congratulations !! ";
			
			totalScoreLabel.text = "Total Score:";
			totalScoreVarLabel.text = score+"";
			
			
			GameObject [] gos = GameObject.FindGameObjectsWithTag("vehicle");
			for(int i = 0; i < gos.Length; i++){
				gos[i].SetActive(false);
			}
			
			gos = GameObject.FindGameObjectsWithTag("human");
			for(int i = 0; i < gos.Length; i++){
				gos[i].SetActive(false);
			}
			
			gos = GameObject.FindGameObjectsWithTag("Finish");
			for(int i = 0; i < gos.Length; i++){
				gos[i].SetActive(false);
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
			
			gos = GameObject.FindGameObjectsWithTag("human");
			for(int i = 0; i < gos.Length; i++){
				gos[i].SetActive(false);
			}
			
			gos = GameObject.FindGameObjectsWithTag("Finish");
			for(int i = 0; i < gos.Length; i++){
				gos[i].SetActive(false);
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
			
			gos = GameObject.FindGameObjectsWithTag("human");
			for(int i = 0; i < gos.Length; i++){
				gos[i].SetActive(false);
			}
			
			gos = GameObject.FindGameObjectsWithTag("Finish");
			for(int i = 0; i < gos.Length; i++){
				gos[i].SetActive(false);
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
