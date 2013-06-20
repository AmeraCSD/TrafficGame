using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bus : MonoBehaviour {

	public static List<float> busTimeSlots;
	
	
	//public static List<int> busStopTimeSlots;
 	
	public static void InitInstances(){
		busTimeSlots = new List<float>();
	//	busStopTimeSlots = new List<int>();
	}
	
	public  static void SetEventTime(List<float> eventTimes){
		
		for (int i = 0 ; i<eventTimes.Count; i++){
			
			busTimeSlots.Add(eventTimes[i]);
			GameMaster.eventsWarningTimes.Add(eventTimes[i]+5);
			GameMaster.eventsWarningNames.Add("bus");
		}	
		
	}
	
	
	public static bool InsideTimeSlotsList(float gameTime){
		bool found = false;
		int i=0;
		while(!found && i < busTimeSlots.Count){
			if(busTimeSlots [i] == gameTime)
				found = true;
			i++;
		}
		return found;
	}
	
	
	public static void GenerateVehicle(GameObject busPrefab, GamePath path){	
		if(busPrefab != null){
			GameObject vehicle;
			vehicle = Instantiate(busPrefab, path.GenerationPointPosition ,Quaternion.identity) as GameObject;
			path.PathStreets[0].VehiclesNumber ++;
			//vehicle.name = "Street # "+path.PathStreets[0].ID + " # " + path.PathStreets[0].VehiclesNumber;
			vehicle.name = "Street # "+path.PathStreets[0].ID + " Car number " + GameMaster.vehicilesCounter;
			//	public Vehicle(VehicleType type,float speed,float size, Direction curDir, Street curStreet, Street nextStreet, Path path)
			vehicle.GetComponent<VehicleController>().myVehicle = new Vehicle(	VehicleType.Bus, 
																				Globals.BUS_SPEED, 
																				MathsCalculatios.getVehicleLargeSize(vehicle), 
																				path.PathStreets[0].StreetLight.Type, 
																				path.PathStreets[0], 
																				path.PathStreets[1], 
																				0,
																				path, null);
		}
			
	}
	
	/*public static void stopInBusStation(GameMaster gameMasterScript, float speed){
		
		
		
		if((gameMasterScript.gameTime == gameMasterScript.busTimeSlots[0] - 3) || (gameMasterScript.gameTime == gameMasterScript.busTimeSlots[0] - 4)){
				speed = 0.0f;
				Debug.Log("hona ******************");
				haveToStop = true;
			}
			else if((gameMasterScript.gameTime == gameMasterScript.busTimeSlots[1] - 9) || (gameMasterScript.gameTime == gameMasterScript.busTimeSlots[1] - 10)){
				speed = 0.0f;
				Debug.Log("hona ******************");
				haveToStop = true;
			}
			else{
				speed = myVehicle.Speed;
				haveToStop = false;
			}
	}
	
	*/
	
}
