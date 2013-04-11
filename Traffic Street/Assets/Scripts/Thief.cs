using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Thief : MonoBehaviour {

	private const int THIEF_HAPPEN_NUMBER = 1;
	
	public static List<int> thiefTimeSlots;
	
	public static void InitInstances(){
		thiefTimeSlots = new List<int>();
	}
	
	public  static void SetThiefRandomTime(int timeBetweenEvents){
				
		int timeValue = 0;
		for (int i = 0 ; i<THIEF_HAPPEN_NUMBER; i++){
			timeValue = Random.Range(140, 145);				//*********** I should make an enum to each level
			if(!thiefTimeSlots.Contains(timeValue)){
				thiefTimeSlots.Add(timeValue - timeBetweenEvents);
				GameMaster.eventsWarningTimes.Add(timeValue -  timeBetweenEvents+ 3);
				GameMaster.eventsWarningNames.Add("t");
			}	
		}	
		
	}
	
	public static bool InsideThiefTimeSlotsList(float gameTime){
		bool found = false;
		int i=0;
		//Debug.Log("timeSlots.count "+ thiefTimeSlots.Count);
		while(!found && i < thiefTimeSlots.Count){
			if(thiefTimeSlots [i] == gameTime)
				found = true;
			i++;
		}
		return found;
	}
	
	public static void GenerateThief(int pos, GameObject thiefPrefab, List<GamePath> Paths){	
		
			
			if(thiefPrefab != null){
				
				GameObject vehicle;
					vehicle = Instantiate(thiefPrefab, Paths[pos].GenerationPointPosition ,Quaternion.identity) as GameObject;
					Paths[pos].PathStreets[0].VehiclesNumber ++;
					//vehicle.name = "Street # "+Paths[pos].PathStreets[0].ID + " # " + Paths[pos].PathStreets[0].VehiclesNumber;
					vehicle.name = "Street # "+Paths[pos].PathStreets[0].ID + " Car number " + GameMaster.vehicilesCounter;
					
					//	public Vehicle(VehicleType type,float speed,float size, Direction curDir, Street curStreet, Street nextStreet, Path path)
					vehicle.GetComponent<VehicleController>().myVehicle = new Vehicle(	VehicleType.Thief, 
																						32.0f, 
																						MathsCalculatios.getVehicleLargeSize(vehicle), 
																						Paths[pos].PathStreets[0].StreetLight.Type, 
																						Paths[pos].PathStreets[0], 
																						Paths[pos].PathStreets[1], 
																						0,
																						Paths[pos]);
				
			}
			
	
	}
	
	
	
}
