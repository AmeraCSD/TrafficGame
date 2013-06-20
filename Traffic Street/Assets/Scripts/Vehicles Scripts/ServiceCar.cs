using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ServiceCar : MonoBehaviour {

	
	public static List<float> serviceCarTimeSlots;
	
	
	public static void InitInstances(){
		serviceCarTimeSlots = new List<float>();
	}
	
	public  static void SetEventTime(List<float> eventTimes){
	
		for (int i = 0 ; i<eventTimes.Count; i++){
			
			serviceCarTimeSlots.Add(eventTimes[i]);
			GameMaster.eventsWarningTimes.Add(eventTimes[i]+5);
			GameMaster.eventsWarningNames.Add("serviceCar");
		}	
		
	}
	
	
	public static bool InsideTimeSlotsList(float gameTime){
		bool found = false;
		int i=0;
		while(!found && i < serviceCarTimeSlots.Count){
			if(serviceCarTimeSlots [i] == gameTime)
				found = true;
			i++;
		}
		return found;
	}
	
	public static void GenerateVehicle(GameObject serviceCarPrefab, Material tx, GamePath path){	
					
		if(serviceCarPrefab != null){
			GameObject vehicle;
			vehicle = Instantiate(serviceCarPrefab, path.GenerationPointPosition ,Quaternion.identity) as GameObject;
			vehicle.renderer.material = tx;
			path.PathStreets[0].VehiclesNumber ++;
			//vehicle.name = "Street # "+path.PathStreets[0].ID + " # " + path.PathStreets[0].VehiclesNumber;
			vehicle.name = "Street # "+path.PathStreets[0].ID + " Car number " + GameMaster.vehicilesCounter;
			//	public Vehicle(VehicleType type,float speed,float size, Direction curDir, Street curStreet, Street nextStreet, Path path)
			vehicle.GetComponent<VehicleController>().myVehicle = new Vehicle(	VehicleType.ServiceCar, 
																				Globals.SERVICE_CAR_SPEED, 
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
	public  static List<int> SetGetServiceCarRandomStops( float timeFrom, float timeTo){
		List<int> serviceCarStops = new List<int>();
		int timeValue = 0;
		for (int i = 0 ; i< 2; i++){
			timeValue = Random.Range((int)timeFrom, (int)timeTo);				//*********** I should make an enum to each level
			if(!serviceCarStops.Contains(timeValue)){
				serviceCarStops.Add(timeValue - (i*2));
				//Debug.Log("Adding to the stops values ... " + (timeValue - (i *2)));
			}	
		}
		return serviceCarStops;
	}
	
	public static bool InsideServiceCarStops(List<int> serviceCarStops, float gameTime){
		bool found = false;
		int i=0;
		//Debug.Log("timeSlots.count "+ serviceCarStops.Count);
		while(!found && i < serviceCarStops.Count){
			if(serviceCarStops [i] == gameTime)
				found = true;
			i++;
		}
		return found;
	}
	
	*/
	
}
