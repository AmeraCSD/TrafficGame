using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Map : MonoBehaviour {
	
	private List<Street> _streets;
	private List<GamePath> _paths;
	private List<Vector3> _intersections;
	private List<TrafficLight> _lights;
	
	public Map(List<Street> theStreets, List<GamePath> thePaths, List<Vector3> theIntersections, List<TrafficLight> theLights){
		_streets = theStreets;
		_paths = thePaths;
		_intersections = theIntersections;
	}
	
	public List<GamePath> GamePaths{
		get{return _paths;}
		set{_paths = value;}
	}
	
	public List<Street> Streets{
		get{return _streets;}
		set{_streets = value;}
	}
	
	public List<Vector3> Intersections{
		get{return _intersections;}
		set{_intersections = value;}
	}
	
	public List<TrafficLight> Lights{
		get{return _lights;}
		set{_lights = value;}
	}
}
