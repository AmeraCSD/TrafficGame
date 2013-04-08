using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Caravan : MonoBehaviour {

	private const int CARAVAN_HAPPEN_NUMBER = 1;
	
	public static List<int> caravanTimeSlots;
	
	public static void InitInstances(){
		caravanTimeSlots = new List<int>();
	}
	
	public  static void SetCaravanRandomTime(int timeBetweenEvents){
				
		int timeValue = 0;
		for (int i = 0 ; i<CARAVAN_HAPPEN_NUMBER; i++){
			timeValue = Random.Range(100, 110);				//*********** I should make an enum to each level
			if(!caravanTimeSlots.Contains(timeValue)){
				caravanTimeSlots.Add(timeValue - timeBetweenEvents);
				GameMaster.eventsWarningTimes.Add(timeValue -  timeBetweenEvents+ 3);
				GameMaster.eventsWarningNames.Add("c");
			}	
		}	
		
	}
	
	public static bool InsideCaravanTimeSlotsList(float gameTime){
		bool found = false;
		int i=0;
		Debug.Log("timeSlots.count "+ caravanTimeSlots.Count);
		while(!found && i < caravanTimeSlots.Count){
			if(caravanTimeSlots [i] == gameTime)
				found = true;
			i++;
		}
		return found;
	}
	
	public static void GenerateCaravan(int pos, GameObject caravanPrefab, List<GamePath> Paths){	
		
			
			if(caravanPrefab != null){
				
				GameObject vehicle;
					vehicle = Instantiate(caravanPrefab, Paths[pos].GenerationPointPosition ,Quaternion.identity) as GameObject;
					Paths[pos].PathStreets[0].VehiclesNumber ++;
					//vehicle.name = "Street # "+Paths[pos].PathStreets[0].ID + " # " + Paths[pos].PathStreets[0].VehiclesNumber;
					vehicle.name = "Street # "+Paths[pos].PathStreets[0].ID + " Car number " + GameMaster.vehicilesCounter;
					
					//	public Vehicle(VehicleType type,float speed,float size, Direction curDir, Street curStreet, Street nextStreet, Path path)
					vehicle.GetComponent<VehicleController>().myVehicle = new Vehicle(	VehicleType.Caravan, 
																						13.0f, 
																						MathsCalculatios.getVehicleLargeSize(vehicle), 
																						Paths[pos].PathStreets[0].StreetLight.Type, 
																						Paths[pos].PathStreets[0], 
																						Paths[pos].PathStreets[1], 
																						0,
																						Paths[pos]);
				
			}
			
	
	}
	
	
	
}
