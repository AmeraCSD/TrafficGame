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
			timeValue = Random.Range(130, 143);				//*********** I should make an enum to each level
			if(!busTimeSlots.Contains(timeValue)){
				busTimeSlots.Add(timeValue - (i * timeBetweenEvents));
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
	
	public static void GenerateBus(int pos,GameObject busPrefab, List<Path> Paths){	
		
			
			if(busPrefab != null){
				GameObject vehicle;
					vehicle = Instantiate(busPrefab, Paths[pos].GenerationPointPosition ,Quaternion.identity) as GameObject;
					Paths[pos].PathStreets[0].VehiclesNumber ++;
					//vehicle.name = "Street # "+Paths[pos].PathStreets[0].ID + " # " + Paths[pos].PathStreets[0].VehiclesNumber;
					vehicle.name = "Street # "+Paths[pos].PathStreets[0].ID + " Car number " + GameMaster.vehicilesCounter;
					
					//	public Vehicle(VehicleType type,float speed,float size, Direction curDir, Street curStreet, Street nextStreet, Path path)
					vehicle.GetComponent<VehicleController>().myVehicle = new Vehicle(	VehicleType.Bus, 
																						10.0f, 
																						MathsCalculatios.getVehicleLargeSize(vehicle), 
																						Paths[pos].PathStreets[0].StreetLight.Type, 
																						Paths[pos].PathStreets[0], 
																						Paths[pos].PathStreets[1], 
																						0,
																						Paths[pos]);
				
			}
			
	}
	
	
	
	
	
}
