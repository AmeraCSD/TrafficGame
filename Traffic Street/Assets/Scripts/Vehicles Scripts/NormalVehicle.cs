using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NormalVehicle : MonoBehaviour {
												// ***************** Groooooooopssssssssssssssss *******************
	
	public static void GenerateNormalVehicle(int pos,GameObject vehiclePrefab, Material tx, List<GamePath> Paths, Queue existedVehicles){	
		while(Paths[pos].PathStreets[0].VehiclesNumber >= Paths[pos].PathStreets[0].StreetCapacity){
			pos = Random.Range(0, Paths.Count);
		}
		
		//pos = 11;
		if(vehiclePrefab != null){
		//*****************************optimization
			GameObject vehicle;
			if(existedVehicles.Count == 0){
				vehicle = Instantiate(vehiclePrefab, Paths[pos].GenerationPointPosition ,Quaternion.identity) as GameObject;
				vehicle.renderer.material = tx;
				Paths[pos].PathStreets[0].VehiclesNumber ++;
				vehicle.name = "Street # "+Paths[pos].PathStreets[0].ID + " Car number " + GameMaster.vehicilesCounter;
				//	public Vehicle(VehicleType type,float speed,float size, Direction curDir, Street curStreet, Street nextStreet, Path path)
				vehicle.GetComponent<VehicleController>().myVehicle = new Vehicle(	VehicleType.Normal, 
																					Random.Range(Globals.NORMAL_AVG_VEHICLE_SPEED -1,Globals.NORMAL_AVG_VEHICLE_SPEED +2 ), 
																					MathsCalculatios.getVehicleLargeSize(vehicle), 
																					Paths[pos].PathStreets[0].StreetLight.Type, 
																					Paths[pos].PathStreets[0], 
																					Paths[pos].PathStreets[1], 
																					0,
																					Paths[pos],
																					Globals.Horns[Random.Range(0,4)]);
			}
			else{
				vehicle = existedVehicles.Dequeue() as GameObject;
				vehicle.transform.position = Paths[pos].GenerationPointPosition;
				vehicle.transform.rotation = Quaternion.identity;
				vehicle.GetComponent<VehicleController>().initInstancesAtFirst();
				vehicle.SetActive(true);
				Paths[pos].PathStreets[0].VehiclesNumber ++;
				vehicle.name = "Street # "+Paths[pos].PathStreets[0].ID + " Car number " + GameMaster.vehicilesCounter;
				
				//	public Vehicle(VehicleType type,float speed,float size, Direction curDir, Street curStreet, Street nextStreet, Path path)
				vehicle.GetComponent<VehicleController>().myVehicle = new Vehicle(	VehicleType.Normal, 
																					Random.Range(Globals.NORMAL_AVG_VEHICLE_SPEED -1,Globals.NORMAL_AVG_VEHICLE_SPEED +2 ), 
																					MathsCalculatios.getVehicleLargeSize(vehicle), 
																					Paths[pos].PathStreets[0].StreetLight.Type, 
																					Paths[pos].PathStreets[0], 
																					Paths[pos].PathStreets[1], 
																					0,
																					Paths[pos],Globals.Horns[Random.Range(0,4)]);
				vehicle.GetComponent<VehicleController>().InitStreetAndVehicleAttributes();
				
			}
			
		}	
		
	}
	
}
