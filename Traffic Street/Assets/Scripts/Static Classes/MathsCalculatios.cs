using UnityEngine;
using System.Collections;

public class MathsCalculatios  {
	
	public static Vector3 GetApproximatedPosition(Vector3 position){
		int xPos  ;
		xPos = Mathf.Abs((((int)(position.x * .1)) * 10))+5;
		if(position.x < 0){
			xPos *= -1;
		}
		
		int zPos  ;
		zPos = Mathf.Abs((((int)(position.z * .1)) * 10))+5;
		if(position.z < 0){
			zPos *= -1;
		}
		
		return new Vector3((float)xPos, 5, (float)zPos );
		
	}
	
	public static float getVehicleLargeSize(GameObject v){
		
		if(v.transform.localScale.x > v.transform.localScale.z)
			return v.transform.localScale.x;
		else
			return v.transform.localScale.z;
	}
	
	public static bool IsLeavingTheStreet2(Transform transform, StreetDirection direction, StreetDirection nextDirection, Vector3 endPosition, Street nextStreet){
		//For Lefts
		if(direction == StreetDirection.Left && (nextStreet == null || nextDirection == StreetDirection.Left)){
			if(transform.position.x < endPosition.x - 3){
				Debug.Log("left left");
				return true;
			}
		}
		
		if(direction == StreetDirection.Left && nextDirection == StreetDirection.Down){
			if(transform.position.x < endPosition.x - 3 && transform.position.z < endPosition.z - 3)
				return true;
		}
		
		if(direction == StreetDirection.Left && nextDirection == StreetDirection.Up){
			if(transform.position.x < endPosition.x - 3 && transform.position.z > endPosition.z + 3){
				Debug.Log("left up");
				return true;
			}
		}
		
		//For Rights
		if(direction == StreetDirection.Right && (nextStreet == null || nextDirection == StreetDirection.Right)){
			if(transform.position.x > endPosition.x + 3)
				return true;
		}
		
		if(direction == StreetDirection.Right &&  nextDirection == StreetDirection.Down){
			if(transform.position.x > endPosition.x + 3 && transform.position.z < endPosition.z - 3)
				return true;
		}
		
		if(direction == StreetDirection.Right && nextDirection == StreetDirection.Up){
			if(transform.position.x > endPosition.x + 3 && transform.position.z > endPosition.z + 3)
				return true;
		}
		
		//For Ups
		if(direction == StreetDirection.Up && (nextStreet == null || nextDirection == StreetDirection.Up)){
			Debug.Log(nextDirection);
			if(transform.position.z > endPosition.z + 3){
				Debug.Log("up up");
				return true;
			}
		}
		
		if(direction == StreetDirection.Up && nextDirection == StreetDirection.Right){
			if(transform.position.z > endPosition.z + 3 && transform.position.x > endPosition.x + 3)
				return true;
		}
		
		if(direction == StreetDirection.Up && nextDirection == StreetDirection.Left){
			if(transform.position.z > endPosition.z + 3 && transform.position.x < endPosition.x - 3){
				Debug.Log("up left");
				return true;
			}
		}
		
		//For Downs
		if(direction == StreetDirection.Down && (nextStreet == null || nextDirection == StreetDirection.Down)){
			if(transform.position.z < endPosition.z - 3)
				return true;
		}
		
		if(direction == StreetDirection.Down && nextDirection == StreetDirection.Right ){
			if(transform.position.z < endPosition.z - 3 && transform.position.x > endPosition.x + 3)
				return true;
		}
		
		if(direction == StreetDirection.Down && nextDirection == StreetDirection.Left ){
			if(transform.position.z < endPosition.z - 3 && transform.position.x < endPosition.x - 3)
				return true;
		}
		return false;
	}
	
	public static bool CheckMyEndPosition(Transform transform, StreetDirection direction, Vector3 endPosition){
		
		if(direction == StreetDirection.Left){
			if(transform.position.x < endPosition.x - 3){
				return true;
			}
		}
		if(direction == StreetDirection.Right){
			if(transform.position.x > endPosition.x + 3)
				return true;
		}
		if(direction == StreetDirection.Up ){
			if(transform.position.z > endPosition.z + 3){
				return true;
			}
		}
		if(direction == StreetDirection.Down ){
			if(transform.position.z < endPosition.z - 3)
				return true;
		}
		
		return false;
	}
	
	public static bool IsLeavingTheStreet(Transform transform, StreetDirection direction,Vector3 endPosition, Street street){
		
		if(direction == StreetDirection.Left){
			
		//	if(transform.position.x < endPosition.x + 1 && transform.position.x > endPosition.x ){
			if(transform.position.x < endPosition.x  ){
				//Debug.Log("it is leaving the street " + street.ID);
				return true;
				
			}
		}
		
	
		if(direction == StreetDirection.Right){
		//	if(transform.position.x > endPosition.x - 1 && transform.position.x < endPosition.x){
			if(transform.position.x > endPosition.x ){
				//Debug.Log("it is leaving the street " + street.ID);
				return true;
			}
		}
		
		
		if(direction == StreetDirection.Up){
			if(transform.position.z > endPosition.z ){
		//	if(transform.position.z > endPosition.z - 1 && transform.position.z < endPosition.z){
				//Debug.Log("it is leaving the street " + street.ID);
				return true;
			}
		}
		
		if(direction == StreetDirection.Down){
		//	if(transform.position.z < endPosition.z + 1 && transform.position.z > endPosition.z){
			if(transform.position.z < endPosition.z ){
				//Debug.Log("it is leaving the street " + street.ID);
				return true;
			}
		}
		return false;
	}
	
}