using UnityEngine;
using System.Collections;

/*
 This class is the base class for all Vehicles

*/

public class Vehicle  {
	private VehicleType _type;			//the vehicle type
	private float _speed;				//the vehicle speed
	private float _size;				//howlong the vehicle is
	private GameObject _vecPrefab;		//the object of the vehicle
	private string _currentDirection;	//the initialized direction of moving the vehicle
	
	
	//attributes to specialize the events
	private bool _stoppable;			//for thief
	
	//the constructor
	public Vehicle(VehicleType t,float sp,float sz, GameObject go, string curDir){
		_type = t;
		_speed = sp;
		_size = sz;
		_vecPrefab = go;
		_currentDirection = curDir;
	}
	
	//setters and getters
	public VehicleType Type{
		get{return _type;}
		set{_type = value;}
	}
	
	public float Speed{
		get{return _speed;}
		set{_speed = value;}
	}
	
	public float Size{
		get{return _size;}
		set{_size = value;}
	}
	
	public GameObject VecObject{
		get{return _vecPrefab;}
		set{_vecPrefab = value;}
	}
	
	public string CurrentDirection{
		get{return _currentDirection;}
		set{_currentDirection = value;}
	}
	
	public bool Stoppable{
		get{return _stoppable;}
		set{_stoppable = value;}
	}
	
}

public enum VehicleType{
	Normal,
	Bus,
	Ambulance,
	Caravan,
	Thief
}
