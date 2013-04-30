using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Map : MonoBehaviour {
	
	private List<GamePath> _paths;
	
	public Map(List<GamePath> thePaths){
		_paths = thePaths;
	}
	
	public List<GamePath> GamePaths{
		get{return _paths;}
		set{_paths = value;}
	}
	
}
