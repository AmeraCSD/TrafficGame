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
	private const float GAME_TIME = 50;			//should equal to 5 minutes
	
	private List<Path> Paths;
	private List<Street> Streets;
	private List<float> _timeSlots;
	private int vehiclesNumber;
	// These constants for the random generation of vehicles
	private const int VEHICLES_LEAST_NUMBER = 150;
	private const int VEHICLES_MOST_NUMBER = 200;
	
	public GameObject vehiclePrefab;				//this object should be initialized in unity with the VehiclePrefab
	
	public bool gameOver;
	
	
	
	void Awake(){
		//initializing the huds 
		gameTime = GAME_TIME;
	//	gameOver = false;
		score = 0;
		
		
	}
	
	// Use this for initialization
	void Start () {
		//initializing the vehicles needs
		//_generationPoints = new List<Vector3>();
		
		Streets = GameObject.FindGameObjectWithTag("master").GetComponent<StreetsGenerator>().getStreets();
		Paths = GameObject.FindGameObjectWithTag("master").GetComponent<StreetsGenerator>().getPaths();
		
		//InitAllGenerationPoints();
		_timeSlots = new List<float>();
		CalculateRandomTimeSlots();
		InvokeRepeating("CountTimeDown", 1.0f, 1.0f);
	}
	
		
	//This method initializes all the generation points for the streets in the map
	private void InitAllGenerationPoints(){
		for(int i=0; i<Streets.Count; i++){
			//if(Streets[i].GenerationPointPosition != Vector3.zero){
			//	_generationPoints.Add(Streets[i].GenerationPointPosition);
			//}
			
		}
	}
	
	//This method calculates random time intervals that the vehicles should be generated in
	private void CalculateRandomTimeSlots(){
		vehiclesNumber = Random.Range(VEHICLES_LEAST_NUMBER, VEHICLES_MOST_NUMBER);
		float timeRatio = 0.0f;
		for (int i = 0 ; i<vehiclesNumber; i++){
			timeRatio = Random.Range(0.0f, gameTime);
			if(!_timeSlots.Contains(timeRatio))
				_timeSlots.Add(timeRatio);
		}
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
		if(InsideTimeSlotsList())
			GenerateRandomVehicle();
	}
	
	
	private bool InsideTimeSlotsList(){
		bool found = false;
		int i=0;
		while(!found && i < _timeSlots.Count){
			if((int)_timeSlots [i] == (int)gameTime)
				found = true;
			i++;
		}
		return found;
	}
	
	//here we give the vehicle all of its properities
	private void GenerateRandomVehicle(){	
		//int pos = Random.Range(0, _generationPoints.Count);
		int pos = Random.Range(0, Paths.Count);
		
		Debug.Log("Paths Number ======= "+  Paths.Count);
		//if(Paths[pos] != Vector3.zero){
		if(vehiclePrefab != null){
		GameObject vehicle = Instantiate(vehiclePrefab, Paths[pos].GenerationPointPosition ,Quaternion.identity) as GameObject;
		Paths[pos].PathStreets[0].VehiclesNumber ++;
		vehicle.name = "____"+Paths[pos].PathStreets[0].StreetLight.Type.ToString() + " # " + Paths[pos].PathStreets[0].VehiclesNumber;
		
		//	public Vehicle(VehicleType type,float speed,float size, Direction curDir, Street curStreet, Street nextStreet, Path path)
		vehicle.GetComponent<VehicleController>().myVehicle = new Vehicle(	VehicleType.Normal, 
																			35.0f, 
																			getVehicleLargeSize(vehicle), 
																			Paths[pos].PathStreets[0].StreetLight.Type, 
																			Paths[pos].PathStreets[0], 
																			Paths[pos].PathStreets[1], 
																			0,
																			Paths[pos]);
		
		
		
		//Debug.Log("Hereeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee______"+ Paths[pos].PathStreets[0].StreetLight.Type);
		//}
		}
	}
	
	private float getVehicleLargeSize(GameObject v){
		
		if(v.transform.localScale.x > v.transform.localScale.z)
			return v.transform.localScale.x;
		else
			return v.transform.localScale.z;
	}
	
	
	
	
	
	// Update is called once per frame
	void Update () {
//		if(UD_queue.Count == 6 || DU_queue.Count == 6 || LR_queue.Count == 6 || RL_queue.Count == 6)
//			gameOver = true;
	}
	
	void OnGUI(){
		GUI.Label( new Rect(10, 10, 100, 35), "Time: "+ gameTime);
		GUI.Label(new Rect(10, 30, 100, 25), "Score : "+score.ToString());

	/*	if(gameOver || gameTime == 0){
		//	st.fontSize = 50;
			//Destroy( GameObject.Find("  Game Master"));
			GUI.Box(new Rect(Screen.width/4, Screen.height/4,  Screen.width/2 , Screen.height/2 ) , " "  );
			GUI.Label(new Rect(Screen.width/2 - 20 , Screen.height/2 - 12, 100, 25), "LOSER !!");
			string temp = score.ToString();
			GUI.Label(new Rect(Screen.width/2 - 20 , Screen.height/2+30-12 , 100, 25), "Score : "+ temp);
			
			GameObject [] gos = GameObject.FindGameObjectsWithTag("vehicle");
			for(int i = 0; i < gos.Length; i++){
				gos[i].SetActive(false);
			}
			
			CancelInvoke("CountTimeDown");
		//	gameObject.SetActive(false);
			
			//Application.Quit();
			
		}*/
	}
	
	
}
