using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MathsCalculatios  {
	
	public static float CalculateAcclerationByNewtonFormula(float initialSpeed, float finalSpeed, float distance){
		if(distance == 0 || initialSpeed==finalSpeed){
			return 0;
		}
		float time = 2 * (distance-2) / (finalSpeed + initialSpeed);
		
		float accleration = (finalSpeed - initialSpeed)/time;
		
		
		return accleration* Time.deltaTime;
		
	}
	
	public static float GetDistanceBetweenVehicleAndOtherPosition(Vector3 vehiclePos, float pos, StreetDirection direction) {
		if(direction == StreetDirection.Right || direction == StreetDirection.Left){
			return Mathf.Abs(pos - vehiclePos.x);
		}
		
		else {
			return Mathf.Abs(pos - vehiclePos.z);
		}
	}
	
	public static float GetDistanceBetweenVehicleAndOtherPosition(Vector3 vehiclePos, Vector3 pos, StreetDirection direction) {
		if(direction == StreetDirection.Right || direction == StreetDirection.Left){
			return Mathf.Abs(pos.x - vehiclePos.x);
		}
		
		else {
			return Mathf.Abs(pos.z - vehiclePos.z);
		}
	}
	/*
	public static int CheckStoppingPosition(StreetDirection direction, Vector3 vehiclePos, float stopPos){
		if(direction == StreetDirection.Right && vehiclePos.x > stopPos - 15) 
			return 1;
		
		else if(direction == StreetDirection.Left && vehiclePos.x < stopPos + 15) 
			return 2;
		
		else if(direction == StreetDirection.Down && vehiclePos.z < stopPos + 15) 
			return 3;
		
		else if(direction == StreetDirection.Up && vehiclePos.z > stopPos - 15) 
			return 4;
		
		else 
			return -1;
	
	}
	*/
	
	public static bool CheckStoppingPosition(StreetDirection direction, Vector3 vehiclePos, float stopPos){
		if(direction == StreetDirection.Right && vehiclePos.x > stopPos - 15) 
			return true;
		
		else if(direction == StreetDirection.Left && vehiclePos.x < stopPos + 15) 
			return true;
		
		else if(direction == StreetDirection.Down && vehiclePos.z < stopPos + 15) 
			return true;
		
		else if(direction == StreetDirection.Up && vehiclePos.z > stopPos - 15) 
			return true;
		
		else 
			return false;
	
	}
	
	public static bool EndAccelerationOnArrival(StreetDirection direction, Vector3 vehiclePos, float stopPos){
		if(direction == StreetDirection.Right && vehiclePos.x > stopPos ) 
			return true;
		
		else if(direction == StreetDirection.Left && vehiclePos.x < stopPos) 
			return true;
		
		else if(direction == StreetDirection.Down && vehiclePos.z < stopPos ) 
			return true;
		
		else if(direction == StreetDirection.Up && vehiclePos.z > stopPos) 
			return true;
		
		else 
			return false;
	
	}
	
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
	
	public static bool CompareTwoPositionsWRTDirections(StreetDirection direction, Vector3 firstPos, Vector3 secondPos, float offset){
		if(direction == StreetDirection.Left){
			if(firstPos.x < secondPos.x - offset){
				return true;
			}
		}
		if(direction == StreetDirection.Right){
			if(firstPos.x > secondPos.x + offset)
				return true;
		}
		if(direction == StreetDirection.Up ){
			if(firstPos.z > secondPos.z + offset){
				return true;
			}
		}
		if(direction == StreetDirection.Down ){
			if(firstPos.z < secondPos.z - offset)
				return true;
		}
		
		return false;
			
	}
	
	public static bool CompareTwoPositionsWRTDirections(StreetDirection direction, Vector3 firstPos, float secondPos, float offset){
		if(direction == StreetDirection.Left){
			if(firstPos.x < secondPos - offset){
				return true;
			}
		}
		if(direction == StreetDirection.Right){
			if(firstPos.x > secondPos + offset)
				return true;
		}
		if(direction == StreetDirection.Up ){
			if(firstPos.z > secondPos + offset){
				return true;
			}
		}
		if(direction == StreetDirection.Down ){
			if(firstPos.z < secondPos - offset)
				return true;
		}
		
		return false;
			
	}
	
	public static bool CheckMyEndPosition(Transform transform, StreetDirection direction, Vector3 endPosition){
		
		if(CompareTwoPositionsWRTDirections(direction, transform.position, endPosition, 3)){
			return true;
		}
		return false;
	}
	
	public static bool IsLeavingTheStreet(Transform transform, StreetDirection direction,Vector3 endPosition, Street street){
		
		if(CompareTwoPositionsWRTDirections(direction, transform.position, endPosition, 0)){
			return true;
		}
		return false;
	}
	
	public static bool IsLeavingTheStreet_Rotate(GameObject [] corners, Transform transform, StreetDirection direction,Vector3 endPosition, Street street, StreetDirection nextDirection, VehicleController vehScript){
		Vector3 obPos = transform.position;
		
		if(CompareTwoPositionsWRTDirections(direction, transform.position, endPosition, -5)){
			if(vehScript.rotateAroundPosition == Vector3.zero){
		//		vehScript.speed = 10;
		//		vehScript.haveToReduceMySpeed = true;
				vehScript.rotateAroundPosition = MathsCalculatios.GetNearestCorner(corners, transform.position);
			} 
			
			return true;
		}
		
		else {
			vehScript.rotateAroundPosition = Vector3.zero;
		}
		return false;
	}
	
	public static bool HasFinishedRotation(Vector3 transformForward, bool rotateNow, StreetDirection direction, StreetDirection nextDirection, VehicleController vehScript){
		//Debug.Log("direc is >>  " + direction);
		if(direction != nextDirection){
			
//			Debug.Log("transformForward ... " + -1*transformForward.x );
			if(direction == StreetDirection.Left && -1*transformForward.x > -0.01 && rotateNow && nextDirection == StreetDirection.Up){
				vehScript.gameObject.transform.forward = -1*Vector3.forward;
				vehScript.speed = vehScript.myVehicle.Speed;
				return true;
			}
			
			if(direction == StreetDirection.Right && -1*transformForward.x < 0.01 && rotateNow && nextDirection == StreetDirection.Up){
				vehScript.gameObject.transform.forward = -1*Vector3.forward;
				vehScript.speed = vehScript.myVehicle.Speed;
//				Debug.Log("safwattttttttttttt");
				return true;
			}
			
			if(direction == StreetDirection.Up && -1*transformForward.z < 0.01 && rotateNow && nextDirection == StreetDirection.Left){
				vehScript.gameObject.transform.forward = Vector3.right;
				vehScript.speed = vehScript.myVehicle.Speed;
				return true;
			}
			
			if(direction == StreetDirection.Down && -1*transformForward.z > -0.01 && rotateNow && nextDirection == StreetDirection.Left){
				vehScript.gameObject.transform.forward = Vector3.right;
				vehScript.speed = vehScript.myVehicle.Speed;
				return true;
			}
			///////////////////////////////////////////////////
			
			
			if(direction == StreetDirection.Left &&  -1*transformForward.x > -0.01 && rotateNow && nextDirection == StreetDirection.Down){
				vehScript.gameObject.transform.forward = Vector3.forward;
				vehScript.speed = vehScript.myVehicle.Speed;
				return true;
			}
			
			if(direction == StreetDirection.Right &&  -1*transformForward.x < 0.01 && rotateNow && nextDirection == StreetDirection.Down){
				vehScript.gameObject.transform.forward = Vector3.forward;
				vehScript.speed = vehScript.myVehicle.Speed;
				return true;
			}
			
			if(direction == StreetDirection.Down && -1*transformForward.z > -0.01 && rotateNow && nextDirection == StreetDirection.Right){
				vehScript.gameObject.transform.forward = -1*Vector3.right;
				vehScript.speed = vehScript.myVehicle.Speed;
				return true;
			}
			
			if(direction == StreetDirection.Up && -1*transformForward.z < 0.01 && rotateNow && nextDirection == StreetDirection.Right){
				vehScript.gameObject.transform.forward = -1*Vector3.right;
				vehScript.speed = vehScript.myVehicle.Speed;
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
		return corners[minIndex].transform.position;
	}
	
	public static bool HaveToAccelerate(Transform transform, StreetDirection direction,Vector3 endPosition, Street street, VehicleController vehScript){
		float rate = 2;
		
		if(CompareTwoPositionsWRTDirections(direction, transform.position, endPosition, -15)){
		//	if(!vehScript._light.Stopped && vehScript.speed > 0){
		//		vehScript.speed -= rate ;
		//	}
			if(!vehScript._light.Stopped)
				vehScript.speed = 18 ;
			vehScript.haveToReduceMySpeed = true;
			return true;
		}
		
		return false;
	}
	
	
	
}