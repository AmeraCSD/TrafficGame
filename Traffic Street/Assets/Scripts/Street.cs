using UnityEngine;
using System.Collections;

public class Street {
	
	private Vector3 _myGenerationPoint;
	private TrafficLight _myLight;
	
	private Queue _queue;
	
	
	//constructor
	public Street(Vector3 go, TrafficLight l){
		_myGenerationPoint = go;
		_myLight = l;
	}
	
	//setters and getters
	public Vector3 GenerationPointPosition{
		get{return _myGenerationPoint;}
		set{_myGenerationPoint = value;}
	}
	
	public TrafficLight LightPosition{
		get{return _myLight;}
		set{_myLight = value;}
	}
	
	public Queue InitQueue{
		get{return _queue;}
		set{_queue = value;}
	}
	
	
	
}
