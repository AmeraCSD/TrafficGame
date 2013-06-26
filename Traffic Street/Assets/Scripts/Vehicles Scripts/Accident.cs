using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Accident : MonoBehaviour {
	
	public static List<float> accidentTimeSlots;
	
	public static void InitInstances(){
		accidentTimeSlots = new List<float>();
	}
	
	public  static void SetEventTime(List<float> eventTimes){
		for (int i = 0 ; i<eventTimes.Count; i++){
			
			accidentTimeSlots.Add(eventTimes[i]);
			GameMaster.eventsWarningTimes.Add(eventTimes[i]+5);
			GameMaster.eventsWarningNames.Add("accident");
		}	
		
	}
	
	public static bool InsideTimeSlotsList(float gameTime){
		bool found = false;
		int i=0;
		while(!found && i < accidentTimeSlots.Count){
			if(accidentTimeSlots [i] == gameTime)
				found = true;
			i++;
		}
		return found;
	}
	
	public static void GenerateVehicle(GameObject accidentPrefab, GamePath path){	
					
		if(accidentPrefab != null){
			GameObject vehicle;
			vehicle = Instantiate(accidentPrefab, path.GenerationPointPosition ,Quaternion.identity) as GameObject;
			path.PathStreets[0].VehiclesNumber ++;
			//vehicle.name = "Street # "+path.PathStreets[0].ID + " # " + path.PathStreets[0].VehiclesNumber;
			vehicle.name = "Street # "+path.PathStreets[0].ID + " Car number " + GameMaster.vehicilesCounter;
			//	public Vehicle(VehicleType type,float speed,float size, Direction curDir, Street curStreet, Street nextStreet, Path path)
			vehicle.GetComponent<VehicleController>().myVehicle = new Vehicle(	VehicleType.Accident, 
																				Globals.ACCIDENT_SPEED, 
																				MathsCalculatios.getVehicleLargeSize(vehicle), 
																				path.PathStreets[0].StreetLight.Type, 
																				path.PathStreets[0], 
																				path.PathStreets[1], 
																				0,
																				path,null);
				
		}	
	
	}
	
	
	
}
