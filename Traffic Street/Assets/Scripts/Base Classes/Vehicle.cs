using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/*
 This class is the base class for all Vehicles

*/

public class Vehicle  {
	
	private VehicleType _type;			//the vehicle type
	private float _speed;				//the vehicle speed
	private float _size;				//howlong the vehicle is

	//***********************************************************Don't forget the collider*******************
	
	private StreetDirection _currentDirection;	//the current direction of moving the vehicle
	private Street _currentStreet;
	private Street _nextStreet;
	private int _curStreetNumber;
	private GamePath _myPath;
	private AudioClip _horn;
	
	
	//attributes to specialize the events
	private bool _stoppable;			//for thief
	
	//the constructor
	
	public Vehicle(VehicleType type,float speed,float size, StreetDirection curDir, Street curStreet, Street nextStreet,int curStrNum, GamePath path, AudioClip theHorn){
		_type = type;
		_speed = speed;
		_size = size;
		_currentDirection = curDir;
		_currentStreet = curStreet;
		_nextStreet = nextStreet;
		_curStreetNumber = curStrNum;
		_myPath = path;
		_horn = theHorn;
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
	
	public StreetDirection CurrentDirection{
		get{return _currentDirection;}
		set{_currentDirection = value;}
	}
	
	public Street CurrentStreet{
		get{return _currentStreet;}
		set{_currentStreet = value;}
	} 
	
	public Street NextStreet{
		get{return _nextStreet;}
		set{_nextStreet = value;}
	} 
	
	public int CurrentStreetNumber{
		get{return _curStreetNumber;}
		set{_curStreetNumber = value;}
	}
	
	public GamePath MyPath{
		get{return _myPath;}
		set{_myPath = value;}
	} 
	
	public bool Stoppable{
		get{return _stoppable;}
		set{_stoppable = value;}
	}
	
	public AudioClip Horn{
		get{return _horn;}
		set{_horn = value;}
	} 
	
}

public enum VehicleType{
	Normal,
	Bus,
	Ambulance,
	Caravan,
	ServiceCar,
	Taxi,
	Thief
}
