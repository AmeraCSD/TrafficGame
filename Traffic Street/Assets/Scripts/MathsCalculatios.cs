using UnityEngine;
using System.Collections;

public class MathsCalculatios  {

	public static float getVehicleLargeSize(GameObject v){
		
		if(v.transform.localScale.x > v.transform.localScale.z)
			return v.transform.localScale.x;
		else
			return v.transform.localScale.z;
	}
	
	public static bool CheckMyEndPosition(Transform transform, StreetDirection direction, StreetDirection nextDirection, Vector3 endPosition){
		//For Lefts
		if(direction == StreetDirection.Left && (nextDirection == null || nextDirection == StreetDirection.Left)){
			if(transform.position.x < endPosition.x)
				return true;
		}
		
		if(direction == StreetDirection.Left && nextDirection == StreetDirection.Down){
			if(transform.position.x < endPosition.x && transform.position.z < endPosition.z)
				return true;
		}
		
		if(direction == StreetDirection.Left && nextDirection == StreetDirection.Up){
			if(transform.position.x < endPosition.x && transform.position.z > endPosition.z)
				return true;
		}
		
		//For Rights
		if(direction == StreetDirection.Right && (nextDirection == null || nextDirection == StreetDirection.Right)){
			if(transform.position.x > endPosition.x)
				return true;
		}
		
		if(direction == StreetDirection.Right &&  nextDirection == StreetDirection.Down){
			if(transform.position.x > endPosition.x && transform.position.z < endPosition.z)
				return true;
		}
		
		if(direction == StreetDirection.Right && nextDirection == StreetDirection.Up){
			if(transform.position.x > endPosition.x && transform.position.z > endPosition.z)
				return true;
		}
		
		//For Ups
		if(direction == StreetDirection.Up && (nextDirection == null || nextDirection == StreetDirection.Up)){
			if(transform.position.z > endPosition.z)
				return true;
		}
		
		if(direction == StreetDirection.Up && nextDirection == StreetDirection.Right){
			if(transform.position.z > endPosition.z && transform.position.x > endPosition.x)
				return true;
		}
		
		if(direction == StreetDirection.Up && nextDirection == StreetDirection.Left){
			if(transform.position.z > endPosition.z && transform.position.x < endPosition.x)
				return true;
		}
		
		//For Downs
		if(direction == StreetDirection.Down && (nextDirection == null || nextDirection == StreetDirection.Down)){
			if(transform.position.z < endPosition.z)
				return true;
		}
		
		if(direction == StreetDirection.Down && nextDirection == StreetDirection.Right ){
			if(transform.position.z < endPosition.z && transform.position.x > endPosition.x)
				return true;
		}
		
		if(direction == StreetDirection.Down && nextDirection == StreetDirection.Left ){
			if(transform.position.z < endPosition.z && transform.position.x < endPosition.x)
				return true;
		}
		return false;
	}
	
}