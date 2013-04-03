using UnityEngine;
using System.Collections;

public class Street {
	
	private int _id;
	private Vector3 _startPoint;
	private Vector3 _endPoint;
	private TrafficLight _myLight;
	private float _stopPosition;					//this is the maximum position that the cars can stop in when the traffic light is red
	private float _minDistanceToOpenTrafficLight;	//this is the minimum distance to change the light for preventing (collisions)
	private int _streetCapacity;
	
	//These variables are for the vehicles
	private Queue _queue;							//this queue would contain the vehicles that are found in this street at the current time
	private int _vehiclesNumber; 					//this is the number of all of the vehciles that would be generated arround the whole game time (should be generated in the game master class)
	private int _existedVehiclesNumberOnYellow;
	
	//constructor
	public Street(int id, Vector3 startPoint, Vector3 endPoint, TrafficLight trafficLight, float stopPosition, float minDistOpenLight, int streetCapacity){
		_id = id;
		_startPoint = startPoint;
		_endPoint = endPoint;
		_myLight = trafficLight;
		_stopPosition = stopPosition;
		_minDistanceToOpenTrafficLight = minDistOpenLight;
		_streetCapacity = streetCapacity;
		
		_queue = new Queue();
		_vehiclesNumber = 0;
		_existedVehiclesNumberOnYellow = 0;
		
	}
	
	
	//setters and getters
	
	public int ID{
		get{return _id;}
		set{_id = value;}
	}
	public Vector3 StartPoint{
		get{return _startPoint;}
		set{_startPoint = value;}
	}
	
	public Vector3 EndPoint{
		get{return _endPoint;}
		set{_endPoint = value;}
	}
	
	
	public TrafficLight StreetLight{
		get{return _myLight;}
		set{_myLight = value;}
	}
	
	public float StopPosition{
		get{return _stopPosition;}
		set{_stopPosition = value;}
	}
	
	
	
	public float MinimumDistanceToOpenTrafficLight{
		get {return _minDistanceToOpenTrafficLight;}
		set {_minDistanceToOpenTrafficLight = value;}
	}
	
	public int StreetCapacity{
		get{return _streetCapacity;}
		set{_streetCapacity = value;}
	}
	
	public Queue StrQueue{
		get{return _queue;}
		set{_queue = value;}
	}
	
	public int VehiclesNumber{
		get{return _vehiclesNumber;}
		set{_vehiclesNumber = value;}
	}
	
	public int GetVehiclesOnYellow(){
		if(_myLight.tLight != null){
			if(_myLight.tLight.renderer.material.color == Color.yellow){
				return _vehiclesNumber;	
			}
		}
	
		return 0;
	}
	
}
