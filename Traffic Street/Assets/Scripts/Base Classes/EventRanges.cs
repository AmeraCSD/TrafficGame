using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EventRanges : MonoBehaviour {

	private List<Range> rangesList;
	
	public EventRanges(List<Range> theRangesList){
		rangesList = theRangesList;
	}
	
	public List<Range> RangesList{
		get{return rangesList;}
		set{rangesList = value;}
	} 
}
