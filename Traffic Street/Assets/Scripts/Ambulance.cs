using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ambulance : MonoBehaviour {

	private const int AMBULANCE_HAPPEN_NUMBER = 2;
	
	public static List<int> ambulanceTimeSlots;
	
	public static void InitInstances(){
		ambulanceTimeSlots = new List<int>();
	}
	
	public  static void SetAmbulanceRandomTime(int timeBetweenEvents){
				
		int timeValue = 0;
		for (int i = 0 ; i<AMBULANCE_HAPPEN_NUMBER; i++){
			timeValue = Random.Range(20, 40);				//*********** I should make an enum to each level
			if(!ambulanceTimeSlots.Contains(timeValue)){
				ambulanceTimeSlots.Add(timeValue - (i * timeBetweenEvents));
			}	
		}	
		
	}
	
	public static bool InsideAmbulanceTimeSlotsList(float gameTime){
		bool found = false;
		int i=0;
		Debug.Log("timeSlots.count "+ ambulanceTimeSlots.Count);
		while(!found && i < ambulanceTimeSlots.Count){
			if(ambulanceTimeSlots [i] == gameTime)
				found = true;
			i++;
		}
		return found;
	}
	
	public static void GenerateAmbulance(int pos, GameObject ambulancePrefab, List<Path> Paths){	
		
			
			if(ambulancePrefab != null){
				
				GameObject vehicle;
					vehicle = Instantiate(ambulancePrefab, Paths[pos].GenerationPointPosition ,Quaternion.identity) as GameObject;
					Paths[pos].PathStreets[0].VehiclesNumber ++;
					//vehicle.name = "Street # "+Paths[pos].PathStreets[0].ID + " # " + Paths[pos].PathStreets[0].VehiclesNumber;
					vehicle.name = "Street # "+Paths[pos].PathStreets[0].ID + " Car number " + GameMaster.vehicilesCounter;
					
					//	public Vehicle(VehicleType type,float speed,float size, Direction curDir, Street curStreet, Street nextStreet, Path path)
					vehicle.GetComponent<VehicleController>().myVehicle = new Vehicle(	VehicleType.Ambulance, 
																						25.0f, 
																						MathsCalculatios.getVehicleLargeSize(vehicle), 
																						Paths[pos].PathStreets[0].StreetLight.Type, 
																						Paths[pos].PathStreets[0], 
																						Paths[pos].PathStreets[1], 
																						0,
																						Paths[pos]);
				
			}
			
	
	}
	
	
	
}
