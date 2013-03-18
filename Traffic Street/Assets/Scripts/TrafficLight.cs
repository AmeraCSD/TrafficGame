using UnityEngine;
using System.Collections;

/*
 This class is the base class for the 4 traffic lights

*/
public class TrafficLight  {
	
	private LightPositionType _type;
	private GameObject _light;
	private bool _stopped;
	
	public TrafficLight(LightPositionType n, GameObject light, bool stopped){
		_type = n;
		_light = light;
		_stopped = stopped; 		
	}
	
	public LightPositionType Type{
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
	
	public void ChangeState(){
		if(_stopped){
			_stopped = false;
			_light.renderer.material.color = Color.green;
		}
		else{
			_stopped = true;
			_light.renderer.material.color = Color.red;
		}
		//Debug.Log("****"+ stoppedUD);
	}
}

public enum LightPositionType{
	Down,
	Up,
	Right,
	Left
}
