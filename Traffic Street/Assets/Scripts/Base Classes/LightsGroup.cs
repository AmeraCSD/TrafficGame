using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightsGroup : MonoBehaviour {
	
	private List<TrafficLight> _groupOfLights;
	
	public LightsGroup (List<TrafficLight> groupOfLights){
		_groupOfLights = groupOfLights;
	}
	
	public List<TrafficLight> GroupOfLights{
		get{return _groupOfLights;}
		set{_groupOfLights = value;}
	}
	
}
