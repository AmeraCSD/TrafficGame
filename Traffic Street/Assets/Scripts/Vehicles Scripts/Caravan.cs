using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Caravan : MonoBehaviour {


	public static List<float> caravanTimeSlots;
	
	public static void InitInstances(){
		caravanTimeSlots = new List<float>();
	}
	
	public  static void SetEventTime(List<float> eventTimes){
		
		for (int i = 0 ; i<eventTimes.Count; i++){
			
			caravanTimeSlots.Add(eventTimes[i]);
			GameMaster.eventsWarningTimes.Add(eventTimes[i]+5);
			GameMaster.eventsWarningNames.Add("caravan");
		}	
		
	}
	
	public static bool InsideTimeSlotsList(float gameTime){
		bool found = false;
		int i=0;
		while(!found && i < caravanTimeSlots.Count){
			if(caravanTimeSlots [i] == gameTime)
				found = true;
			i++;
		}
		return found;
	}
	
	public static void GenerateVehicle(GameObject caravanPrefab, GamePath path){	
					
		if(caravanPrefab != null){
			GameObject vehicle;
			vehicle = Instantiate(caravanPrefab, GameObject.Find("CaravanSpawnPoint").transform.position ,Quaternion.identity) as GameObject;
			path.PathStreets[0].VehiclesNumber ++;
			//vehicle.name = "Street # "+path.PathStreets[0].ID + " # " + path.PathStreets[0].VehiclesNumber;
			vehicle.name = "Street # "+path.PathStreets[0].ID + " Car number " + GameMaster.vehicilesCounter;
			//	public Vehicle(VehicleType type,float speed,float size, Direction curDir, Street curStreet, Street nextStreet, Path path)
			vehicle.GetComponent<VehicleController>().myVehicle = new Vehicle(	VehicleType.Caravan, 
																				Globals.CARAVAN_SPEED, 
																				MathsCalculatios.getVehicleLargeSize(vehicle), 
																				path.PathStreets[0].StreetLight.Type, 
																				path.PathStreets[0], 
																				path.PathStreets[1], 
																				0,
																				path, null);
				
		}	
	
	}
	
	
}
