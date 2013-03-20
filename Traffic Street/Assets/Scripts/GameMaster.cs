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
	private const float GAME_TIME = 10;			//should equal to 5 minutes
	
	
	private List<Street> Streets;
	private List<Vector3> _generationPoints;
	private List<float> _timeSlots;
	private int vehiclesNumber;
	// These constants for the random generation of vehicles
	private const int VEHICLES_LEAST_NUMBER = 8;
	private const int VEHICLES_MOST_NUMBER = 8;
	
	public GameObject vehiclePrefab;				//this object should be initialized in unity with the VehiclePrefab
	
//	public bool gameOver;
	
	
	
	void Awake(){
		//initializing the huds 
		gameTime = GAME_TIME;
	//	gameOver = false;
		score = 0;
		
		
	}
	
	// Use this for initialization
	void Start () {
		//initializing the vehicles needs
		_generationPoints = new List<Vector3>();
		Streets = GameObject.FindGameObjectWithTag("master").GetComponent<StreetsGenerator>().getStreets();
		InitAllGenerationPoints();
		_timeSlots = new List<float>();
		CalculateRandomTimeSlots();
		InvokeRepeating("CountTimeDown", 1.0f, 1.0f);
	}
	
	//This method initialized all the generation points for the streets in the map
	private void InitAllGenerationPoints(){
		for(int i=0; i<Streets.Count; i++){
			_generationPoints.Add(Streets[i].GenerationPointPosition);
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
			Debug.Log("time slotttt ... "+ timeRatio);
		}
	}	
	
	//This method is called in the Start() it counts down the game time each second and generates the vehicles in the random time calculated at the first of the game
	void CountTimeDown ()
	{
		Debug.Log("Game Time= "+gameTime);
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
		int pos = Random.Range(0, _generationPoints.Count);
		GameObject vehicle = Instantiate(vehiclePrefab, _generationPoints[pos] ,Quaternion.identity) as GameObject;
		Streets[pos].VehiclesNumber ++;
		vehicle.name = "____"+Streets[pos].StreetLight.Type.ToString() + " # " + Streets[pos].VehiclesNumber;
		
		vehicle.GetComponent<VehicleController>().myVehicle = new Vehicle(VehicleType.Normal, 20.0f, getVehicleLargeSize(vehicle), Streets[pos].StreetLight.Type, Streets[pos]);
		
		
		//Vehicle vehicleObj;
		
		//vehicleObj = vehicle.GetComponent<Vehicle>();
		//vehicleObj.Type = VehicleType.Normal;
		//vehicleObj.Speed = 50.0f;
		//vehicleObj.Size = 14;
		//vehicleObj.CurrentDirection = Streets[pos].StreetLight.Type;
		Debug.Log("Hereeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee______"+ Streets[pos].StreetLight.Type);
		//vehicleObj.CurrentStreet = Streets[pos];
		
		//vehicle.GetComponent<Vehicle>()= vehicleObj;
		
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

		
	}
	
	
}
