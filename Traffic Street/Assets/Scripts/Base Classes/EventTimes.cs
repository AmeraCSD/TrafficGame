using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EventTimes : MonoBehaviour {

	private List<float> _timesList;
	
	public EventTimes(List<float> times){
		_timesList = times;
	}
	
	public List<float> TimesList{
		get{return _timesList;}
		set{_timesList = value;}
	} 
}
