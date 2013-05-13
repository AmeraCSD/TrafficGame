using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
 This class is the base class for the 4 traffic lights

*/
public class TrafficLight   : MonoBehaviour{
	
	private StreetDirection _type;
	private GameObject _light;
	private bool _stopped;
	private bool _onHold;
	private List<Street> _attachedStreets;
	
	
	public TrafficLight(StreetDirection n, GameObject light, bool stopped){
		_type = n;
		_light = light;
		_stopped = stopped; 
		_onHold = false;
	}
	
	public StreetDirection Type{
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
	
	public List<Street> AttachedStreets{
		get{return _attachedStreets;}
		set{_attachedStreets = value;}
	}
	
}

public enum StreetDirection{
	Down,
	Up,
	Right,
	Left
}
