using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bus : MonoBehaviour {

	private const int BUS_HAPPEN_NUMBER = 3;

	public static List<int> busTimeSlots;
	
	
	public static List<int> busStopTimeSlots;
 	
	public static void InitInstances(){
		busTimeSlots = new List<int>();
		busStopTimeSlots = new List<int>();
	}
	
	public static void SetBusRandomTime(int timeBetweenEvents){
				
		int timeValue = 0;
		for (int i = 0 ; i<BUS_HAPPEN_NUMBER; i++){
			timeValue = Random.Range(100, 140);				//*********** I should make an enum to each level
			if(!busTimeSlots.Contains(timeValue)){
				busTimeSlots.Add(timeValue - timeBetweenEvents);
				GameMaster.eventsWarningTimes.Add(timeValue - timeBetweenEvents+ 3);
				GameMaster.eventsWarningNames.Add("b");
			}	
		}	
		
	}
	
	
	
	public static bool InsideBusTimeSlotsList(float gameTime){
		bool found = false;
		int i=0;
		Debug.Log("bustimeSlots.count "+ busTimeSlots.Count);
		while(!found && i < busTimeSlots.Count){
			if(busTimeSlots [i] == gameTime)
				found = true;
			i++;
		}
		return found;
	}
	
	public static void GenerateBus(int pos,GameObject busPrefab, List<GamePath> Paths){	
		
			
			if(busPrefab != null){
				GameObject vehicle;
					vehicle = Instantiate(busPrefab, Paths[pos].GenerationPointPosition ,Quaternion.identity) as GameObject;
					Paths[pos].PathStreets[0].VehiclesNumber ++;
					//vehicle.name = "Street # "+Paths[pos].PathStreets[0].ID + " # " + Paths[pos].PathStreets[0].VehiclesNumber;
					vehicle.name = "Street # "+Paths[pos].PathStreets[0].ID + " Car number " + GameMaster.vehicilesCounter;
					
					//	public Vehicle(VehicleType type,float speed,float size, Direction curDir, Street curStreet, Street nextStreet, Path path)
					vehicle.GetComponent<VehicleController>().myVehicle = new Vehicle(	VehicleType.Bus, 
																						18.0f, 
																						MathsCalculatios.getVehicleLargeSize(vehicle), 
																						Paths[pos].PathStreets[0].StreetLight.Type, 
																						Paths[pos].PathStreets[0], 
																						Paths[pos].PathStreets[1], 
																						0,
																						Paths[pos]);
				
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
