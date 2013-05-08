using UnityEngine;
using System.Collections;

public class Range : MonoBehaviour {

	private int fromNum;
	private int toNum;
	
	public Range(int theFrom, int theTo){
		fromNum = theFrom;
		toNum = theTo;
	}
	
	public int FromNum{
		get{return fromNum;}
		set{fromNum = value;}
	} 
	
	public int ToNum{
		get{return toNum;}
		set{toNum = value;}
	}
	
	
	
}
