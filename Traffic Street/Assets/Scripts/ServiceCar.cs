using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ServiceCar : MonoBehaviour {

	private const int SERVICE_CAR_HAPPEN_NUMBER = 2;
	
	public static List<int> serviceCarTimeSlots;
	
	
	public static void InitInstances(){
		serviceCarTimeSlots = new List<int>();
	}
	
	public  static void SetServiceCarRandomTime(int timeBetweenEvents){
				
		int timeValue = 0;
		timeValue = Random.Range(130, 140);			//*********** I should make an enum to each level
		for (int i = 0 ; i<SERVICE_CAR_HAPPEN_NUMBER; i++){
			
			timeValue -= timeBetweenEvents;
		//	if(timeValue < 130 ){
		//		timeValue += 10;
		//	}
			if(timeValue >= 140){
				timeValue -= 5;
			}
			if(!serviceCarTimeSlots.Contains(timeValue)){
				serviceCarTimeSlots.Add(timeValue) ;
				GameMaster.eventsWarningTimes.Add(timeValue);
				Debug.Log("Adding to the list the time ..." + i + "...its equal to .. " + timeValue);
				GameMaster.eventsWarningNames.Add("s");
			}	
		}	
		
	}
	
	
	
	public static bool InsideServiceCarTimeSlotsList(float gameTime){
		bool found = false;
		int i=0;
		Debug.Log("timeSlots.count "+ serviceCarTimeSlots.Count);
		while(!found && i < serviceCarTimeSlots.Count){
			if(serviceCarTimeSlots [i] == gameTime)
				found = true;
			i++;
		}
		return found;
	}
	
	public static void GenerateServiceCar(int pos, GameObject serviceCarPrefab,Texture2D tx, List<GamePath> Paths){	
		
			
			if(serviceCarPrefab != null){
				
				GameObject vehicle;
					vehicle = Instantiate(serviceCarPrefab, Paths[pos].GenerationPointPosition ,Quaternion.identity) as GameObject;
					Paths[pos].PathStreets[0].VehiclesNumber ++;
					//vehicle.name = "Street # "+Paths[pos].PathStreets[0].ID + " # " + Paths[pos].PathStreets[0].VehiclesNumber;
					vehicle.name = "Street # "+Paths[pos].PathStreets[0].ID + " Car number " + GameMaster.vehicilesCounter;
					vehicle.renderer.material.mainTexture = tx;
					//	public Vehicle(VehicleType type,float speed,float size, Direction curDir, Street curStreet, Street nextStreet, Path path)
					vehicle.GetComponent<VehicleController>().myVehicle = new Vehicle(	VehicleType.ServiceCar, 
																						13.0f, 
																						MathsCalculatios.getVehicleLargeSize(vehicle), 
																						Paths[pos].PathStreets[0].StreetLight.Type, 
																						Paths[pos].PathStreets[0], 
																						Paths[pos].PathStreets[1], 
																						0,
																						Paths[pos]);
				
			}
			
	
	}
	
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
	
	
	
}
