using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ambulance : MonoBehaviour {

	private const int AMBULANCE_HAPPEN_NUMBER = 1;
	
	public static List<int> ambulanceTimeSlots;
	
	public static void InitInstances(){
		ambulanceTimeSlots = new List<int>();
	}
	
	public  static void SetAmbulanceRandomTime(int timeBetweenEvents){
				
		int timeValue = 0;
		timeValue = Random.Range(120, 130 );
		
		for (int i = 0 ; i<AMBULANCE_HAPPEN_NUMBER; i++){
			timeValue -= timeBetweenEvents;
			if(timeValue >= 130){
				timeValue -= 5;
			}
			if(!ambulanceTimeSlots.Contains(timeValue)){
				ambulanceTimeSlots.Add(timeValue);
				GameMaster.eventsWarningTimes.Add(timeValue+5);
				GameMaster.eventsWarningNames.Add("a");
			}
		}	
		
	}
	
	public static bool InsideAmbulanceTimeSlotsList(float gameTime){
		bool found = false;
		int i=0;
		//Debug.Log("timeSlots.count "+ ambulanceTimeSlots.Count);
		while(!found && i < ambulanceTimeSlots.Count){
			if(ambulanceTimeSlots [i] == gameTime)
				found = true;
			i++;
		}
		return found;
	}
	
	public static void GenerateAmbulance(int pos, GameObject ambulancePrefab, List<GamePath> Paths){	
		
			
			if(ambulancePrefab != null){
				
				GameObject vehicle;
					vehicle = Instantiate(ambulancePrefab, Paths[Paths.Count-1].GenerationPointPosition ,Quaternion.identity) as GameObject;
					Paths[Paths.Count-1].PathStreets[0].VehiclesNumber ++;
					//vehicle.name = "Street # "+Paths[pos].PathStreets[0].ID + " # " + Paths[pos].PathStreets[0].VehiclesNumber;
					vehicle.name = "Street # "+Paths[Paths.Count-1].PathStreets[0].ID + " Car number " + GameMaster.vehicilesCounter;
					
					//	public Vehicle(VehicleType type,float speed,float size, Direction curDir, Street curStreet, Street nextStreet, Path path)
					vehicle.GetComponent<VehicleController>().myVehicle = new Vehicle(	VehicleType.Ambulance, 
																						29.0f, 
																						MathsCalculatios.getVehicleLargeSize(vehicle), 
																						Paths[Paths.Count-1].PathStreets[0].StreetLight.Type, 
																						Paths[Paths.Count-1].PathStreets[0], 
																						Paths[Paths.Count-1].PathStreets[1], 
																						0,
																						Paths[Paths.Count-1]);
				
			}
			
	
	}
	
	
	
}
