using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path : MonoBehaviour {
	private List<Street> _pathStreets;
	private Vector3 _generationPointPosition;
	private Vector3 _endPosition;			//this is the poisition that the cars leave the street in
	
	public Path(List<Street> streetsList, Vector3 generationPointPos, Vector3 endPointPos){
		_pathStreets = streetsList;
		_generationPointPosition = generationPointPos;
		_endPosition = endPointPos;
	}
	
	public List<Street> PathStreets{
		get{return _pathStreets;}
		set{_pathStreets = value;}
	}
	
	public Vector3 EndPosition{
		get{return _endPosition;}
		set{_endPosition = value;}
	}
	
	public Vector3 GenerationPointPosition{
		get{return _generationPointPosition;}
		set{_generationPointPosition = value;}
	}
	
}
