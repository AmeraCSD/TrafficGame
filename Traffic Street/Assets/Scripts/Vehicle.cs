using UnityEngine;
using System.Collections;

/*
 This class is the base class for all Vehicles

*/

public class Vehicle  {
	private VehicleType _type;			//the vehicle type
	private float _speed;				//the vehicle speed
	private float _size;				//howlong the vehicle is

	//***********************************************************Don't forget the collider*******************
	
	private Direction _currentDirection;	//the current direction of moving the vehicle
	private Street _currentStreet;
	
	
	//attributes to specialize the events
	private bool _stoppable;			//for thief
	
	//the constructor
	
	public Vehicle(VehicleType type,float speed,float size, Direction curDir, Street curStreet){
		_type = type;
		_speed = speed;
		_size = size;
		_currentDirection = curDir;
		_currentStreet = curStreet;
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
	
	public Direction CurrentDirection{
		get{return _currentDirection;}
		set{_currentDirection = value;}
	}
	
	public Street CurrentStreet{
		get{return _currentStreet;}
		set{_currentStreet = value;}
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
