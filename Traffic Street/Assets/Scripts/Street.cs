using UnityEngine;
using System.Collections;

public class Street {
	
	private Vector3 _myGenerationPoint;
	private TrafficLight _myLight;
	private float _stopPosition;				//this is the maximum position that the cars can stop in when the traffic light is red
	private float _streetEndPosition;			//this is the poisition that the cars leave the street in
	
	//These variables are for the vehicles
	private Queue _queue;					//this queue would contain the vehicles that are found in this street at the current time
	private int _vehiclesNumber; 			//this is the number of all of the vehciles that would be generated arround the whole game time (should be generated in the game master class)
	
	//constructor
	public Street(Vector3 generationPoint, TrafficLight trafficLight, float stopPosition, float endPosition){
		_myGenerationPoint = generationPoint;
		_myLight = trafficLight;
		_stopPosition = stopPosition;
		_streetEndPosition = endPosition;
		_queue = new Queue();
		_vehiclesNumber = 0;
		
	}
	
	//setters and getters
	public Vector3 GenerationPointPosition{
		get{return _myGenerationPoint;}
		set{_myGenerationPoint = value;}
	}
	
	public TrafficLight StreetLight{
		get{return _myLight;}
		set{_myLight = value;}
	}
	
	public float StopPosition{
		get{return _stopPosition;}
		set{_stopPosition = value;}
	}
	
	public float StreetEndPosition{
		get{return _streetEndPosition;}
		set{_streetEndPosition = value;}
	}
	
	public Queue StrQueue{
		get{return _queue;}
		set{_queue = value;}
	}
	
	public int VehiclesNumber{
		get{return _vehiclesNumber;}
		set{_vehiclesNumber = value;}
	}
	
	
	
}
