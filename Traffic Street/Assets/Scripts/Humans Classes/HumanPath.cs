using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanPath : MonoBehaviour {
	
	private Vector3 _generationPos;
	private string _walkAnimationName;
	private string _passAnimationName;
	private List<Street> _toBePassedStreets;
	private StreetDirection _directionAxis;
	private float _walkEndPos;
	private float _passEndPos;
	private bool _isLocked;
	
	public HumanPath(Vector3 generationPos, string walkAnimationName, string passAnimationName, List<Street> toBePassedStreets, StreetDirection directionAxis, float walkEndPos, float passEndPos, bool locked){
		_generationPos = generationPos;
		_walkAnimationName = walkAnimationName;
		_passAnimationName = passAnimationName;
		_toBePassedStreets = toBePassedStreets;
		_directionAxis = directionAxis;
		_walkEndPos = walkEndPos;
		_passEndPos = passEndPos;
		_isLocked = locked;
	}
	
	public Vector3 GenerationPosition{
		get{return _generationPos;}
		set{_generationPos = value;}
	}
	
	public string WalkAnimationName{
		get{return _walkAnimationName;}
		set{_walkAnimationName = value;}
	}
	
	public string PassAnimationName{
		get{return _passAnimationName;}
		set{_passAnimationName = value;}
	}
	
	public List<Street> ToBePassedStreets{
		get{return _toBePassedStreets;}
		set{_toBePassedStreets = value;}
	}
	
	public StreetDirection DirectionAxis{
		get{return _directionAxis;}
		set{_directionAxis = value;}
	}
	
	public float WalkEndPos{
		get{return _walkEndPos;}
		set{_walkEndPos = value;}
	}
	
	public float PassEndPos{
		get{return _passEndPos;}
		set{_passEndPos = value;}
	}
	
	public bool IsLocked{
		get{return _isLocked;}
		set{_isLocked = value;}
	}
}
