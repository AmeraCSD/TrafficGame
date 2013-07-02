using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

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
	
	public static bool IsLeavingTheStreet2(Transform transform, StreetDirection direction, Vector3 endPosition, Street nextStreet, StreetDirection nextDirection){
		//For Lefts
		if(direction == StreetDirection.Left && (nextStreet == null || nextDirection == StreetDirection.Left)){
			if(transform.position.x < endPosition.x){
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
	
	public static bool IsLeavingTheStreet_Rotate(GameObject [] corners, Transform transform, StreetDirection direction,Vector3 endPosition, Street street, StreetDirection nextDirection, VehicleController vehScript){
		Vector3 obPos = transform.position;
		
		if(direction == StreetDirection.Left){
			
			if(transform.position.x < endPosition.x+5 ){
				if(vehScript.rotateAroundPosition == Vector3.zero){
					vehScript.speed = 10;
					vehScript.haveToReduceMySpeed = true;
					vehScript.rotateAroundPosition = MathsCalculatios.GetNearestCorner(corners, transform.position);
				} 
				
				
				//*******************************************************************
				return true;
				
			}
		}
		
		if(direction == StreetDirection.Down){
			
			if(transform.position.z < endPosition.z +5){
				if(vehScript.rotateAroundPosition == Vector3.zero){
					vehScript.speed = 10;
					vehScript.haveToReduceMySpeed = true;
					vehScript.rotateAroundPosition = MathsCalculatios.GetNearestCorner(corners, transform.position);
				} 
				
				return true;
			}
			
		}
		
		if(direction == StreetDirection.Right){
		//	if(transform.position.x > endPosition.x - 1 && transform.position.x < endPosition.x){
			if(transform.position.x > endPosition.x - 5){
				if(vehScript.rotateAroundPosition == Vector3.zero){
					vehScript.speed = 10;
					vehScript.haveToReduceMySpeed = true;
					vehScript.rotateAroundPosition = MathsCalculatios.GetNearestCorner(corners, transform.position);
				} 
				return true;
			}
		}
		
		
		if(direction == StreetDirection.Up){
			
			if(transform.position.z > endPosition.z - 5){
				if(vehScript.rotateAroundPosition == Vector3.zero){
					vehScript.speed = 10;
					vehScript.haveToReduceMySpeed = true;
					vehScript.rotateAroundPosition = MathsCalculatios.GetNearestCorner(corners, transform.position);
				} 
				return true;
			}
		}
		
		
		else {
			vehScript.rotateAroundPosition = Vector3.zero;
		}
		return false;
	}
	/*
	public static bool IsLeavingTheStreet_Rotate(GameObject [] corners, Transform transform, StreetDirection direction,Vector3 endPosition, Street street, StreetDirection nextDirection, AccidentVehicleController vehScript){
		Vector3 obPos = transform.position;
		
		if(direction == StreetDirection.Left){
			
			if(transform.position.x < endPosition.x+5 ){
				if(vehScript.rotateAroundPosition == Vector3.zero){
					vehScript.rotateAroundPosition = MathsCalculatios.GetNearestCorner(corners, transform.position);
				} 
				
				
				//*******************************************************************
				return true;
				
			}
		}
		
		if(direction == StreetDirection.Down){
			
			if(transform.position.z < endPosition.z +5){
				if(vehScript.rotateAroundPosition == Vector3.zero){
					vehScript.rotateAroundPosition = MathsCalculatios.GetNearestCorner(corners, transform.position);
				} 
				
				return true;
			}
			
		}
		
		if(direction == StreetDirection.Right){
		//	if(transform.position.x > endPosition.x - 1 && transform.position.x < endPosition.x){
			if(transform.position.x > endPosition.x - 5){
				if(vehScript.rotateAroundPosition == Vector3.zero){
					vehScript.rotateAroundPosition = MathsCalculatios.GetNearestCorner(corners, transform.position);
				} 
				return true;
			}
		}
		
		
		if(direction == StreetDirection.Up){
			
			if(transform.position.z > endPosition.z - 5){
				if(vehScript.rotateAroundPosition == Vector3.zero){
					vehScript.rotateAroundPosition = MathsCalculatios.GetNearestCorner(corners, transform.position);
				} 
				return true;
			}
		}
		
		
		else {
			vehScript.rotateAroundPosition = Vector3.zero;
		}
		return false;
	}
	*/
	public static bool HasFinishedRotation(Vector3 transformForward, bool rotateNow, StreetDirection direction, StreetDirection nextDirection, VehicleController vehScript){
		//Debug.Log("direc is >>  " + direction);
		if(direction != nextDirection){
			
//			Debug.Log("transformForward ... " + -1*transformForward.x );
			if(direction == StreetDirection.Left && -1*transformForward.x > -0.01 && rotateNow && nextDirection == StreetDirection.Up){
				vehScript.gameObject.transform.forward = -1*Vector3.forward;
				return true;
			}
			
			if(direction == StreetDirection.Right && -1*transformForward.x < 0.01 && rotateNow && nextDirection == StreetDirection.Up){
				vehScript.gameObject.transform.forward = -1*Vector3.forward;
//				Debug.Log("safwattttttttttttt");
				return true;
			}
			
			if(direction == StreetDirection.Up && -1*transformForward.z < 0.01 && rotateNow && nextDirection == StreetDirection.Left){
				vehScript.gameObject.transform.forward = Vector3.right;
				return true;
			}
			
			if(direction == StreetDirection.Down && -1*transformForward.z > -0.01 && rotateNow && nextDirection == StreetDirection.Left){
				vehScript.gameObject.transform.forward = Vector3.right;
				return true;
			}
			///////////////////////////////////////////////////
			
			
			if(direction == StreetDirection.Left &&  -1*transformForward.x > -0.01 && rotateNow && nextDirection == StreetDirection.Down){
				vehScript.gameObject.transform.forward = Vector3.forward;
				return true;
			}
			
			if(direction == StreetDirection.Right &&  -1*transformForward.x < 0.01 && rotateNow && nextDirection == StreetDirection.Down){
				vehScript.gameObject.transform.forward = Vector3.forward;
				return true;
			}
			
			if(direction == StreetDirection.Down && -1*transformForward.z > -0.01 && rotateNow && nextDirection == StreetDirection.Right){
				vehScript.gameObject.transform.forward = -1*Vector3.right;
				return true;
			}
			
			if(direction == StreetDirection.Up && -1*transformForward.z < 0.01 && rotateNow && nextDirection == StreetDirection.Right){
				vehScript.gameObject.transform.forward = -1*Vector3.right;
				return true;
			}
			else
				return false;
		}
		return true;
	}
	
	public static bool HasFinishedRotation(Vector3 transformForward, bool rotateNow, StreetDirection direction, StreetDirection nextDirection, AccidentVehicleController vehScript){
		//Debug.Log("direc is >>  " + direction);
		if(direction != nextDirection){
			
//			Debug.Log("transformForward ... " + -1*transformForward.x );
			if(direction == StreetDirection.Left && -1*transformForward.x > -0.01 && rotateNow && nextDirection == StreetDirection.Up){
				vehScript.gameObject.transform.forward = -1*Vector3.forward;
				return true;
			}
			
			if(direction == StreetDirection.Right && -1*transformForward.x < 0.01 && rotateNow && nextDirection == StreetDirection.Up){
				vehScript.gameObject.transform.forward = -1*Vector3.forward;
				Debug.Log("safwattttttttttttt");
				return true;
			}
			
			if(direction == StreetDirection.Up && -1*transformForward.z < 0.01 && rotateNow && nextDirection == StreetDirection.Left){
				vehScript.gameObject.transform.forward = Vector3.right;
				return true;
			}
			
			if(direction == StreetDirection.Down && -1*transformForward.z > -0.01 && rotateNow && nextDirection == StreetDirection.Left){
				vehScript.gameObject.transform.forward = Vector3.right;
				return true;
			}
			///////////////////////////////////////////////////
			
			
			if(direction == StreetDirection.Left &&  -1*transformForward.x > -0.01 && rotateNow && nextDirection == StreetDirection.Down){
				vehScript.gameObject.transform.forward = Vector3.forward;
				return true;
			}
			
			if(direction == StreetDirection.Right &&  -1*transformForward.x < 0.01 && rotateNow && nextDirection == StreetDirection.Down){
				vehScript.gameObject.transform.forward = Vector3.forward;
				return true;
			}
			
			if(direction == StreetDirection.Down && -1*transformForward.z > -0.01 && rotateNow && nextDirection == StreetDirection.Right){
				vehScript.gameObject.transform.forward = -1*Vector3.right;
				return true;
			}
			
			if(direction == StreetDirection.Up && -1*transformForward.z < 0.01 && rotateNow && nextDirection == StreetDirection.Right){
				vehScript.gameObject.transform.forward = -1*Vector3.right;
				return true;
			}
			else
				return false;
		}
		return true;
	}
	
	public static Vector3 GetNearestCorner(GameObject[] corners, Vector3 vehPosition){
		List<float> temp =  new List<float>();
		float tempValue;
		int minIndex = 0;
		for(int i = 0; i<corners.Length; i++){
			temp.Add(Vector3.Distance(vehPosition, corners[i].transform.position));
			
			if(Vector3.Distance(vehPosition, corners[i].transform.position) < temp[minIndex]){
				minIndex = i;
			}
		}
		
		//temp.Sort();
		return corners[minIndex].transform.position;
	}
	
	public static bool HaveToAccelerate(Transform transform, StreetDirection direction,Vector3 endPosition, Street street, VehicleController vehScript){
		//Debug.Log("amiraaaaaaaaaaa");
		float rate = (vehScript.myVehicle.Speed - 10)/1.5f;
		
		if(direction == StreetDirection.Left){
			
			if(transform.position.x < endPosition.x +15 ){
				if(vehScript.speed > 0){
					vehScript.speed -= rate;
				}
				vehScript.haveToReduceMySpeed = true;
				return true;
				
			}
		}
		
	
		if(direction == StreetDirection.Right){
			if(transform.position.x > endPosition.x -15){
				if(vehScript.speed > 0){
					vehScript.speed -= rate;
				}
				vehScript.haveToReduceMySpeed = true;
				return true;
			}
		}
		
		
		if(direction == StreetDirection.Up){
			if(transform.position.z > endPosition.z -15){
				if(vehScript.speed > 0){
					vehScript.speed -= rate;
				}
				vehScript.haveToReduceMySpeed = true;
				return true;
			}
		}
		
		if(direction == StreetDirection.Down){
			if(transform.position.z < endPosition.z +15){
				if(vehScript.speed > 0){
					vehScript.speed -= rate;
				}
				vehScript.haveToReduceMySpeed = true;
				return true;
			}
		}
		return false;
	}
	
	
	
}