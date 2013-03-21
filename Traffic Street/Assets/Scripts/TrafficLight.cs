using UnityEngine;
using System.Collections;

/*
 This class is the base class for the 4 traffic lights

*/
public class TrafficLight   : MonoBehaviour{
	
	private Direction _type;
	private GameObject _light;
	private bool _stopped;
	
	public const float MIN_VEHICLE_SPEED = 8.0f;		//this should be in the global class 
	
	public TrafficLight(Direction n, GameObject light, bool stopped){
		_type = n;
		_light = light;
		_stopped = stopped; 		
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
	
	public void ChangeState(float streetWidth){
		Debug.Log("STOPPEDDDDD is = " + _stopped);
		if(_stopped){
			Debug.Log("HEREEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
			_light.renderer.material.color = Color.yellow;
			StartCoroutine(WaitForMe());
			_stopped = false;
			_light.renderer.material.color = Color.green;
		}
		else{
			_stopped = true;
			_light.renderer.material.color = Color.red;
		}
		//Debug.Log("****"+ stoppedUD);
	}
	
	IEnumerator WaitForMe(){
		//yield return new WaitForSeconds( streetWidth / MIN_VEHICLE_SPEED *Time.deltaTime);
		yield return new WaitForSeconds(3);
	}
}

public enum Direction{
	Down,
	Up,
	Right,
	Left
}
