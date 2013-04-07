using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GamePath : MonoBehaviour {
	private List<Street> _pathStreets;
	private Vector3 _generationPointPosition;
	private Vector3 _endPosition;			//this is the poisition that the cars leave the street in
	private bool _hasUniqueGenerationPoint;
	
	public GamePath(List<Street> streetsList, Vector3 generationPointPos, Vector3 endPointPos,bool hasUniqueGenerationPoint){
		_pathStreets = streetsList;
		_generationPointPosition = generationPointPos;
		_endPosition = endPointPos;
		_hasUniqueGenerationPoint = hasUniqueGenerationPoint;
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
	
	public bool HasUniqueGenerationPoint{
		get{return _hasUniqueGenerationPoint;}
		set{_hasUniqueGenerationPoint = value;}
	}
	
}
