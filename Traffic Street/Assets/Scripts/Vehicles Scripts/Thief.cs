using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Thief : MonoBehaviour {

	
	public static List<float> thiefTimeSlots;
	
	public static void InitInstances(){
		thiefTimeSlots = new List<float>();
	}
	
	public  static void SetEventTime(List<float> eventTimes){
		
		for (int i = 0 ; i<eventTimes.Count; i++){
			
			thiefTimeSlots.Add(eventTimes[i]);
			GameMaster.eventsWarningTimes.Add(eventTimes[i]+5);
			GameMaster.eventsWarningNames.Add("thief");
		}	
		
	}
	
	public static bool InsideTimeSlotsList(float gameTime){
		bool found = false;
		int i=0;
		while(!found && i < thiefTimeSlots.Count){
			if(thiefTimeSlots [i] == gameTime)
				found = true;
			i++;
		}
		return found;
	}
	
	public static void GenerateVehicle(GameObject thiefPrefab, GamePath path){	
					
		if(thiefPrefab != null){
			GameObject vehicle;
			vehicle = Instantiate(thiefPrefab, path.GenerationPointPosition ,Quaternion.identity) as GameObject;
			path.PathStreets[0].VehiclesNumber ++;
			//vehicle.name = "Street # "+path.PathStreets[0].ID + " # " + path.PathStreets[0].VehiclesNumber;
			vehicle.name = "Street # "+path.PathStreets[0].ID + " Car number " + GameMaster.vehicilesCounter;
			//	public Vehicle(VehicleType type,float speed,float size, Direction curDir, Street curStreet, Street nextStreet, Path path)
			vehicle.GetComponent<VehicleController>().myVehicle = new Vehicle(	VehicleType.Thief, 
																				Globals.THIEF_SPEED, 
																				MathsCalculatios.getVehicleLargeSize(vehicle), 
																				path.PathStreets[0].StreetLight.Type, 
																				path.PathStreets[0], 
																				path.PathStreets[1], 
																				0,
																				path,null);
				
		}	
	
	}
	
	
	
	
}
