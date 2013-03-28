using UnityEngine;
using System.Collections;

/*
 This class is the base class for the 4 traffic lights

*/
public class TrafficLight   : MonoBehaviour{
	
	private Direction _type;
	private GameObject _light;
	private bool _stopped;
	private bool _onHold;
	private bool _yellowAfterRed;
	private bool _yellowAfterGreen;
	
	
	public TrafficLight(Direction n, GameObject light, bool stopped){
		_type = n;
		_light = light;
		_stopped = stopped; 
		_onHold = false;
		_yellowAfterRed = false;
		_yellowAfterGreen = false;
	}
	
	public Direction Type{
		get{return _type;}
		set{_type = value;}
	}
	
	public GameObject tLight{
		get{return _light;}
		set{_light = value;}
	}
	
	public bool Stopped{
		get{return _stopped;}
		set{_stopped = value;}
	}
	
	public bool OnHold{
		get{return _onHold;}
		set{_onHold = value;}
	}
	
	public bool YellowAfterRed{
		get{return _yellowAfterRed;}
		set{
			_yellowAfterRed = value;
			_stopped = true;
		}
	}
	
	public bool YellowAfterGreen{
		get{return _yellowAfterGreen;}
		set{
			_yellowAfterGreen = value;
			_stopped = false;
		}
	}
	
}

public enum Direction{
	Down,
	Up,
	Right,
	Left
}
