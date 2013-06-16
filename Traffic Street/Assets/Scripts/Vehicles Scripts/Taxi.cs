using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Taxi : MonoBehaviour {

	public static List<float> taxiTimeSlots;
	
	public static void InitInstances(){
		taxiTimeSlots = new List<float>();
	}
	
	public  static void SetEventTime(List<float> eventTimes){
	
		for (int i = 0 ; i<eventTimes.Count; i++){
			
			taxiTimeSlots.Add(eventTimes[i]);
			GameMaster.eventsWarningTimes.Add(eventTimes[i]+5);
			GameMaster.eventsWarningNames.Add("taxi");
		}	
		
	}
	
	
	public static bool InsideTimeSlotsList(float gameTime){
		bool found = false;
		int i=0;
		while(!found && i < taxiTimeSlots.Count){
			if(taxiTimeSlots [i] == gameTime)
				found = true;
			i++;
		}
		return found;
	}
	
	public static void GenerateVehicle(GameObject taxiPrefab, GamePath path){	
					
		if(taxiPrefab != null){
			GameObject vehicle;
			vehicle = Instantiate(taxiPrefab, path.GenerationPointPosition ,Quaternion.identity) as GameObject;
			path.PathStreets[0].VehiclesNumber ++;
			//vehicle.name = "Street # "+path.PathStreets[0].ID + " # " + path.PathStreets[0].VehiclesNumber;
			vehicle.name = "Street # "+path.PathStreets[0].ID + " Car number " + GameMaster.vehicilesCounter;
			//	public Vehicle(VehicleType type,float speed,float size, Direction curDir, Street curStreet, Street nextStreet, Path path)
			vehicle.GetComponent<VehicleController>().myVehicle = new Vehicle(	VehicleType.Taxi, 
																				Globals.TAXI_SPEED, 
																				MathsCalculatios.getVehicleLargeSize(vehicle), 
																				path.PathStreets[0].StreetLight.Type, 
																				path.PathStreets[0], 
																				path.PathStreets[1], 
																				0,
																				path,
																				Globals.Horns[Random.Range(0,4)]);
				
		}	
	
	}
	/*
	public  static List<int> SetGetTaxiRandomStops( float timeFrom, float timeTo){
		List<int> taxiStops = new List<int>();
		int timeValue = 0;
		for (int i = 0 ; i< 2; i++){
			timeValue = Random.Range((int)timeFrom, (int)timeTo);				//*********** I should make an enum to each level
			if(!taxiStops.Contains(timeValue)){
				taxiStops.Add(timeValue - (i*2));
				//Debug.Log("Adding to the stops values ... " + (timeValue - (i *2)));
			}	
		}
		return taxiStops;
	}
	
	
	public static bool InsideTaxiStops(List<int> taxiStops, float gameTime){
		bool found = false;
		
		
		int i=0;
		//Debug.Log("timeSlots.count "+ serviceCarStops.Count);
		while(!found && i < taxiStops.Count){
			if(taxiStops [i] == gameTime)
				found = true;
			i++;
		}
		return found;
	}
	
	
	*/
}
