using UnityEngine;
using System.Collections;

public class MathsCalculatios  {

	public static float getVehicleLargeSize(GameObject v){
		
		if(v.transform.localScale.x > v.transform.localScale.z)
			return v.transform.localScale.x;
		else
			return v.transform.localScale.z;
	}
	
	public static bool CheckMyEndPosition(Transform transform, Direction direction, Direction nextDirection, Vector3 endPosition){
		//For Lefts
		if(direction == Direction.Left && (nextDirection == null || nextDirection == Direction.Left)){
			if(transform.position.x < endPosition.x)
				return true;
		}
		
		if(direction == Direction.Left && nextDirection == Direction.Down){
			if(transform.position.x < endPosition.x && transform.position.z < endPosition.z)
				return true;
		}
		
		if(direction == Direction.Left && nextDirection == Direction.Up){
			if(transform.position.x < endPosition.x && transform.position.z > endPosition.z)
				return true;
		}
		
		//For Rights
		if(direction == Direction.Right && (nextDirection == null || nextDirection == Direction.Right)){
			if(transform.position.x > endPosition.x)
				return true;
		}
		
		if(direction == Direction.Right &&  nextDirection == Direction.Down){
			if(transform.position.x > endPosition.x && transform.position.z < endPosition.z)
				return true;
		}
		
		if(direction == Direction.Right && nextDirection == Direction.Up){
			if(transform.position.x > endPosition.x && transform.position.z > endPosition.z)
				return true;
		}
		
		//For Ups
		if(direction == Direction.Up && (nextDirection == null || nextDirection == Direction.Up)){
			if(transform.position.z > endPosition.z)
				return true;
		}
		
		if(direction == Direction.Up && nextDirection == Direction.Right){
			if(transform.position.z > endPosition.z && transform.position.x > endPosition.x)
				return true;
		}
		
		if(direction == Direction.Up && nextDirection == Direction.Left){
			if(transform.position.z > endPosition.z && transform.position.x < endPosition.x)
				return true;
		}
		
		//For Downs
		if(direction == Direction.Down && (nextDirection == null || nextDirection == Direction.Down)){
			if(transform.position.z < endPosition.z)
				return true;
		}
		
		if(direction == Direction.Down && nextDirection == Direction.Right ){
			if(transform.position.z < endPosition.z && transform.position.x > endPosition.x)
				return true;
		}
		
		if(direction == Direction.Down && nextDirection == Direction.Left ){
			if(transform.position.z < endPosition.z && transform.position.x < endPosition.x)
				return true;
		}
		return false;
	}
	
}